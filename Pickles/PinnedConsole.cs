using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pickles
{
    internal class PinnedConsole
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Interval { get; set; }
        public string Command { get; set; }
        public int ID { get; set; }
        public bool Hidden { get; set; }
        public Timer Timer { get; set; }
        public bool Global { get; set; }
        internal static object DisplayLock = new object();

        public PinnedConsole()
        {
            Hidden = false;
        }

        public void WriteLine(string Text)
        {
            Write(Text + "\n");
        }

        public void Write(string s)
        {
            if (Hidden)
                return;
            lock (DisplayLock)
            {
                foreach (char c in s)
                {
                    if (c == '\n')
                    {
                        Console.CursorTop++;
                        Console.CursorLeft = Left;
                    }
                    else
                    {
                        Console.Write(c);
                    }
                }
            }
        }

        public void Clear()
        {
            if (Hidden || Global)
                return;
            lock (DisplayLock)
            {
                string clear = "";
                for (int i = 0; i < Width; i++)
                    clear += " ";
                for (int i = 0; i < Height; i++)
                {
                    Console.CursorLeft = Left;
                    Console.CursorTop = Top + i;
                    Console.Write(clear);
                }
                Console.CursorLeft = Left;
                Console.CursorTop = Top;
            }
        }
    }
}
