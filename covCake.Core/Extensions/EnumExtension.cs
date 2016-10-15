using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace covCake
{
    //[DebuggerStepThrough]
    public static class EnumExtension
    {
        public static string GetValue(this Enum value)
        {
            return Enum.Format(value.GetType(), value, "D");
        }
    }
}
