using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tomato.Hardware;
using System.Globalization;

namespace Lettuce.HardwareControllers
{
    public partial class LEM1802Controller : DeviceController
    {
        public LEM1802Controller()
        {
            InitializeComponent();
            this.Shown += new EventHandler(LEM1802Controller_Shown);
            this.TargetType = typeof(LEM1802);
        }

        void LEM1802Controller_Shown(object sender, EventArgs e)
        {
            deviceIndexLabel.Text += CPU.Devices.IndexOf(Device);
            hardwareIdLabel.Text += Debugger.GetHexString(Device.DeviceID, 8);
            manufacturerIdLabel.Text += Debugger.GetHexString(Device.ManufacturerID, 8);
            versionLabel.Text += Debugger.GetHexString(Device.Version, 4);
            LEM1802 lem = (Device as LEM1802);
            lcdMapTextBox.Text = Debugger.GetHexString(lem.ScreenMap, 4);
            fontMapTextBox.Text = Debugger.GetHexString(lem.FontMap, 4);
            paletteMapTextBox.Text = Debugger.GetHexString(lem.PaletteMap, 4);
            borderColorTextBox.Text = Debugger.GetHexString(lem.BorderColorValue, 4);
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

        private void lcdMapTextBox_TextChanged(object sender, EventArgs e)
        {
            (Device as LEM1802).ScreenMap = ushort.Parse(lcdMapTextBox.Text, NumberStyles.HexNumber);
        }

        private void fontMapTextBox_TextChanged(object sender, EventArgs e)
        {
            (Device as LEM1802).FontMap = ushort.Parse(fontMapTextBox.Text, NumberStyles.HexNumber);
        }

        private void paletteMapTextBox_TextChanged(object sender, EventArgs e)
        {
            (Device as LEM1802).PaletteMap = ushort.Parse(paletteMapTextBox.Text, NumberStyles.HexNumber);
        }

        private void borderColorTextBox_TextChanged(object sender, EventArgs e)
        {
            (Device as LEM1802).BorderColorValue = ushort.Parse(borderColorTextBox.Text, NumberStyles.HexNumber);
        }
    }
}
