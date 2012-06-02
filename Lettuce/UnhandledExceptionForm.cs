using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Lettuce
{
    public partial class UnhandledExceptionForm : Form
    {
        public Exception Exception { get; set; }

        public UnhandledExceptionForm(Exception Exception, bool IsTerminating)
        {
            InitializeComponent();
            this.Exception = Exception;
            textBox1.Text = Exception.ToString();
            if (IsTerminating)
            {
                button6.Visible = false;
                labelRecover.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(string.Format("mailto:sir@cmpwn.com?subject={0}&body={1}",
                Uri.EscapeUriString("Unhandled Exception: " + Exception.GetType().Name),
                Uri.EscapeUriString(textBox1.Text)));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("http://thenicestplaceontheinter.net");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(string.Format("https://github.com/SirCmpwn/Tomato/issues/new?title={0}&body={1}",
                Uri.EscapeUriString("Unhandled Exception: " + Exception.GetType().Name),
                Uri.EscapeUriString(textBox1.Text)));
        }
    }
}
