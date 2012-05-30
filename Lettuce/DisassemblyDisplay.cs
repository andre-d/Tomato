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
using System.Threading;

namespace Lettuce
{
    public partial class DisassemblyDisplay : UserControl
    {
        public ushort SelectedAddress { get; set; }
        public DCPU CPU { get; set; }
        public ushort EndAddress { get; set; }
        private ushort wordsWide;
        private Point MouseLocation;
        private bool IsMouseWithin;

        public DisassemblyDisplay()
        {
            this.CPU = new DCPU();
            InitializeComponent();
            this.Font = new Font(FontFamily.GenericMonospace, 8);
            this.MouseMove += new MouseEventHandler(DisassemblyDisplay_MouseMove);
            this.MouseEnter += new EventHandler(DisassemblyDisplay_MouseEnter);
            this.MouseLeave += new EventHandler(DisassemblyDisplay_MouseLeave);
            this.MouseDoubleClick += new MouseEventHandler(DisassemblyDisplay_MouseDoubleClick);
            EnableUpdates = true;
        }

        void DisassemblyDisplay_MouseLeave(object sender, EventArgs e)
        {
            IsMouseWithin = false;
        }

        void DisassemblyDisplay_MouseEnter(object sender, EventArgs e)
        {
            IsMouseWithin = true;
        }

        void DisassemblyDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            MouseLocation = e.Location;
            this.Invalidate();
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
            this.MouseDoubleClick += new MouseEventHandler(DisassemblyDisplay_MouseDoubleClick);
            EnableUpdates = true;
        }

        void DisassemblyDisplay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ushort address = SelectedAddress;
            int offset = e.Y / (TextRenderer.MeasureText("0000", this.Font).Height + 2);
            int index = 0;
            while (offset != 0)
            {
                if (!Disassembly[index].IsLabel)
                    address += CPU.InstructionLength(address);
                index++;
                offset--;
            }
            if (CPU.Breakpoints.Contains(address))
                CPU.Breakpoints.Remove(address);
            else
                CPU.Breakpoints.Add(address);
            this.Invalidate();
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
            bool setLast = false, dark = SelectedAddress % 2 == 0;

            for (int y = 0; y < this.Height; y += TextRenderer.MeasureText("0000", this.Font).Height + 2)
            {
                string address = Debugger.GetHexString(Disassembly[index].Address, 4) + ": ";
                Brush foreground = Brushes.Black;
                if (dark)
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 230, 230, 230)), new Rectangle(0, y, this.Width, TextRenderer.MeasureText(address, this.Font).Height + 2));
                dark = !dark;

                if (CPU.Breakpoints.Contains(Disassembly[index].Address))
                {
                    e.Graphics.FillRectangle(Brushes.DarkRed, new Rectangle(0, y, this.Width, TextRenderer.MeasureText(address, this.Font).Height + 2));
                    foreground = Brushes.White;
                }
                if (Disassembly[index].Address == CPU.PC)
                {
                    if (CPU.Breakpoints.Contains(Disassembly[index].Address))
                    {
                        if (Disassembly[index].IsLabel)
                            e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(0, y + 2, this.Width, TextRenderer.MeasureText(address, this.Font).Height));
                        else
                        {
                            if (index != 0)
                            {
                                if (Disassembly[index - 1].IsLabel)
                                    e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(0, y, this.Width, TextRenderer.MeasureText(address, this.Font).Height));
                                else
                                    e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(0, y + 2, this.Width, TextRenderer.MeasureText(address, this.Font).Height - 2));
                            }
                            else
                                e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(0, y + 2, this.Width, TextRenderer.MeasureText(address, this.Font).Height - 2));
                        }
                    }
                    else
                        e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(0, y, this.Width, TextRenderer.MeasureText(address, this.Font).Height + 2));
                    foreground = Brushes.Black;
                }

                e.Graphics.DrawString(address, this.Font, Brushes.Gray, 2, y);

                if (!Debugger.KnownCode.ContainsKey(Disassembly[index].Address) || Disassembly[index].IsLabel)
                    e.Graphics.DrawString(Disassembly[index].Code, this.Font, foreground, 2 + TextRenderer.MeasureText(address, this.Font).Width + 3, y);
                else
                    e.Graphics.DrawString(Debugger.KnownCode[Disassembly[index].Address], this.Font, foreground,
                        2 + TextRenderer.MeasureText(address, this.Font).Width + 3, y);

                if (y + TextRenderer.MeasureText(address, this.Font).Height > this.Height)
                {
                    setLast = true;
                    EndAddress = Disassembly[index].Address;
                }

                index++;
            }
            if (!setLast)
                EndAddress = Disassembly[index--].Address;
            index = 0;
            if (IsMouseWithin && !CPU.IsRunning) // TODO: Make this more versatile, probably integrate with organic
            {
                int x = MouseLocation.X;
                for (int y = 0; y < this.Height; y += TextRenderer.MeasureText("0000", this.Font).Height + 2)
                {
                    if (Disassembly[index].IsLabel || Disassembly[index].Code.StartsWith("DAT"))
                    {
                        index++;
                        continue;
                    }
                    Size size = TextRenderer.MeasureText("0000: " + Disassembly[index].Code, this.Font);
                    size.Width += 5;
                    if (new Rectangle(new Point(0, y), size).IntersectsWith(
                        new Rectangle(new Point(x, MouseLocation.Y), new Size(1, 1))))
                    {
                        ushort oldPC = CPU.PC;
                        ushort oldSP = CPU.SP;
                        CPU.PC = Disassembly[index].Address;
                        int valueA = 0, valueB = 0;
                        ushort valueBcalc = CPU.Get(Disassembly[index].ValueB);
                        ushort valueAcalc = CPU.Get(Disassembly[index].ValueA);
                        if (Disassembly[index].Opcode == 0)
                        {
                            valueB = int.MaxValue;
                            valueA = TextRenderer.MeasureText("0000: " + Disassembly[index].OpcodeText + " ", this.Font).Width + 4;
                        }
                        else
                        {
                            valueB = TextRenderer.MeasureText("0000: " + Disassembly[index].OpcodeText + " ", this.Font).Width + 4;
                            valueA = valueB + TextRenderer.MeasureText(Disassembly[index].ValueBText + ", ", this.Font).Width - 8;
                        }
                        if (x >= valueB && x <= valueA)
                        {
                            // hovering over value B
                            if (Disassembly[index].ValueB <= 0x1E)
                            {
                                e.Graphics.FillRectangle(Brushes.LightBlue, new Rectangle(new Point(valueB, y),
                                    TextRenderer.MeasureText(Disassembly[index].ValueBText, this.Font)));
                                e.Graphics.DrawString(Disassembly[index].ValueBText, this.Font, Brushes.Black, new PointF(valueB, y));
                                int locationY = y + size.Height;
                                if (this.Height / 2 < y)
                                    locationY = y - size.Height;
                                string text = Disassembly[index].ValueBText + " = 0x" + Debugger.GetHexString(valueBcalc, 4);
                                Size hoverSize = TextRenderer.MeasureText(text, this.Font);
                                e.Graphics.FillRectangle(Brushes.LightBlue, new Rectangle(new Point(
                                    (valueB + (TextRenderer.MeasureText(Disassembly[index].ValueBText, this.Font).Width / 2)) -
                                    (hoverSize.Width / 2), locationY), hoverSize));
                                e.Graphics.DrawString(text, this.Font, Brushes.Black, new Point(
                                    (valueB + (TextRenderer.MeasureText(Disassembly[index].ValueBText, this.Font).Width / 2)) -
                                    (hoverSize.Width / 2), locationY));
                            }
                        }
                        else if (x >= valueA)
                        {
                            // hovering over value A
                            if (Disassembly[index].ValueA <= 0x1E)
                            {
                                e.Graphics.FillRectangle(Brushes.LightBlue, new Rectangle(new Point(valueA, y),
                                    TextRenderer.MeasureText(Disassembly[index].ValueAText, this.Font)));
                                e.Graphics.DrawString(Disassembly[index].ValueAText, this.Font, Brushes.Black, new PointF(valueA, y));
                                int locationY = y + size.Height;
                                if (this.Height / 2 < y)
                                    locationY = y - size.Height;
                                string text = Disassembly[index].ValueAText + " = 0x" + Debugger.GetHexString(valueAcalc, 4);
                                Size hoverSize = TextRenderer.MeasureText(text, this.Font);
                                e.Graphics.FillRectangle(Brushes.LightBlue, new Rectangle(new Point(
                                    (valueA + (TextRenderer.MeasureText(Disassembly[index].ValueAText, this.Font).Width / 2)) -
                                    (hoverSize.Width / 2), locationY), hoverSize));
                                e.Graphics.DrawString(text, this.Font, Brushes.Black, new Point(
                                    (valueA + (TextRenderer.MeasureText(Disassembly[index].ValueAText, this.Font).Width / 2)) -
                                    (hoverSize.Width / 2), locationY));
                            }
                        }
                        CPU.PC = oldPC;
                        CPU.SP = oldSP;
                        break;
                    }
                    index++;
                }
            }
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        private void gotoAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToAddressForm gtaf = new GoToAddressForm(SelectedAddress);
            if (gtaf.ShowDialog() == DialogResult.OK)
            {
                SelectedAddress = gtaf.Value;
                this.Invalidate();
            }
        }
    }
}
