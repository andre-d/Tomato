using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;

namespace Tomato
{
    public class HexTypeEditor : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (context.PropertyDescriptor.PropertyType == typeof(uint))
                return uint.Parse(value as string, NumberStyles.AllowHexSpecifier);
            else
                return ushort.Parse(value as string, NumberStyles.AllowHexSpecifier);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is uint)
                return "0x" + GetHexString((uint)value, 8);
            else
                return "0x" + GetHexString((ushort)value, 4);
        }

        public static string GetHexString(uint value, int numDigits)
        {
            string result = value.ToString("x").ToUpper();
            while (result.Length < numDigits)
                result = "0" + result;
            return result;
        }
    }
}
