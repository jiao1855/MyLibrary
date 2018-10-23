using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSBC.INS.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string str)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}
