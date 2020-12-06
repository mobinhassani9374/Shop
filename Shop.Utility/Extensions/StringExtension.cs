using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Utility.Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
    }
}
