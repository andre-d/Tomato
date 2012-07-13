using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tomato.Hardware;
using Tomato;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Globalization;
using System.Drawing;
using System.Security.Permissions;

namespace Lettuce
{
    public static class Program
    {
        public static DCPU CPU;
        public static DateTime LastTick;
        public static Debugger debugger;
        
        public static string lastbinFilepath = "";
        public static string lastlistingFilepath = "";
        public static bool lastlittleEndian = false;
        
        public static Dictionary<Device, Form> Windows = new Dictionary<Device, Form>();
        
        private static System.Threading.Timer timer;
        private static Point screenLocation = new Point();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        [STAThread]
        static void Main(string[] args)
        {
            RuntimeInfo.GatherInfo();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException, true);
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            }

            // Enumerate loaded devices from plugins and Tomato
            List<Device> PossibleDevices = new List<Device>();
            GenericKeyboard kb = new GenericKeyboard();
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var types = asm.GetTypes().Where(t => typeof(Device).IsAssignableFrom(t) && t.IsAbstract == false);
                foreach (var type in types)
                {
                    PossibleDevices.Add((Device)Activator.CreateInstance(type));
                }
            }

            CPU = new DCPU();
            string binFile = null;
            bool littleEndian = false;
            List<Device> devices = new List<Device>();
            CPU.IsRunning = false;
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg.StartsWith("-"))
                {
                    switch (arg)
                    {
                        case "--no-wait":
                        case "--nowait":
                            CPU.IsRunning = true;
                            break;
                        case "-c":
                        case "--connect":
                            string deviceID = args[++i];
                            string[] ids = deviceID.Split(',');
                            foreach (var dID in ids)
                            {
                                uint id;
                                if (uint.TryParse(dID, NumberStyles.HexNumber, null, out id))
                                {
                                    foreach (Device d in PossibleDevices)
                                    {
                                        if (d.DeviceID == id)
                                            devices.Add((Device)Activator.CreateInstance(d.GetType()));
                                    }
                                }
                                else
                                {
                                    foreach (Device d in PossibleDevices)
                                    {
                                        if (d.GetType().Name.ToLower() == dID.ToLower())
                                            devices.Add((Device)Activator.CreateInstance(d.GetType()));
                                    }
                                }
                            }
                            break;
                        case "--listing":
                            Debugger.LoadOrganicListing(args[++i]);
                            break;
                    }
                }
                else
                {
                    if (binFile == null)
                        binFile = arg;
                    else
                        Debugger.LoadOrganicListing(args[i]);
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
                    devices.Add(device);
            }
            if (!string.IsNullOrEmpty(binFile))
            {
                lastbinFilepath = binFile;
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
            foreach (var device in devices)
                CPU.ConnectDevice(device);

            debugger = new Debugger(ref CPU);
            debugger.StartPosition = FormStartPosition.Manual;
            debugger.Location = new Point(0, 0);
            debugger.ResetLayout();
            debugger.Show();
            
            screenLocation.Y = debugger.Location.Y + 4;
            screenLocation.X = debugger.Location.X + debugger.Width + 5;
            
            int keyboardCount = 0;
            foreach (Device d in CPU.Devices)
                if (d is LEM1802)
                    AddWindow(new LEM1802Window(d as LEM1802, CPU, true));
                else if (d is GenericKeyboard)
                    keyboardCount++;
            
            int remaining = keyboardCount - LEM1802Window.AssignedKeyboards.Count();
            for(;remaining > 0; remaining--)
                AddWindow(new GenerickeyboardWindow(null, CPU, true));
            
            debugger.Focus();
            LastTick = DateTime.Now;
            timer = new System.Threading.Timer(FetchExecute, null, 10, Timeout.Infinite);
            Application.Run(debugger);
            timer.Dispose();
        }
        
        static void AddWindow(LEM1802Window window) {
            window.StartPosition = FormStartPosition.Manual;
            window.Location = screenLocation;
            screenLocation.Y += window.Height + 12;
            window.Show();
            window.Invalidate();
            window.Update();
            window.Focus();
            if(window.Keyboard != null) {
                Windows.Add(window.Keyboard, window);
            }
            if(window.Screen != null) {
                Windows.Add(window.Screen, window);
            }
        }
        
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            CurrentDomain_UnhandledException(sender, new UnhandledExceptionEventArgs(e.Exception, false));
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            UnhandledExceptionForm uef = new UnhandledExceptionForm((Exception)e.ExceptionObject, e.IsTerminating);
            uef.ShowDialog();
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
