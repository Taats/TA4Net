using System;
using System.Collections.Generic;
using System.Text;

namespace TA4Net.Test.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime withDayOfMonth(this DateTime current, int day)
        {
            return new DateTime(current.Year, current.Month, day, current.Hour, current.Minute, current.Second);
        }

        public static DateTime withMinute(this DateTime current, int minutes)
        {
            return new DateTime(current.Year, current.Month, current.Day, current.Hour, minutes, current.Second);
        }

        //public static DateTime withDayOfMonth(this DateTime current, int day)
        //{
        //    return new DateTime(current.Year, current.Month, day, current.Hour, current.Minute, current.Second);
        //}
    }
}
