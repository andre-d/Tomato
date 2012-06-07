using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tomato;
using System.IO;
using Tomato.Hardware;
using System.Reflection;
using System.Globalization;
using System.Threading;

namespace Pickles
{
    class Program
    {
        static List<Device> PossibleDevices = new List<Device>();
        static List<PinnedConsole> Consoles = new List<PinnedConsole>();
        static int ConsoleID = 0;
        static Timer ClockTimer;
        public static DCPU CPU;
        public static DateTime LastTick;
        public static Dictionary<string, string> Shortcuts = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            PinnedConsole globalConsole = new PinnedConsole()
            {
                ID = -1,
                Width = Console.WindowWidth,
                Height = Console.WindowHeight,
                Interval = -1,
                Left = 0,
                Top = 0,
                Global = true
            };

            Console.Clear();
            Console.TreatControlCAsInput = true;

            globalConsole.WriteLine("Pickles DCPU-16 Debugger     Copyright Drew DeVault 2012");
            globalConsole.WriteLine("Use \"help\" for assistance.");

            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var types = asm.GetTypes().Where(t => typeof(Device).IsAssignableFrom(t) && t.IsAbstract == false);
                foreach (var type in types)
                    PossibleDevices.Add((Device)Activator.CreateInstance(type));
            }

            string input;
            CPU = new DCPU();
            CPU.IsRunning = false;
            LastTick = DateTime.Now;
            ClockTimer = new System.Threading.Timer(FetchExecute, null, 10, Timeout.Infinite);
            while (true)
            {
                globalConsole.Write(">");
                string originalInput = ReadLineEnableShortcuts().Trim();
                ParseInput(originalInput, globalConsole);
                if (originalInput.ToLower() == "quit" || originalInput.ToLower() == "q")
                    break;
            }
        }

        private static void ParseInput(string originalInput, PinnedConsole Console)
        {
            string input = originalInput.ToLower().Trim();
            string[] parameters = input.Split(' ');

            if (input == "quit" || input == "q")
                return;
            else if (input == "clear")
            {
                if (Console.Global)
                    System.Console.Clear();
                else
                    Console.Clear();
            }
            else if (input.StartsWith("bind "))
            {
                string[] parts = input.Split(' ');
                Shortcuts.Add(parts[1].ToLower(), input.Substring(5 + parts[1].Length));
            }
            else if (input.StartsWith("pin ")) // pin x y interval command...
            {
                string[] parts = input.Split(' ');
                int x = int.Parse(parts[1]);
                int y = int.Parse(parts[2]);
                int interval = int.Parse(parts[3]);
                string command = input.Substring(input.IndexOf(' ') + 1);
                command = command.Substring(command.IndexOf(' ') + 1);
                command = command.Substring(command.IndexOf(' ') + 1);
                command = command.Substring(command.IndexOf(' ') + 1);
                PinnedConsole pc = new PinnedConsole()
                {
                    Command = command,
                    Left = x,
                    Top = y,
                    Interval = interval,
                    ID = ConsoleID++
                };
                pc.Timer = new Timer(new TimerCallback(UpdateConsole), pc, pc.Interval, System.Threading.Timeout.Infinite);
                Console.WriteLine("Console ID " + pc.ID.ToString() + " created.");
            }
            else if (input.StartsWith("flash "))
            {
                bool littleEndian = false;
                string file;
                if (parameters.Length == 2)
                    file = parameters[1];
                else if (parameters.Length == 3)
                {
                    file = parameters[2];
                    littleEndian = parameters[1].ToLower() == "little";
                }
                else
                    return;
                List<ushort> data = new List<ushort>();
                using (Stream stream = File.OpenRead(file))
                {
                    for (int i = 0; i < stream.Length; i += 2)
                    {
                        byte a = (byte)stream.ReadByte();
                        byte b = (byte)stream.ReadByte();
                        if (littleEndian)
                            data.Add((ushort)(a | (b << 8)));
                        else
                            data.Add((ushort)(b | (a << 8)));
                    }
                }
                CPU.FlashMemory(data.ToArray());
            }
            else if (input.StartsWith("connect "))
                ConnectDevice(input.Substring(8));
            else if (input == "start")
                CPU.IsRunning = true;
            else if (input == "stop")
                CPU.IsRunning = false;
            else if (input.StartsWith("step"))
            {
                if (CPU.IsRunning)
                {
                    Console.WriteLine("CPU is still running; use \"stop\" first.");
                    return;
                }
                string[] parts = input.Split(' ');
                if (parts.Length > 1)
                {
                    if (parts[1] == "into" || parts[1] == "once")
                        CPU.Execute(-1);
                    else if (parts[1] == "over")
                    {
                    }
                    else
                    {
                        int i = int.Parse(parts[1]);
                        while (i > 0)
                        {
                            CPU.Execute(-1);
                            i--;
                        }
                    }
                }
                else
                {
                    CPU.Execute(-1);
                }
            }
            else if (input.StartsWith("dump "))
            {
                string[] parts = input.Substring(5).Split(' ');
                if (parts[0] == "screen")
                {
                    if (parts.Length > 1)
                    {
                        int index = int.Parse(parts[1]);
                        if (CPU.Devices[index] is LEM1802)
                            DrawScreen(CPU.Devices[index] as LEM1802, Console);
                    }
                    else
                    {
                        foreach (var device in CPU.Devices)
                            if (device is LEM1802)
                            {
                                DrawScreen(device as LEM1802, Console);
                                break;
                            }
                    }
                }
                else if (parts[0] == "memory" || parts[0] == "mem")
                {
                    ushort start = 0;
                    if (parts.Length > 1)
                        start = ushort.Parse(parts[1], NumberStyles.HexNumber);
                    ushort end = (ushort)(start + 0x40);
                    if (parts.Length > 2)
                        end = ushort.Parse(parts[2], NumberStyles.HexNumber);
                    int index = 0;
                    while (start < end)
                    {
                        if (index % 8 == 0)
                            Console.Write("0x" + GetHexString(start, 4) + ": ");
                        if (CPU.PC == start)
                            Console.Write("[" + GetHexString(CPU.Memory[start], 4) + "]");
                        else
                            Console.Write(" " + GetHexString(CPU.Memory[start], 4) + " ");
                        if (index % 8 == 7)
                            Console.Write("\n");
                        index++;
                        start++;
                    }
                }
                else if (parts[0] == "stack")
                {
                    ushort address = CPU.SP;
                    for (ushort i = 0; i < 10; i++)
                    {
                        Console.WriteLine("[" + GetHexString(address, 4) + "]: " + GetHexString(CPU.Memory[address], 4));
                        address++;
                    }
                }
            }
            else if (input.StartsWith("list "))
            {
                string[] parts = input.Split(' ');
                if (parts[1] == "registers")
                {
                    Console.Write("A:  " + GetHexString(CPU.A, 4));
                    Console.Write("   B:  " + GetHexString(CPU.B, 4) + "\n");
                    Console.Write("C:  " + GetHexString(CPU.C, 4));
                    Console.Write("   X:  " + GetHexString(CPU.X, 4) + "\n");
                    Console.Write("Y:  " + GetHexString(CPU.Y, 4));
                    Console.Write("   Z:  " + GetHexString(CPU.Z, 4) + "\n");
                    Console.Write("I:  " + GetHexString(CPU.I, 4));
                    Console.Write("   J:  " + GetHexString(CPU.J, 4) + "\n");
                    Console.Write("PC: " + GetHexString(CPU.PC, 4));
                    Console.Write("   SP: " + GetHexString(CPU.SP, 4) + "\n");
                    Console.Write("EX: " + GetHexString(CPU.EX, 4));
                    Console.Write("   IA: " + GetHexString(CPU.IA, 4) + "\n");
                }
                else if (parts[1] == "hardware")
                {
                    foreach (var hw in CPU.Devices)
                        Console.WriteLine(hw.FriendlyName);
                }
            }
            else if (input.StartsWith("dasm") || input.StartsWith("dis"))
            {
                FastDisassembler fdas = new FastDisassembler(new Dictionary<ushort, string>());
                ushort start = (ushort)(CPU.PC - 2);
                string[] parts = input.Split(' ');
                if (parts.Length > 1)
                    start = ushort.Parse(parts[1], NumberStyles.HexNumber);
                var code = fdas.FastDisassemble(ref CPU.Memory, start, (ushort)(start + 0x10));
                foreach (var entry in code)
                {
                    if (CPU.PC == entry.Address)
                    {
                        ConsoleColor background = System.Console.BackgroundColor;
                        ConsoleColor foreground = System.Console.ForegroundColor;
                        System.Console.BackgroundColor = ConsoleColor.Yellow;
                        System.Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(GetHexString(entry.Address, 4) + ": " + entry.Code);
                        System.Console.ForegroundColor = foreground;
                        System.Console.BackgroundColor = background;
                    }
                    else if (CPU.Breakpoints.Where(b => b.Address == entry.Address).Count() != 0)
                    {
                        ConsoleColor background = System.Console.BackgroundColor;
                        ConsoleColor foreground = System.Console.ForegroundColor;
                        System.Console.BackgroundColor = ConsoleColor.DarkRed;
                        System.Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(GetHexString(entry.Address, 4) + ": " + entry.Code);
                        System.Console.ForegroundColor = foreground;
                        System.Console.BackgroundColor = background;
                    }
                    else
                        Console.WriteLine(GetHexString(entry.Address, 4) + ": " + entry.Code);
                }
            }
            else if (input.StartsWith("breakpoint "))
            {
                CPU.Breakpoints.Add(new Breakpoint()
                {
                    Address = ushort.Parse(input.Substring(11), NumberStyles.HexNumber)
                });
            }
            else if (input == "") { }
            else
            {
                if (File.Exists(input)) // Script?
                {
                    string[] file = File.ReadAllLines(input);
                    foreach (var line in file)
                        ParseInput(line, Console);
                }
                else
                    Console.WriteLine("Unknown command.");
            }
            return;
        }

        static void UpdateConsole(object o)
        {
            PinnedConsole pc = o as PinnedConsole;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            Console.CursorLeft = pc.Left;
            Console.CursorTop = pc.Top;
            pc.Clear();
            ParseInput(pc.Command, pc);
            Console.CursorLeft = left;
            Console.CursorTop = top;
            pc.Timer = new Timer(new TimerCallback(UpdateConsole), pc, pc.Interval, System.Threading.Timeout.Infinite);
        }

        private static List<string> History = new List<string>();
        public static string ReadLineEnableShortcuts()
        {
            string entry = "";
            int cursor = 0, historyEntry = -1, curLeft = Console.CursorLeft, curTop = Console.CursorTop;
            while (true)
            {
                // TODO: Support scrolling across newlines and window boundaries
                var keyinfo = Console.ReadKey(true);
                lock (PinnedConsole.DisplayLock)
                {
                    if (keyinfo.Modifiers == ConsoleModifiers.Control)
                    {
                        if (Shortcuts.ContainsKey(keyinfo.Key.ToString().ToLower()))
                            ParseInput(Shortcuts[keyinfo.Key.ToString().ToLower()], new PinnedConsole()
                                {
                                    Hidden = true
                                });
                        continue;
                    }
                    if (keyinfo.Key == ConsoleKey.Enter)
                        break;
                    else if (keyinfo.Key == ConsoleKey.LeftArrow)
                    {
                        if (cursor > 0)
                        {
                            Console.CursorLeft--;
                            cursor--;
                        }
                    }
                    else if (keyinfo.Key == ConsoleKey.RightArrow)
                    {
                        if (cursor < entry.Length)
                        {
                            Console.CursorLeft++;
                            cursor++;
                        }
                    }
                    else if (keyinfo.Key == ConsoleKey.UpArrow)
                    {
                        Console.SetCursorPosition(curLeft, curTop);
                        for (int i = 0; i < entry.Length; i++)
                            Console.Write(" ");
                        historyEntry++;
                        if (historyEntry > History.Count - 1)
                            historyEntry--;
                        Console.SetCursorPosition(curLeft, curTop);
                        Console.Write(History.ElementAt(historyEntry));
                        entry = History.ElementAt(historyEntry);
                        cursor = entry.Length - 1;
                    }
                    else if (keyinfo.Key == ConsoleKey.DownArrow)
                    {
                        Console.SetCursorPosition(curLeft, curTop);
                        for (int i = 0; i < entry.Length; i++)
                            Console.Write(" ");
                        historyEntry--;
                        if (historyEntry < -1)
                            historyEntry++;
                        if (historyEntry == -1)
                            entry = "";
                        else
                        {
                            entry = History.ElementAt(historyEntry);
                            cursor = entry.Length - 1;
                        }
                        Console.SetCursorPosition(curLeft, curTop);
                        Console.Write(entry);
                    }
                    else if (keyinfo.Key == ConsoleKey.Backspace)
                    {
                        if (cursor > 0)
                        {
                            Console.CursorLeft--;
                            cursor--;
                            entry = entry.Remove(cursor, 1);
                            int left = Console.CursorLeft;
                            Console.Write(entry.Substring(cursor) + " ");
                            Console.CursorLeft = left;
                        }
                    }
                    else if (keyinfo.Key == ConsoleKey.Delete)
                    {
                        if (cursor < entry.Length)
                        {
                            entry = entry.Remove(cursor, 1);
                            int left = Console.CursorLeft;
                            Console.Write(entry.Substring(cursor) + " ");
                            Console.CursorLeft = left;
                        }
                    }
                    else
                    {
                        entry = entry.Insert(cursor, keyinfo.KeyChar.ToString());
                        cursor++;
                        int left = Console.CursorLeft;
                        Console.Write(entry.Substring(cursor - 1));
                        Console.CursorLeft = left + 1;
                    }
                }
            }
            if (History.Count == 20)
                History.RemoveAt(History.Count - 1);
            History.Insert(0, entry);
            Console.Write("\n");
            return entry;
        }

        public static string GetHexString(uint value, int numDigits)
        {
            string result = value.ToString("x").ToUpper();
            while (result.Length < numDigits)
                result = "0" + result;
            return result;
        }

        private static void DrawScreen(LEM1802 Screen, PinnedConsole Console)
        {
            Console.Clear(); // TODO: not this
            ConsoleColor foregroundInitial = System.Console.ForegroundColor;
            ConsoleColor backgroundInitial = System.Console.BackgroundColor;
            ushort address = 0;
            System.Console.ForegroundColor = GetPaletteColor((byte)Screen.BorderColorValue);
            Console.Write("                                  \n");
            for (int y = 0; y < 12; y++)
            {
                System.Console.ForegroundColor = GetPaletteColor((byte)Screen.BorderColorValue);
                Console.Write(" ");
                for (int x = 0; x < 32; x++)
                {
                    ushort value = CPU.Memory[Screen.ScreenMap + address];

                    ConsoleColor background = GetPaletteColor((byte)((value & 0xF00) >> 8));
                    ConsoleColor foreground = GetPaletteColor((byte)((value & 0xF000) >> 12));
                    System.Console.ForegroundColor = foreground;
                    System.Console.BackgroundColor = background;
                    Console.Write(Encoding.ASCII.GetString(new byte[] { (byte)(value & 0xFF) }));
                    address++;
                }
                System.Console.ForegroundColor = GetPaletteColor((byte)Screen.BorderColorValue);
                Console.Write(" \n");
            }
            System.Console.ForegroundColor = foregroundInitial;
            System.Console.BackgroundColor = backgroundInitial;
        }

        private static ConsoleColor GetPaletteColor(byte p)
        {
            p &= 0xF;
            return new ConsoleColor[]
            {
                ConsoleColor.Black, // TODO
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.Black,
                ConsoleColor.White
            }[p];
        }

        private static void FetchExecute(object o)
        {
            if (!CPU.IsRunning)
            {
                ClockTimer = new System.Threading.Timer(FetchExecute, null, 10, Timeout.Infinite);
                LastTick = DateTime.Now;
                return;
            }
            TimeSpan timeToEmulate = DateTime.Now - LastTick;
            LastTick = DateTime.Now;

            CPU.Execute((int)(timeToEmulate.TotalMilliseconds * (CPU.ClockSpeed / 1000)));
            ClockTimer = new System.Threading.Timer(FetchExecute, null, 10, Timeout.Infinite);
        }

        private static void ConnectDevice(string device)
        {
            string[] ids = device.Split(',');
            foreach (var dID in ids)
            {
                uint id;
                if (uint.TryParse(dID, NumberStyles.HexNumber, null, out id))
                {
                    foreach (Device d in PossibleDevices)
                    {
                        if (d.DeviceID == id)
                            CPU.ConnectDevice((Device)Activator.CreateInstance(d.GetType()));
                    }
                }
                else
                {
                    foreach (Device d in PossibleDevices)
                    {
                        if (d.GetType().Name.ToLower() == dID.ToLower())
                            CPU.ConnectDevice((Device)Activator.CreateInstance(d.GetType()));
                    }
                }
            }
        }
    }
}
