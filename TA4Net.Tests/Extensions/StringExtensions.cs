using System;
using System.Globalization;

namespace TA4Net.Tests.Extensions
{
    public static class StringExtensions
    {
        public static Decimal ToDecimal(this string value)
        {
            return decimal.Parse(value, new NumberFormatInfo() { NumberDecimalSeparator = "." });
        }
    }
}
