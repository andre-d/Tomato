using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tomato.Hardware;
using System.Reflection;

namespace Lettuce
{
    public partial class HardwareConfiguration : Form
    {
        private List<Device> PossibleDevices;

        public List<Device> SelectedDevices
        {
            get
            {
                List<Device> devices = new List<Device>();
                for (int i = 0; i < hardwareSelectionListBox.Items.Count; i++)
                {
                    if (hardwareSelectionListBox.GetItemChecked(i))
                        devices.Add(PossibleDevices[i]);
                }
                return devices;
            }
        }

        public HardwareConfiguration()
        {
            InitializeComponent();
            PossibleDevices = new List<Device>();
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var types = asm.GetTypes().Where(t => typeof(Device).IsAssignableFrom(t) && t.IsAbstract == false);
                foreach (var type in types)
                {
                    PossibleDevices.Add((Device)Activator.CreateInstance(type));
                }
            }
            foreach (var device in PossibleDevices)
                hardwareSelectionListBox.Items.Add(device.FriendlyName, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
