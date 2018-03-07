using System;
using System.Collections.Generic;
using System.Linq;

namespace TA4Net.Constants
{
    public class TimeFrame
    {
        public static TimeFrame OneMinute;

        public int InSeconds { get; }

        public int InMiliseconds { get; }

        private int _MinusSeconds;

        static TimeFrame()
        {
            OneMinute = new TimeFrame(60);
        }

        private TimeFrame(int inSeconds)
        {
            InSeconds = inSeconds;
            InMiliseconds = inSeconds * 1000;
            _MinusSeconds = InSeconds * -1;
        }

        public DateTime GetFromTime()
        {
            return DateTime.UtcNow.AddSeconds(_MinusSeconds);
        }

        public DateTime GetFromTime(int numberOfPeriods)
        {
            return DateTime.UtcNow.AddSeconds(_MinusSeconds * numberOfPeriods);
        }

        public IDictionary<DateTime, T[]> GroupBy<T>(IEnumerable<T> items, Func<T, DateTime> groupProperty)
        {
            var results = new Dictionary<DateTime, T[]>();

            DateTime startWindow = groupProperty(items.First());
            DateTime endWindow = startWindow.AddSeconds(InSeconds);

            var windowResults = new List<T>();

            foreach (var item in items)
            {
                if (endWindow < groupProperty(item))
                {
                    startWindow = endWindow;
                    results.Add(startWindow, windowResults.ToArray());
                    windowResults = new List<T>();
                    endWindow = startWindow.AddSeconds(InSeconds);
                }

                windowResults.Add(item);
            }
            results.Add(startWindow, windowResults.ToArray());
            return results;
        }

    }
}
