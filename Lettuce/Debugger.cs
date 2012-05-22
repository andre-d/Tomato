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
using System.Globalization;

namespace Lettuce
{
    public partial class Debugger : Form
    {
        public DCPU CPU { get; set; }
        private bool MayUpdateLayout = true;

        public Debugger(ref DCPU CPU)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.CPU = CPU;
            this.rawMemoryDisplay.CPU = this.CPU;
            foreach (Device d in CPU.ConnectedDevices)
                listBoxConnectedDevices.Items.Add(d.FriendlyName);
        }

        public static string GetHexString(uint value, int numDigits)
        {
            string result = value.ToString("x").ToUpper();
            while (result.Length < numDigits)
                result = "0" + result;
            return result;
        }

        delegate void ResetLayoutDelegate();
        public void ResetLayout()
        {
            if (this.IsDisposed || this.Disposing)
                return;
            if (this.InvokeRequired)
            {
                try
                {
                    ResetLayoutDelegate rld = new ResetLayoutDelegate(ResetLayout);
                    this.Invoke(rld);
                }
                catch { }
            }
            else
            {
                if (!MayUpdateLayout)
                    return;
                MayUpdateLayout = false;
                textBoxRegisterA.Text = GetHexString(CPU.A, 4);
                textBoxRegisterB.Text = GetHexString(CPU.B, 4);
                textBoxRegisterC.Text = GetHexString(CPU.C, 4);
                textBoxRegisterX.Text = GetHexString(CPU.X, 4);
                textBoxRegisterY.Text = GetHexString(CPU.Y, 4);
                textBoxRegisterZ.Text = GetHexString(CPU.Z, 4);
                textBoxRegisterI.Text = GetHexString(CPU.I, 4);
                textBoxRegisterJ.Text = GetHexString(CPU.J, 4);
                textBoxRegisterPC.Text = GetHexString(CPU.PC, 4);
                textBoxRegisterSP.Text = GetHexString(CPU.SP, 4);
                textBoxRegisterEX.Text = GetHexString(CPU.EX, 4);
                textBoxRegisterIA.Text = GetHexString(CPU.IA, 4);
                checkBoxRunning.Checked = CPU.IsRunning;
                checkBoxInterruptQueue.Checked = CPU.InterruptQueueEnabled;
                checkBoxOnFire.Checked = CPU.IsOnFire;
                rawMemoryDisplay.Invalidate();
                if (CPU.IsRunning)
                    DisableAll();
                else
                    EnableAll();
                MayUpdateLayout = true;
            }
        }

        private void DisableAll()
        {
            textBoxRegisterA.Enabled = false;
            textBoxRegisterB.Enabled = false;
            textBoxRegisterC.Enabled = false;
            textBoxRegisterX.Enabled = false;
            textBoxRegisterY.Enabled = false;
            textBoxRegisterZ.Enabled = false;
            textBoxRegisterI.Enabled = false;
            textBoxRegisterJ.Enabled = false;
            textBoxRegisterPC.Enabled = false;
            textBoxRegisterSP.Enabled = false;
            textBoxRegisterEX.Enabled = false;
            textBoxRegisterIA.Enabled = false;
            checkBoxOnFire.Enabled = false;
            checkBoxInterruptQueue.Enabled = false;
            buttonStepInto.Enabled = false;
            buttonStepOver.Enabled = false;
            stopToolStripMenuItem.Text = "Stop";
            rawMemoryDisplay.Enabled = false;
        }

        private void EnableAll()
        {
            textBoxRegisterA.Enabled = true;
            textBoxRegisterB.Enabled = true;
            textBoxRegisterC.Enabled = true;
            textBoxRegisterX.Enabled = true;
            textBoxRegisterY.Enabled = true;
            textBoxRegisterZ.Enabled = true;
            textBoxRegisterI.Enabled = true;
            textBoxRegisterJ.Enabled = true;
            textBoxRegisterPC.Enabled = true;
            textBoxRegisterSP.Enabled = true;
            textBoxRegisterEX.Enabled = true;
            textBoxRegisterIA.Enabled = true;
            checkBoxOnFire.Enabled = true;
            checkBoxInterruptQueue.Enabled = true;
            buttonStepInto.Enabled = true;
            buttonStepOver.Enabled = true;
            stopToolStripMenuItem.Text = "Start";
            rawMemoryDisplay.Enabled = true;
        }

        private void listBoxConnectedDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxConnectedDevices.SelectedIndex == -1)
                return;
            Device selected = CPU.ConnectedDevices[listBoxConnectedDevices.SelectedIndex];
            labelHardwareID.Text = "Hardware ID: " + GetHexString(selected.DeviceID, 8);
            labelManufacturer.Text = "Manufacturer: " + GetHexString(selected.ManufacturerID, 8);
            labelVersion.Text = "Version: " + GetHexString(selected.Version, 4);
        }

        private void checkBoxRunning_CheckedChanged(object sender, EventArgs e)
        {
            if (MayUpdateLayout)
                CPU.IsRunning = !CPU.IsRunning;
            ResetLayout();
        }

        private void textBoxRegisterX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control || e.Alt)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.D1 ||
                e.KeyCode == Keys.D2 ||
                e.KeyCode == Keys.D3 ||
                e.KeyCode == Keys.D4 ||
                e.KeyCode == Keys.D5 ||
                e.KeyCode == Keys.D6 ||
                e.KeyCode == Keys.D7 ||
                e.KeyCode == Keys.D8 ||
                e.KeyCode == Keys.D9 ||
                e.KeyCode == Keys.D0 ||
                e.KeyCode == Keys.NumPad1 ||
                e.KeyCode == Keys.NumPad2 ||
                e.KeyCode == Keys.NumPad3 ||
                e.KeyCode == Keys.NumPad4 ||
                e.KeyCode == Keys.NumPad5 ||
                e.KeyCode == Keys.NumPad6 ||
                e.KeyCode == Keys.NumPad7 ||
                e.KeyCode == Keys.NumPad8 ||
                e.KeyCode == Keys.NumPad9 ||
                e.KeyCode == Keys.NumPad0 ||
                e.KeyCode == Keys.A ||
                e.KeyCode == Keys.B ||
                e.KeyCode == Keys.C ||
                e.KeyCode == Keys.D ||
                e.KeyCode == Keys.E ||
                e.KeyCode == Keys.F ||
                e.KeyCode == Keys.Back ||
                e.KeyCode == Keys.Delete ||
                e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Right)
            {
                return;
            }
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void textBoxRegisterA_TextChanged(object sender, EventArgs e)
        {
            if (textBoxRegisterA.Text.Length != 0)
                CPU.A = ushort.Parse(textBoxRegisterA.Text, NumberStyles.HexNumber);
            rawMemoryDisplay.Invalidate();
        }

        private void textBoxRegisterB_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0)
                CPU.B = ushort.Parse((sender as TextBox).Text, NumberStyles.HexNumber);
            rawMemoryDisplay.Invalidate();
        }

        private void textBoxRegisterC_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0)
                CPU.C = ushort.Parse((sender as TextBox).Text, NumberStyles.HexNumber);
            rawMemoryDisplay.Invalidate();
        }

        private void textBoxRegisterX_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0)
                CPU.X = ushort.Parse((sender as TextBox).Text, NumberStyles.HexNumber);
            rawMemoryDisplay.Invalidate();
        }

        private void textBoxRegisterY_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0)
                CPU.Y = ushort.Parse((sender as TextBox).Text, NumberStyles.HexNumber);
            rawMemoryDisplay.Invalidate();
        }

        private void textBoxRegisterZ_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0)
                CPU.Z = ushort.Parse((sender as TextBox).Text, NumberStyles.HexNumber);
            rawMemoryDisplay.Invalidate();
        }

        private void textBoxRegisterI_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0)
                CPU.I = ushort.Parse((sender as TextBox).Text, NumberStyles.HexNumber);
            rawMemoryDisplay.Invalidate();
        }

        private void textBoxRegisterJ_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0)
                CPU.J = ushort.Parse((sender as TextBox).Text, NumberStyles.HexNumber);
            rawMemoryDisplay.Invalidate();
        }

        private void textBoxRegisterPC_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0)
                CPU.PC = ushort.Parse((sender as TextBox).Text, NumberStyles.HexNumber);
            rawMemoryDisplay.Invalidate();
        }

        private void textBoxRegisterEX_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0)
                CPU.EX = ushort.Parse((sender as TextBox).Text, NumberStyles.HexNumber);
            rawMemoryDisplay.Invalidate();
        }

        private void textBoxRegisterSP_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0)
                CPU.SP = ushort.Parse((sender as TextBox).Text, NumberStyles.HexNumber);
            rawMemoryDisplay.Invalidate();
        }

        private void textBoxRegisterIA_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0)
                CPU.IA = ushort.Parse((sender as TextBox).Text, NumberStyles.HexNumber);
            rawMemoryDisplay.Invalidate();
        }

        private void buttonStepOver_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void buttonStepInto_Click(object sender, EventArgs e)
        {
            CPU.Execute(-1);
            ResetLayout();
        }

        private void Debugger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6) // TODO: Keyboard customization
                buttonStepInto_Click(sender, e);
            if (e.KeyCode == Keys.F7)
                buttonStepOver_Click(sender, e);
            if (e.KeyCode == Keys.F5)
                checkBoxRunning.Checked = !checkBoxRunning.Checked;
            if (e.Control)
            {
                if (e.KeyCode == Keys.G)
                    gotoAddressToolStripMenuItem_Click(sender, e);
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CPU.Reset();
            ResetLayout();
        }

        private void gotoAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rawMemoryDisplay.gotoAddressToolStripMenuItem_Click(sender, e);
        }

        private void resetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CPU.Memory = new ushort[0x10000];
            ResetLayout();
        }
    }
}
