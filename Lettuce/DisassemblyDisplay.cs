using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tomato;
using System.Threading;

namespace Lettuce
{
    public partial class DisassemblyDisplay : UserControl
    {
        public ushort SelectedAddress { get; set; }
        public DCPU CPU { get; set; }
        public ushort EndAddress { get; set; }
        private Point MouseLocation;
        private bool IsMouseWithin;
        //public event EventHandler

        public DisassemblyDisplay()
        {
            this.CPU = new DCPU();
            InitializeComponent();
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
                if (!RuntimeInfo.IsMono)
                    ((HandledMouseEventArgs)e).Handled = true;
                if (e.Delta > 0)
                    SelectedAddress--;
                else if (e.Delta < 0)
                    SelectedAddress += CPU.InstructionLength(SelectedAddress);
                this.Invalidate();
                base.OnMouseWheel(e);
            }
        }

        public DisassemblyDisplay(ref DCPU CPU) : this()
        {
            InitializeComponent();
            this.CPU = CPU;
            this.MouseMove += new MouseEventHandler(DisassemblyDisplay_MouseMove);
            this.MouseDoubleClick += new MouseEventHandler(DisassemblyDisplay_MouseDoubleClick);
            EnableUpdates = true;
        }

        void DisassemblyDisplay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Font font = new Font(FontFamily.GenericMonospace, 12);
            ushort address = SelectedAddress;
            int offset = e.Y / (TextRenderer.MeasureText("0000", font).Height + 2);
            int index = 0;
            while (offset != 0)
            {
                if (!Disassembly[index].IsLabel)
                    address += CPU.InstructionLength(address);
                index++;
                offset--;
            }
            if (CPU.Breakpoints.Where(b => b.Address == address).Count() != 0)
                CPU.Breakpoints.Remove(CPU.Breakpoints.Where(b => b.Address == address).First());
            else
                CPU.Breakpoints.Add(new Breakpoint() { Address = address });
            this.Invalidate();
        }

        static Color[] PriorToPC;
        List<CodeEntry> Disassembly;
        public bool EnableUpdates { get; set; }

        private void DisassemblyDisplay_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font(FontFamily.GenericMonospace, 12);

            e.Graphics.FillRectangle(Brushes.White, this.ClientRectangle);
            if (this.DesignMode)
            {
                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                return;
            }

            FastDisassembler disassembler = new FastDisassembler(Debugger.KnownLabels);
            Disassembly = disassembler.FastDisassemble(ref CPU.Memory, SelectedAddress, (ushort)(SelectedAddress + 100));

            int index = 0;
            bool setLast = false, dark = SelectedAddress % 2 == 0;

            for (int y = 0; y < this.Height; y += TextRenderer.MeasureText("0000", font).Height + 2)
            {
                string address = Debugger.GetHexString(Disassembly[index].Address, 4) + ": ";
                Brush foreground = Brushes.Black;
                if (dark)
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 230, 230, 230)), new Rectangle(0, y, this.Width, TextRenderer.MeasureText(address, font).Height + 2));
                dark = !dark;

                if (CPU.Breakpoints.Where(b => b.Address == Disassembly[index].Address).Count() != 0)
                {
                    e.Graphics.FillRectangle(Brushes.DarkRed, new Rectangle(0, y, this.Width, TextRenderer.MeasureText(address, font).Height + 2));
                    foreground = Brushes.White;
                }
                if (Disassembly[index].Address == CPU.PC)
                {
                    if (CPU.Breakpoints.Where(b => b.Address == Disassembly[index].Address).Count() != 0)
                    {
                        if (Disassembly[index].IsLabel)
                            e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(0, y + 2, this.Width, TextRenderer.MeasureText(address, font).Height));
                        else
                        {
                            if (index != 0)
                            {
                                if (Disassembly[index - 1].IsLabel)
                                    e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(0, y, this.Width, TextRenderer.MeasureText(address, font).Height));
                                else
                                    e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(0, y + 2, this.Width, TextRenderer.MeasureText(address, font).Height - 2));
                            }
                            else
                                e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(0, y + 2, this.Width, TextRenderer.MeasureText(address, font).Height - 2));
                        }
                    }
                    else
                        e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(0, y, this.Width, TextRenderer.MeasureText(address, font).Height + 2));
                    foreground = Brushes.Black;
                }

                e.Graphics.DrawString(address, font, Brushes.Gray, 2, y);

                if (!Debugger.KnownCode.ContainsKey(Disassembly[index].Address) || Disassembly[index].IsLabel)
                    e.Graphics.DrawString(Disassembly[index].Code, font, foreground, 2 + TextRenderer.MeasureText(address, font).Width + 3, y);
                else
                    e.Graphics.DrawString(Debugger.KnownCode[Disassembly[index].Address], font, foreground,
                        2 + TextRenderer.MeasureText(address, font).Width + 3, y);

                if (y + TextRenderer.MeasureText(address, font).Height > this.Height)
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
                for (int y = 0; y < this.Height; y += TextRenderer.MeasureText("0000", font).Height + 2)
                {
                    if (Disassembly[index].IsLabel || Disassembly[index].Code.StartsWith("DAT"))
                    {
                        index++;
                        continue;
                    }
                    Size size = TextRenderer.MeasureText("0000: " + Disassembly[index].Code, font);
                    size.Width += 5;
                    if (new Rectangle(new Point(0, y), size).IntersectsWith(
                        new Rectangle(new Point(x, MouseLocation.Y), new Size(1, 1))))
                    {
                        ushort oldPC = CPU.PC;
                        ushort oldSP = CPU.SP;
                        CPU.PC = (ushort)(Disassembly[index].Address + 1);
                        int valueA = 0, valueB = 0;
                        ushort valueAcalc = CPU.Get(Disassembly[index].ValueA);
                        ushort valueBcalc = CPU.Get(Disassembly[index].ValueB);
                        if (Disassembly[index].Opcode == 0)
                        {
                            valueB = int.MaxValue;
                            valueA = TextRenderer.MeasureText("0000: " + Disassembly[index].OpcodeText + " ", font).Width + 4;
                        }
                        else
                        {
                            valueB = TextRenderer.MeasureText("0000: " + Disassembly[index].OpcodeText + " ", font).Width + 4;
                            valueA = valueB + TextRenderer.MeasureText(Disassembly[index].ValueBText + ", ", font).Width - 8;
                        }
                        if (x >= valueB && x <= valueA)
                        {
                            // hovering over value B
                            if (Disassembly[index].ValueB <= 0x1E)
                            {
                                e.Graphics.FillRectangle(Brushes.LightBlue, new Rectangle(new Point(valueB, y),
                                    TextRenderer.MeasureText(Disassembly[index].ValueBText, font)));
                                e.Graphics.DrawString(Disassembly[index].ValueBText, font, Brushes.Black, new PointF(valueB, y));
                                int locationY = y + size.Height;
                                if (this.Height / 2 < y)
                                    locationY = y - size.Height;
                                string text = Disassembly[index].ValueBText + " = 0x" + Debugger.GetHexString(valueBcalc, 4);
                                Size hoverSize = TextRenderer.MeasureText(text, font);
                                e.Graphics.FillRectangle(Brushes.LightBlue, new Rectangle(new Point(
                                    (valueB + (TextRenderer.MeasureText(Disassembly[index].ValueBText, font).Width / 2)) -
                                    (hoverSize.Width / 2), locationY), hoverSize));
                                e.Graphics.DrawString(text, font, Brushes.Black, new Point(
                                    (valueB + (TextRenderer.MeasureText(Disassembly[index].ValueBText, font).Width / 2)) -
                                    (hoverSize.Width / 2), locationY));
                            }
                        }
                        else if (x >= valueA)
                        {
                            // hovering over value A
                            if (Disassembly[index].ValueA <= 0x1E)
                            {
                                e.Graphics.FillRectangle(Brushes.LightBlue, new Rectangle(new Point(valueA, y),
                                    TextRenderer.MeasureText(Disassembly[index].ValueAText, font)));
                                e.Graphics.DrawString(Disassembly[index].ValueAText, font, Brushes.Black, new PointF(valueA, y));
                                int locationY = y + size.Height;
                                if (this.Height / 2 < y)
                                    locationY = y - size.Height;
                                string text = Disassembly[index].ValueAText + " = 0x" + Debugger.GetHexString(valueAcalc, 4);
                                Size hoverSize = TextRenderer.MeasureText(text, font);
                                e.Graphics.FillRectangle(Brushes.LightBlue, new Rectangle(new Point(
                                    (valueA + (TextRenderer.MeasureText(Disassembly[index].ValueAText, font).Width / 2)) -
                                    (hoverSize.Width / 2), locationY), hoverSize));
                                e.Graphics.DrawString(text, font, Brushes.Black, new Point(
                                    (valueA + (TextRenderer.MeasureText(Disassembly[index].ValueAText, font).Width / 2)) -
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
