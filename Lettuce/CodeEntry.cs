using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inorganic
{
    /// <summary>
    /// Represents a line of disassembled code.
    /// </summary>
    public class CodeEntry
    {
        public string Code { get; set; }
        public byte Opcode { get; set; }
        public byte ValueA { get; set; }
        public byte ValueB { get; set; }
        public ushort Address { get; set; }
    }

    public enum CodeEntryType
    {
        Data,
        Code,
    }
}
