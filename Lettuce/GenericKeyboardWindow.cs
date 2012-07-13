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
    public partial class GenerickeyboardWindow : LEM1802Window
    {
        protected override void InitClientSize() {
            this.ClientSize = new Size(200, 50);
        }
        
        public GenerickeyboardWindow(LEM1802 LEM1802, DCPU CPU, bool AssignKeyboard)
        :base(LEM1802, CPU, AssignKeyboard)
        {
            this.Text = "Generic Keyboard";
            this.Name = "test";
            this.takeScreenshotToolStripMenuItem.Enabled = false;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            string title = "Generic Keyboard #" + KeyboardIndex + "\n\n(type in here)";
            // Title bar
            e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, this.Width, this.Height));
            // Devices
            e.Graphics.DrawString(title, this.Font, Brushes.Black, new PointF(0, 0));
        }
    }
}
