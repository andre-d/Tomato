using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tomato;
using Tomato.Hardware;
using System.Drawing.Drawing2D;
using System.Threading;

namespace Lettuce
{
    public partial class LEM1802Window : Form
    {
        public static List<int> AssignedKeyboards;
        public LEM1802 Screen;
        public GenericKeyboard Keyboard;
        public DCPU CPU;
        private int ScreenIndex, KeyboardIndex;

        /// <summary>
        /// Assigns a LEM to the window.  If AssignKeyboard is true, it will search for a keyboard
        /// in the given CPU and assign it to this window as well.
        /// </summary>
        public LEM1802Window(LEM1802 LEM1802, DCPU CPU, bool AssignKeyboard)
        {
            if (AssignedKeyboards == null)
                AssignedKeyboards = new List<int>();

            InitializeComponent();
            // Set up drawing
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint | ControlStyles.Opaque, true);
            this.ClientSize = new Size(LEM1802.Width * 4 + 20, LEM1802.Height * 4 + 35);
            // Take a screen
            Screen = LEM1802;
            ScreenIndex = CPU.ConnectedDevices.IndexOf(Screen);
            // Take a keyboard
            if (AssignKeyboard)
            {
                for (int i = 0; i < CPU.ConnectedDevices.Count; i++)
                {
                    if (AssignedKeyboards.Contains(i))
                        continue;
                    if (CPU.ConnectedDevices[i] is GenericKeyboard)
                    {
                        Keyboard = CPU.ConnectedDevices[i] as GenericKeyboard;
                        AssignedKeyboards.Add(i);
                        KeyboardIndex = i;
                        break;
                    }
                }
                this.KeyDown += new KeyEventHandler(LEM1802Window_KeyDown);
                this.KeyUp += new KeyEventHandler(LEM1802Window_KeyUp);
            }
            timer = new System.Threading.Timer(delegate(object o)
                {
                    InvalidateAsync();
                }, null, 16, 16); // 60 Hz
            FormClosing += new FormClosingEventHandler(LEM1802Window_FormClosing);
        }

        void LEM1802Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Dispose();
        }
        System.Threading.Timer timer;

        void LEM1802Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard != null)
                Keyboard.KeyUp(e.KeyCode);
        }

        void LEM1802Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard != null)
                Keyboard.KeyDown(e.KeyCode);
        }

        private delegate void InvalidateAsyncDelegate();
        private void InvalidateAsync()
        {
            if (this.InvokeRequired)
            {
                try
                {
                    InvalidateAsyncDelegate iad = new InvalidateAsyncDelegate(InvalidateAsync);
                    this.Invoke(iad);
                }
                catch { }
            }
            else
            {
                this.Invalidate();
                this.Update();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            string title = "LEM1802 #" + ScreenIndex;
            if (Keyboard != null)
                title += ", Generic Keyboard #" + KeyboardIndex;

            // Border
            e.Graphics.FillRectangle(new SolidBrush(Screen.BorderColor), new Rectangle(0, 0, this.Width, this.Height));

            // Title bar
            e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, this.Width, 15));
            // Devices
            e.Graphics.DrawString(title, this.Font, Brushes.Black, new PointF(0, 0));

            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            // Screen
            e.Graphics.DrawImage(Screen.ScreenImage, 10, 25, this.ClientSize.Width - 20, this.ClientSize.Height - 35);
        }

        private void takeScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = (Bitmap)Screen.ScreenImage.Clone();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Bitmap Image (*.bmp)|*.bmp|All Files (*.*)|*.*";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            image.Save(sfd.FileName);
        }
    }
}
