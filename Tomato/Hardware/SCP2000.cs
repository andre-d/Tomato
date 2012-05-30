using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tomato.Hardware
{
    public class SCP2000 : Device
    {
        public uint UnitToSkip { get; set; }
        public ushort SkipUnit { get; set; }

        public override uint DeviceID
        {
            get { return 0x40e41d9d; }
        }

        public override uint ManufacturerID
        {
            get { return 0x1c6c8b36; }
        }

        public override ushort Version
        {
            get { return 0x005e; }
        }

        public override string FriendlyName
        {
            get { return "Suspension Chamber 2000"; }
        }

        public override int HandleInterrupt()
        {
            switch (AttachedCPU.Memory.A)
            {
                case 0:
                    GetStatus();
                    break;
                case 1:
                    UnitToSkip = (uint)(AttachedCPU.Memory[AttachedCPU.Memory.B] << 16);
                    UnitToSkip |= AttachedCPU.Memory[AttachedCPU.Memory.B + 1];
                    break;
                case 2:
                    GetStatus();
                    if (AttachedCPU.Memory.C == 0) // Error in spec?
                    {
                        // Trigger device
                        // Not sure what to do here.
                    }
                    break;
                case 3:
                    SkipUnit = AttachedCPU.Memory.B;
                    break;
            }
            return 0;
        }

        private void GetStatus()
        {
            AttachedCPU.Memory.C = 0;
            AttachedCPU.Memory.B = 6; // Mechanical error
        }

        public override void Reset()
        {
            //throw new NotImplementedException();
        }
    }
}
