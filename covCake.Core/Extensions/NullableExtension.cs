using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace covCake
{
    public static class IntExtension
    {
        public static string ToString(this int? val)
        {
            return (val.HasValue)? val.Value.ToString() : "";
        }
    }
}
