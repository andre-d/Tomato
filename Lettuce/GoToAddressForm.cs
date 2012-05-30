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
        public ushort Value { get; set; }

        public GoToAddressForm(ushort initialValue)
        {
            InitializeComponent();
            textBox1.Text = Debugger.GetHexString(initialValue, 4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ushort outValue;
            if (!ushort.TryParse(textBox1.Text, out outValue))
            {
                foreach (var kvp in Debugger.KnownLabels)
                {
                    if (kvp.Value.ToLower() == textBox1.Text.ToLower())
                    {
                        Value = kvp.Key;
                        DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }
                }
                MessageBox.Show("Unable to parse value.");
                return;
            }
            Value = outValue;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
