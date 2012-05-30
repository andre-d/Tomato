using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tomato;
using Inorganic;

namespace Lettuce
{
    public partial class DisassemblyDisplay : UserControl
    {
        public ushort SelectedAddress { get; set; }
        public DCPU CPU { get; set; }
        public ushort EndAddress { get; set; }
        private ushort wordsWide;
        private Point MouseLocation;

        public DisassemblyDisplay()
        {
            this.CPU = new DCPU();
            InitializeComponent();
            this.Font = new Font(FontFamily.GenericMonospace, 8);
            this.MouseMove += new MouseEventHandler(DisassemblyDisplay_MouseMove);
            EnableUpdates = true;
        }

        void DisassemblyDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            MouseLocation = e.Location;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (ClientRectangle.IntersectsWith(new Rectangle(e.Location, new Size(1, 1))))
            {
                ((HandledMouseEventArgs)e).Handled = true;
                if (e.Delta > 0)
                    SelectedAddress--;
                else if (e.Delta < 0)
                    SelectedAddress++;
                this.Invalidate();
                base.OnMouseWheel(e);
            }
        }

        public DisassemblyDisplay(ref DCPU CPU) : this()
        {
            InitializeComponent();
            this.CPU = CPU;
            this.Font = new Font(FontFamily.GenericMonospace, 8);
            this.MouseMove += new MouseEventHandler(DisassemblyDisplay_MouseMove);
            EnableUpdates = true;
        }

        static Color[] PriorToPC;
        List<CodeEntry> Disassembly;
        public bool EnableUpdates { get; set; }

        private void DisassemblyDisplay_Paint(object sender, PaintEventArgs e)
        {
            this.Font = new Font(FontFamily.GenericMonospace, 12);

            e.Graphics.FillRectangle(Brushes.White, this.ClientRectangle);
            if (this.DesignMode)
            {
                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                return;
            }

            FastDisassembler disassembler = new FastDisassembler();
            Disassembly = disassembler.FastDisassemble(ref CPU.Memory, SelectedAddress, (ushort)(SelectedAddress + 100));

            int index = 0;
            bool setLast = false;

            for (int y = 0; y < this.Height; y += TextRenderer.MeasureText("0000", this.Font).Height + 2)
            {
                string address = Debugger.GetHexString(Disassembly[index].Address, 4) + ": ";
                Brush foreground = Brushes.Black;

                if (Disassembly[index].Address == CPU.PC)
                    e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(0, y, this.Width, TextRenderer.MeasureText(address, this.Font).Height + 2));
                if (CPU.Breakpoints.Contains(Disassembly[index].Address))
                {
                    e.Graphics.FillRectangle(Brushes.DarkRed, new Rectangle(0, y, this.Width, TextRenderer.MeasureText(address, this.Font).Height + 2));
                    foreground = Brushes.White;
                }

                e.Graphics.DrawString(address, this.Font, Brushes.Gray, 2, y);

                e.Graphics.DrawString(Disassembly[index].Code, this.Font, foreground, 2 + TextRenderer.MeasureText(address, this.Font).Width + 3, y);
                if (y + TextRenderer.MeasureText(address, this.Font).Height > this.Height)
                {
                    setLast = true;
                    EndAddress = Disassembly[index].Address;
                }

                index++;
            }
            if (!setLast)
                EndAddress = Disassembly[index--].Address;
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }
    }
}
