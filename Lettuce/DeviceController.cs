using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tomato.Hardware;
using Tomato;

namespace Lettuce
{
    public class DeviceController : Form
    {
        public Device Device { get; set; }
        public DCPU CPU { get; set; }
        public Type TargetType { get; protected set; }
    }
}
