using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TA4Net
{
    public static class TempExtensions
    {
        public static bool isEmpty<T>(this IEnumerable<T> items)
        {
            return !items.Any();
        }
    }
}
