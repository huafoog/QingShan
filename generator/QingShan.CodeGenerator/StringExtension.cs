using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class StringExtension
    {
        public static String GetValue(this string[] str,int index,string defult)
        {
            if (str.Length < index + 1)
            {
                return defult;
            }
            return str[index];
        }
    }
}
