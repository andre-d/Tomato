using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Lettuce
{
    public partial class GoToAddressForm : Form
    {
        public ushort Value
        {
            get
            {
                return ushort.Parse(textBox1.Text, NumberStyles.HexNumber);
            }
        }

        public GoToAddressForm(ushort initialValue)
        {
            InitializeComponent();
            textBox1.Text = Debugger.GetHexString(initialValue, 4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
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
    }
}
