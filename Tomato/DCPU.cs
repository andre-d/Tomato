using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tomato.Hardware;

namespace Tomato
{
    public partial class DCPU
    {
        public DCPU()
        {
            ConnectedDevices = new List<Device>();
            Breakpoints = new List<ushort>();
            InterruptQueue = new Queue<ushort>();
            Memory = new ushort[0x10000];
            InterruptQueueEnabled = IsOnFire = false;
            IsRunning = true;
            if (Random == null)
                Random = new Random();
        }

        public List<Device> ConnectedDevices;
        public List<ushort> Breakpoints;
        public Queue<ushort> InterruptQueue;
        public bool InterruptQueueEnabled, IsOnFire;
        public ushort[] Memory;
        public ushort PC, SP, EX, IA, A, B, C, X, Y, Z, I, J;
        public int ClockSpeed = 100000;
        public bool IsRunning;
        /// <summary>
        /// Called when a breakpoint is hit, before it is executed.
        /// </summary>
        public event EventHandler BreakpointHit;

        private static Random Random;
        private int cycles = 0;

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
                    if (Breakpoints.Contains((ushort)(PC + 1)))
                        BreakpointHit(this, null);
                if (IsOnFire)
                    Memory[Random.Next(0xFFFF)] = (ushort)Random.Next(0xFFFF);
                if (!InterruptQueueEnabled && InterruptQueue.Count > 0)
                    FireInterrupt(InterruptQueue.Dequeue());

                ushort instruction = Memory[PC++];
                byte opcode = (byte)(instruction & 0x1F);
                byte valueB = (byte)((instruction & 0x3E0) >> 5);
                byte valueA = (byte)((instruction & 0xFC00) >> 10);
                ushort result, opA = 0, opB = 0;
                opA = Get(valueA);
                if (opcode != 0)
                {
                    ushort pc_old = PC, sp_old = SP;
                    opB = Get(valueB);
                    PC = pc_old;
                    SP = sp_old;
                }
                short opB_s = (short)opA;
                short opA_s = (short)opB;
                cycles--;
                switch (opcode)
                {
                    case 0x00: // (nonbasic)
                        switch (valueB)
                        {
                            case 0x01: // JSR a
                                cycles -= 2;
                                Memory[--SP] = PC;
                                PC = opA;
                                break;
                            case 0x08: // INT a
                                cycles -= 3;
                                FireInterrupt(opA);
                                break;
                            case 0x09: // IAG a
                                Set(valueA, IA);
                                break;
                            case 0x0A: // IAS a
                                IA = opA;
                                break;
                            case 0x0B: // RFI a
                                A = Memory[PC++];
                                PC = Memory[PC++];
                                InterruptQueueEnabled = false;
                                break;
                            case 0x10: // HWN a
                                cycles--;
                                Set(valueA, (ushort)ConnectedDevices.Count);
                                break;
                            case 0x11: // HWQ a
                                cycles -= 3;
                                if (opA < ConnectedDevices.Count)
                                {
                                    Device d = ConnectedDevices[opA];
                                    A = (ushort)(d.DeviceID & 0xFFFF);
                                    B = (ushort)((d.DeviceID & 0xFFFF0000) >> 16);
                                    C = d.Version;
                                    X = (ushort)(d.ManufacturerID & 0xFFFF);
                                    Y = (ushort)((d.ManufacturerID & 0xFFFF0000) >> 16);
                                }
                                break;
                            case 0x12: // HWI a
                                cycles -= 3;
                                cycles -= ConnectedDevices[opA].HandleInterrupt();
                                break;
                        }
                        break;
                    case 0x01: // SET b, a
                        Set(valueB, opA);
                        break;
                    case 0x02: // ADD b, a
                        cycles--;
                        if (opB + opA > 0xFFFF)
                            EX = 0xFFFF;
                        Set(valueB, (ushort)(opB + opA));
                        break;
                    case 0x03: // SUB b, a
                        cycles--;
                        if (opB - opA < 0)
                            EX = 0xFFFF;
                        Set(valueB, (ushort)(opB - opA));
                        break;
                    case 0x04: // MUL b, a
                        cycles--;
                        EX = (ushort)(((opB * opA) >> 16) & 0xffff);
                        Set(valueB, (ushort)(opB * opA));
                        break;
                    case 0x05: // MLI b, a
                        cycles--;
                        EX = (ushort)(((opB_s * opA_s) >> 16) & 0xffff);
                        Set(valueB, (ushort)(opB_s * opA_s));
                        break;
                    case 0x06: // DIV b, a
                        cycles -= 2;
                        if (opA == 0)
                        {
                            EX = 0;
                            Set(valueB, 0);
                        }
                        else
                        {
                            EX = (ushort)(((opB << 16) / opA) & 0xffff);
                            Set(valueB, (ushort)(opB / opA));
                        }
                        break;
                    case 0x07: // DVI b, a
                        cycles -= 2;
                        if (opA_s == 0)
                        {
                            EX = 0;
                            Set(valueB, 0);
                        }
                        else
                        {
                            EX = (ushort)(((opB_s << 16) / opA_s) & 0xffff);
                            Set(valueB, (ushort)(opB_s / opA_s));
                        }
                        break;
                    case 0x08: // MOD b, a
                        cycles -= 2;
                        if (opA == 0)
                            Set(valueB, 0);
                        else
                            Set(valueB, (ushort)(opB % opA));
                        break;
                    case 0x09: // MDI b, a
                        cycles -= 2;
                        if (opA_s == 0)
                            Set(valueB, 0);
                        else
                            Set(valueB, (ushort)(opB_s % opA_s));
                        break;
                    case 0x0A: // AND b, a
                        Set(valueB, (ushort)(opB & opA));
                        break;
                    case 0x0B: // BOR b, a
                        Set(valueB, (ushort)(opB | opA));
                        break;
                    case 0x0C: // XOR b, a
                        Set(valueB, (ushort)(opB ^ opA));
                        break;
                    case 0x0D: // SHR b, a
                        EX = (ushort)(((opB << 16) >> opA) & 0xffff);
                        Set(valueB, (ushort)(opB >> opA));
                        break;
                    case 0x0E: // ASR b, a
                        EX = (ushort)(((opB_s << 16) >> opA) & 0xffff);
                        Set(valueB, (ushort)(opB_s >> opA));
                        break;
                    case 0x0F: // SHL b, a
                        EX = (ushort)(((opB << opA) >> 16) & 0xffff);
                        Set(valueB, (ushort)(opB << opA));
                        break;
                    case 0x10: // IFB b, a
                        cycles -= 2;
                        if (!((ushort)(opB & opA) != 0))
                            SkipIfChain();
                        break;
                    case 0x11: // IFC b, a
                        cycles -= 2;
                        if (!((ushort)(opB & opA) == 0))
                            SkipIfChain();
                        break;
                    case 0x12: // IFE b, a
                        cycles -= 2;
                        if (!(opB == opA))
                            SkipIfChain();
                        break;
                    case 0x13: // IFN b, a
                        cycles -= 2;
                        if (!(opB != opA))
                            SkipIfChain();
                        break;
                    case 0x14: // IFG b, a
                        cycles -= 2;
                        if (!(opB > opA))
                            SkipIfChain();
                        break;
                    case 0x15: // IFA b, a
                        cycles -= 2;
                        if (!(opB_s > opA_s))
                            SkipIfChain();
                        break;
                    case 0x16: // IFL b, a
                        cycles -= 2;
                        if (!(opB < opA))
                            SkipIfChain();
                        break;
                    case 0x17: // IFU b, a
                        cycles += 2;
                        if (!(opB_s < opA_s))
                            SkipIfChain();
                        break;
                    case 0x1A: // ADX b, a
                        cycles -= 2;
                        if (opB + opA + EX > 0xFFFF)
                            EX = 0x0001;
                        Set(valueB, (ushort)(opB + opA + EX));
                        break;
                    case 0x1B: // SBX b, a
                        cycles -= 2;
                        if (opB - opA + EX > 0xFFFF)
                            EX = 0x0001;
                        Set(valueB, (ushort)(opB - opA + EX));
                        break;
                    case 0x1E: // STI b, a
                        cycles--;
                        Set(valueB, opA);
                        I++;
                        J++;
                        break;
                    case 0x1F: // STD b, a
                        cycles--;
                        Set(valueB, opA);
                        I--;
                        J--;
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
            byte opcode = 0x11;
            while (opcode > 0x10)
            {
                cycles--;
                ushort instruction = Memory[PC++];
                opcode = (byte)(instruction & 0x1F);
                byte valueB = (byte)((instruction & 0x3E0) >> 5);
                byte valueA = (byte)((instruction & 0xFC00) >> 10);
                Get(valueA);
                Get(valueB);
            }
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
                if (IA != 0)
                {
                    Memory[--SP] = PC;
                    Memory[--SP] = A;
                    PC = IA;
                    A = Message;
                    InterruptQueueEnabled = true;
                }
            }
        }

        public void ConnectDevice(Device Device)
        {
            Device.AttachedCPU = this;
            ConnectedDevices.Add(Device);
        }

        public void FlashMemory(ushort[] Data)
        {
            Array.Copy(Data, Memory, Data.Length);
        }

        #region Get/Set

        private void Set(byte destination, ushort value)
        {
            switch (destination)
            {
                case 0x00:
                    A = value;
                    break;
                case 0x01:
                    B = value;
                    break;
                case 0x02:
                    C = value;
                    break;
                case 0x03:
                    X = value;
                    break;
                case 0x04:
                    Y = value;
                    break;
                case 0x05:
                    Z = value;
                    break;
                case 0x06:
                    I = value;
                    break;
                case 0x07:
                    J = value;
                    break;
                case 0x08:
                    Memory[A] = value;
                    break;
                case 0x09:
                    Memory[B] = value;
                    break;
                case 0x0A:
                    Memory[C] = value;
                    break;
                case 0x0B:
                    Memory[X] = value;
                    break;
                case 0x0C:
                    Memory[Y] = value;
                    break;
                case 0x0D:
                    Memory[Z] = value;
                    break;
                case 0x0E:
                    Memory[I] = value;
                    break;
                case 0x0F:
                    Memory[J] = value;
                    break;
                case 0x10:
                    cycles--;
                    Memory[A + Memory[PC++]] = value;
                    break;
                case 0x11:
                    cycles--;
                    Memory[B + Memory[PC++]] = value;
                    break;
                case 0x12:
                    cycles--;
                    Memory[C + Memory[PC++]] = value;
                    break;
                case 0x13:
                    cycles--;
                    Memory[X + Memory[PC++]] = value;
                    break;
                case 0x14:
                    cycles--;
                    Memory[Y + Memory[PC++]] = value;
                    break;
                case 0x15:
                    cycles--;
                    Memory[Z + Memory[PC++]] = value;
                    break;
                case 0x16:
                    cycles--;
                    Memory[I + Memory[PC++]] = value;
                    break;
                case 0x17:
                    cycles--;
                    Memory[J + Memory[PC++]] = value;
                    break;
                case 0x18:
                    Memory[--SP] = value;
                    break;
                case 0x19:
                    Memory[SP] = value;
                    break;
                case 0x1A:
                    cycles--;
                    Memory[SP + Memory[PC++]] = value;
                    break;
                case 0x1B:
                    SP = value;
                    break;
                case 0x1C:
                    PC = value;
                    break;
                case 0x1D:
                    EX = value;
                    break;
                case 0x1E:
                    cycles--;
                    Memory[Memory[PC++]] = value;
                    break;
                case 0x1F:
                    cycles--;
                    PC++;
                    break;
            }
        }

        private ushort Get(byte target)
        {
            switch (target)
            {
                case 0x00:
                    return A;
                case 0x01:
                    return B;
                case 0x02:
                    return C;
                case 0x03:
                    return X;
                case 0x04:
                    return Y;
                case 0x05:
                    return Z;
                case 0x06:
                    return I;
                case 0x07:
                    return J;
                case 0x08:
                case 0x09:
                case 0x0A:
                case 0x0B:
                case 0x0C:
                case 0x0D:
                case 0x0E:
                case 0x0F:
                    return Memory[(ushort)(Get((byte)(target - 0x08)))];
                case 0x10:
                case 0x11:
                case 0x12:
                case 0x13:
                case 0x14:
                case 0x15:
                case 0x16:
                case 0x17:
                    cycles--;
                    return Memory[(ushort)(Get((byte)(target - 0x10)) + Memory[PC++])];
                case 0x18:
                    return Memory[SP++];
                case 0x19:
                    return Memory[SP];
                case 0x1A:
                    cycles--;
                    return Memory[(ushort)(SP + Memory[PC++])];
                case 0x1B:
                    return SP;
                case 0x1C:
                    return PC;
                case 0x1D:
                    return EX;
                case 0x1E:
                    cycles--;
                    return Memory[Memory[PC++]];
                case 0x1F:
                    cycles--;
                    return Memory[PC++];
            }
            if (target > 0x1F && target < 0x40)
                return (ushort)(target - 0x21);
            return 0x00;
        }

        #endregion
    }
}
