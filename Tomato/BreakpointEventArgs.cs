using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tomato
{
    public class BreakpointEventArgs : EventArgs
    {
        public bool ContinueExecution;

        public BreakpointEventArgs()
        {
            ContinueExecution = false;
        }
    }
}
