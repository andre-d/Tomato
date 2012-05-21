using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tomato.Hardware;
using Tomato;
using System.IO;
using System.Threading;

namespace Lettuce
{
    public static class Program
    {
        public static DCPU CPU;
        public static DateTime LastTick;
        public static Debugger debugger;

        private static System.Threading.Timer timer;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CPU = new DCPU();
            string binFile = null;
            bool littleEndian = false;
            List<Device> devices = new List<Device>();

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg.StartsWith("-"))
                {
                    switch (arg)
                    {
                        case "-w":
                        case "--wait":
                            CPU.IsRunning = false;
                            break;
                    }
                }
                else
                {
                    if (binFile == null)
                        binFile = arg;
                    else
                    {
                        Console.WriteLine("Invalid parameter: " + arg);
                        return;
                    }
                }
            }
            if (binFile == null)
            {
                MemoryConfiguration mc = new MemoryConfiguration();
                if (mc.ShowDialog() == DialogResult.OK)
                {
                    binFile = mc.FileName;
                    littleEndian = mc.LittleEndian;
                }
            }
            if (devices.Count == 0)
            {
                HardwareConfiguration hwc = new HardwareConfiguration();
                hwc.ShowDialog();
                foreach (var device in hwc.SelectedDevices)
                    CPU.ConnectDevice(device);
            }
            if (!string.IsNullOrEmpty(binFile))
            {
                // Load binary file
                List<ushort> data = new List<ushort>();
                using (Stream stream = File.OpenRead(binFile))
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
            else
                CPU.IsRunning = false;

            debugger = new Debugger(ref CPU);
            foreach (Device d in CPU.ConnectedDevices)
            {
                if (d is LEM1802)
                {
                    LEM1802Window window = new LEM1802Window(d as LEM1802);
                    window.Show();
                }
            }

            debugger.ResetLayout();

            LastTick = DateTime.Now;
            timer = new System.Threading.Timer(FetchExecute, null, 10, Timeout.Infinite);
            Application.Run(debugger);
            timer = null;
        }

        private static void FetchExecute(object o)
        {
            if (!CPU.IsRunning)
            {
                timer = new System.Threading.Timer(FetchExecute, null, 10, Timeout.Infinite);
                LastTick = DateTime.Now;
                return;
            }
            TimeSpan timeToEmulate = DateTime.Now - LastTick;
            LastTick = DateTime.Now;

            CPU.Execute((int)(timeToEmulate.TotalMilliseconds * (CPU.ClockSpeed / 1000)));
            debugger.ResetLayout();
            timer = new System.Threading.Timer(FetchExecute, null, 10, Timeout.Infinite);
        }
    }
}
