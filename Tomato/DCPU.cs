using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tomato.Hardware;

namespace Tomato
{
    public partial class DCPU
    {
        private static int[] OPCODE_COST = new int[] {
            0,1,2,2,2,2,3,3,3,3,1,1,1,1,1,1,2,2,2,2,2,2,2,2,0,0,3,3,0,0,2,2
        };
        private static int[] BOPCODE_COST = new int [] {
            0,3,0,0,0,0,0,0,4,1,1,3,2,0,0,0,2,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0
        };
        private static int[] FIELD_COST = new int[] {
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,1,0,0,0,1,1
        };

        public List<Device> ConnectedDevices;
        public List<ushort> Breakpoints;
        public Queue<ushort> InterruptQueue;
        public bool InterruptQueueEnabled, IsOnFire;
        public DCPUMemory Memory;

        public int ClockSpeed = 100000;
        public bool IsRunning;
        /// <summary>
        /// Called when a breakpoint is hit, before it is executed.
        /// </summary>
        public event EventHandler<BreakpointEventArgs> BreakpointHit;

        private static Random Random;
        private int cycles = 0;

        public DCPU()
        {
            ConnectedDevices = new List<Device>();
            Breakpoints = new List<ushort>();
            InterruptQueue = new Queue<ushort>();
            Memory = new DCPUMemory();
            InterruptQueueEnabled = IsOnFire = false;
            IsRunning = true;
            if (Random == null)
                Random = new Random();
        }

        public ushort InstructionLength(ushort address)
        {
            ushort length = 1;
            ushort instruction = Memory[address];
            byte opcode = (byte)(instruction & 0x1F);
            byte valueB = (byte)((instruction & 0x3E0) >> 5);
            byte valueA = (byte)((instruction & 0xFC00) >> 10);
            length += (ushort)(((valueA >= 0x10 && valueA <= 0x17) ||
                                 valueA == 0x1E || valueA == 0x1F) ? 1 : 0);
            if (opcode != 0)
            {
                length += (ushort)(((valueB >= 0x10 && valueB <= 0x17) ||
                     valueB == 0x1E || valueB == 0x1F) ? 1 : 0);
            }
            return length;
        }

        public int InstructionCost(int instruction)
        {
            int opcode = instruction & 0x1F;
            int valueB = (instruction & 0x3E0) >> 5;
            int valueA = (instruction & 0xFC00) >> 10;

            if (opcode == 0)
            {
                return BOPCODE_COST[valueB] + FIELD_COST[valueA];
            }
            else
            {
                return OPCODE_COST[opcode] + BOPCODE_COST[valueB] + FIELD_COST[valueA];
            }
        }

        public void Execute(int CyclesToExecute)
        {
            if (!IsRunning && CyclesToExecute != -1)
                return;
            int oldCycles = cycles;
            if (CyclesToExecute == -1)
                cycles = 1;
            else
                cycles += CyclesToExecute;
            while (cycles > 0)
            {
                if (BreakpointHit != null)
                    if (Breakpoints.Contains(Memory.PC))
                    {
                        BreakpointEventArgs bea = new BreakpointEventArgs();
                        BreakpointHit(this, bea);
                        if (!bea.ContinueExecution)
                            return;
                    }
                if (IsOnFire)
                    Memory[Random.Next(0xFFFF)] = (ushort)Random.Next(0xFFFF);
                if (!InterruptQueueEnabled && InterruptQueue.Count > 0)
                    FireInterrupt(InterruptQueue.Dequeue());

                ushort instruction = Memory[Memory.PC++];
                byte opcode = (byte)(instruction & 0x1F);
                byte valueB = (byte)((instruction & 0x3E0) >> 5);
                byte valueA = (byte)((instruction & 0xFC00) >> 10);
                ushort opA = 0, opB = 0;
                int ea_a = -1, ea_b = -1;

                cycles -= InstructionCost(instruction);

                opA = Get(true, valueA, ref ea_a);
                short opB_s = (short)opB;
                short opA_s = (short)opA;

                if (opcode != 0) {
                    opB = Get(false, valueB, ref ea_b);
                }

                switch (opcode)
                {
                    case 0x00: // (nonbasic)
                        switch (valueB)
                        {
                            case 0x01: // JSR a
                                Memory[--Memory.SP] = Memory.PC;
                                Memory.PC = opA;
                                break;
                            case 0x08: // INT a
                                InterruptQueueEnabled = false;
                                FireInterrupt(opA);
                                break;
                            case 0x09: // IAG a
                                Set(ea_a, Memory.IA);
                                break;
                            case 0x0A: // IAS a
                                Memory.IA = opA;
                                break;
                            case 0x0B: // RFI a
                                Memory.A = Memory[Memory.SP++];
                                Memory.PC = Memory[Memory.SP++];
                                InterruptQueueEnabled = false;
                                break;
                            case 0x10: // HWN a
                                Set(ea_a, (ushort)ConnectedDevices.Count);
                                break;
                            case 0x11: // HWQ a
                                if (opA < ConnectedDevices.Count)
                                {
                                    Device d = ConnectedDevices[opA];
                                    Memory.A = (ushort)(d.DeviceID & 0xFFFF);
                                    Memory.B = (ushort)((d.DeviceID & 0xFFFF0000) >> 16);
                                    Memory.C = d.Version;
                                    Memory.X = (ushort)(d.ManufacturerID & 0xFFFF);
                                    Memory.Y = (ushort)((d.ManufacturerID & 0xFFFF0000) >> 16);
                                }
                                break;
                            case 0x12: // HWI a
                                if (opA < ConnectedDevices.Count)
                                    cycles -= ConnectedDevices[opA].HandleInterrupt();
                                break;
                        }
                        break;
                    case 0x01: // SET b, a
                        Set(ea_b, opA);
                        break;
                    case 0x02: // ADD b, a
                        if (opB + opA > 0xFFFF)
                            Memory.EX = 0x0001;
                        else
                            Memory.EX = 0;
                        Set(ea_b, (ushort)(opB + opA));
                        break;
                    case 0x03: // SUB b, a
                        if (opB - opA < 0)
                            Memory.EX = 0xFFFF;
                        else
                            Memory.EX = 0;
                        Set(ea_b, (ushort)(opB - opA));
                        break;
                    case 0x04: // MUL b, a
                        Memory.EX = (ushort)(((opB * opA) >> 16) & 0xffff);
                        Set(ea_b, (ushort)(opB * opA));
                        break;
                    case 0x05: // MLI b, a
                        Memory.EX = (ushort)(((opB_s * opA_s) >> 16) & 0xffff);
                        Set(ea_b, (ushort)(opB_s * opA_s));
                        break;
                    case 0x06: // DIV b, a
                        if (opA == 0)
                        {
                            Memory.EX = 0;
                            Set(ea_b, 0);
                        }
                        else
                        {
                            Memory.EX = (ushort)(((opB << 16) / opA) & 0xffff);
                            Set(ea_b, (ushort)(opB / opA));
                        }
                        break;
                    case 0x07: // DVI b, a
                        if (opA_s == 0)
                        {
                            Memory.EX = 0;
                            Set(ea_b, 0);
                        }
                        else
                        {
                            Memory.EX = (ushort)(((opB_s << 16) / opA_s) & 0xffff);
                            Set(ea_b, (ushort)(opB_s / opA_s));
                        }
                        break;
                    case 0x08: // MOD b, a
                        if (opA == 0)
                            Set(ea_b, 0);
                        else
                            Set(ea_b, (ushort)(opB % opA));
                        break;
                    case 0x09: // MDI b, a
                        if (opA_s == 0)
                            Set(ea_b, 0);
                        else
                            Set(ea_b, (ushort)(opB_s % opA_s));
                        break;
                    case 0x0A: // AND b, a
                        Set(ea_b, (ushort)(opB & opA));
                        break;
                    case 0x0B: // BOR b, a
                        Set(ea_b, (ushort)(opB | opA));
                        break;
                    case 0x0C: // XOR b, a
                        Set(ea_b, (ushort)(opB ^ opA));
                        break;
                    case 0x0D: // SHR b, a
                        Memory.EX = (ushort)(((opB << 16) >> opA) & 0xffff);
                        Set(ea_b, (ushort)(opB >> opA));
                        break;
                    case 0x0E: // ASR b, a
                        Memory.EX = (ushort)(((opB_s << 16) >> opA) & 0xffff);
                        Set(ea_b, (ushort)(opB_s >> opA));
                        break;
                    case 0x0F: // SHL b, a
                        Memory.EX = (ushort)(((opB << opA) >> 16) & 0xffff);
                        Set(ea_b, (ushort)(opB << opA));
                        break;
                    case 0x10: // IFB b, a
                        if (!((ushort)(opB & opA) != 0))
                            SkipIfChain();
                        break;
                    case 0x11: // IFC b, a
                        if (!((ushort)(opB & opA) == 0))
                            SkipIfChain();
                        break;
                    case 0x12: // IFE b, a
                        if (!(opB == opA))
                            SkipIfChain();
                        break;
                    case 0x13: // IFN b, a
                        if (!(opB != opA))
                            SkipIfChain();
                        break;
                    case 0x14: // IFG b, a
                        if (!(opB > opA))
                            SkipIfChain();
                        break;
                    case 0x15: // IFA b, a
                        if (!(opB_s > opA_s))
                            SkipIfChain();
                        break;
                    case 0x16: // IFL b, a
                        if (!(opB < opA))
                            SkipIfChain();
                        break;
                    case 0x17: // IFU b, a
                        if (!(opB_s < opA_s))
                            SkipIfChain();
                        break;
                    case 0x1A: // ADX b, a
                        uint resADX = (uint)(opB + opA + (short)Memory.EX);
                        Memory.EX = (ushort)((resADX >> 16) & 0xFFFF);
                        Set(ea_b, (ushort)resADX);
                        break;
                    case 0x1B: // SBX b, a
                        uint resSBX = (uint)(opB - opA + (short)Memory.EX);
                        Memory.EX = (ushort)((resSBX >> 16) & 0xFFFF);
                        Set(ea_b, (ushort)resSBX);
                        break;
                    case 0x1E: // STI b, a
                        cycles--;
                        Set(ea_b, opA);
                        Memory.I++;
                        Memory.J++;
                        break;
                    case 0x1F: // STD b, a
                        cycles--;
                        Set(ea_b, opA);
                        Memory.I--;
                        Memory.J--;
                        break;
                    default:
                        // According to spec, should take zero cycles
                        // For sanity, all NOPs take one cycle
                        break;
                }
            }
            if (CyclesToExecute == -1)
                cycles = oldCycles;
        }

        private void SkipIfChain()
        {
            int opcode;
            do
            {
                opcode = Memory[Memory.PC] & 0x1F;
                Memory.PC += InstructionLength(Memory.PC);
            } while (opcode >= 0x10 && opcode <= 0x17);
        }

        public void FireInterrupt(ushort Message)
        {
            if (InterruptQueueEnabled)
            {
                InterruptQueue.Enqueue(Message);
                if (InterruptQueue.Count > 0xFF)
                    IsOnFire = true;
            }
            else
            {
                if (Memory.IA != 0)
                {
                    Memory[--Memory.SP] = Memory.PC;
                    Memory[--Memory.SP] = Memory.A;
                    Memory.PC = Memory.IA;
                    Memory.A = Message;
                    InterruptQueueEnabled = true;
                }
            }
        }

        public void ConnectDevice(Device Device)
        {
            Device.AttachedCPU = this;
            ConnectedDevices.Add(Device);
        }

        #region Get/Set

        public void Set(int ea, ushort value)
        {
            if (ea >= 0)
                Memory[ea] = value;
        }

        public ushort Get(bool field_a, byte target, ref int ea)
        {
            switch (target)
            {
                case 0x00: case 0x01: case 0x02: case 0x03: 
                case 0x04: case 0x05: case 0x06: case 0x07:
                    ea = 0x10000 + target;
                    break;
                case 0x08: case 0x09: case 0x0A: case 0x0B:
                case 0x0C: case 0x0D: case 0x0E: case 0x0F:
                    ea = Memory[0x10000 - 8 + target];
                    break;
                case 0x10: case 0x11: case 0x12: case 0x13:
                case 0x14: case 0x15: case 0x16: case 0x17:
                    ea = (Memory[0x10000 - 16 + target] + Memory[Memory.PC++]) & 0xFFFF;
                    cycles--;
                    break ;
                case 0x18:
                    ea = field_a ? Memory.SP++ : --Memory.SP;
                    break;
                case 0x19:
                    ea = Memory.SP;
                    break ;
                case 0x1A:
                    ea = (Memory.SP + Memory[Memory.PC++]) & 0xFFFF;
                    cycles--;
                    break;
                case 0x1B: case 0x1C: case 0x1D:
                    ea = 0x10008 - 0x1B + target;
                    break ;
                case 0x1E:
                    ea = Memory[Memory.PC++];
                    cycles--;
                    break;
                case 0x1F:
                    ea = Memory.PC++;
                    cycles--;
                    break;
                default:
                    ea = -1;
                    return (ushort)(target - 0x21);
            }
            return Memory[ea];
        }

        #endregion

        public void Reset()
        {
            Memory.Reset();

            foreach (var device in ConnectedDevices)
                device.Reset();
        }
    }
}
