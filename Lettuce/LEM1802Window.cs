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
        public LEM1802 Screen;

        public LEM1802Window(LEM1802 LEM1802)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint | ControlStyles.Opaque, true);
            this.ClientSize = new Size(LEM1802.Width * 4, LEM1802.Height * 4);
            Screen = LEM1802;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Screen.BorderColor), new Rectangle(0, 0, this.Width, this.Height));
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.DrawImage(Screen.ScreenImage, 10, 10, this.ClientSize.Width - 20, this.ClientSize.Height - 20);
            this.Invalidate(true);
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
