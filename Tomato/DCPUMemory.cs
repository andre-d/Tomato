using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tomato
{
    public class DCPUMemory
    {
        private ushort[] memory;

        public ushort A { get { return memory[0x10000]; } set { memory[0x10000] = value; } }
        public ushort B { get { return memory[0x10001]; } set { memory[0x10001] = value; } }
        public ushort C { get { return memory[0x10002]; } set { memory[0x10002] = value; } }
        public ushort X { get { return memory[0x10003]; } set { memory[0x10003] = value; } }
        public ushort Y { get { return memory[0x10004]; } set { memory[0x10004] = value; } }
        public ushort Z { get { return memory[0x10005]; } set { memory[0x10005] = value; } }
        public ushort I { get { return memory[0x10006]; } set { memory[0x10006] = value; } }
        public ushort J { get { return memory[0x10007]; } set { memory[0x10007] = value; } }
        public ushort SP { get { return memory[0x10008]; } set { memory[0x10008] = value; } }
        public ushort PC { get { return memory[0x10009]; } set { memory[0x10009] = value; } }
        public ushort EX { get { return memory[0x1000A]; } set { memory[0x1000A] = value; } }
        public ushort IA { get { return memory[0x1000B]; } set { memory[0x1000B] = value; } }

        public ushort[] Registers
        {
            get
            {
                ushort[] copy = new ushort[memory.Length - 0x10000];
                Array.Copy(memory, 0x10000, copy, 0, copy.Length);
                return copy;
            }
        }

        public ushort[] Raw
        {
            get
            {
                return memory;
            }
        }

        public ushort this[int i]
        {
            get
            {
                return memory[i];
            }

            set
            {
                memory[i] = value;
            }
        }

        public DCPUMemory()
        {
            memory = new ushort[0x10000 + 8 + 3 + 1];
        }

        public void Reset()
        {
            for (var i = 0x10000; i < memory.Length; i++)
            {
                memory[i] = 0;
            }
        }

        public void Flash(ushort[] data)
        {
            Array.Copy(data, memory, data.Length);
        }

        public void Flash(ushort[] data, int index)
        {
            Array.Copy(data, 0, memory, index, data.Length);
        }
    }
}
