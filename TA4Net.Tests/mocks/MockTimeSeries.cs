/*
  The MIT License (MIT)

  Copyright (c) 2014-2017 Marc de Verdelhan & respective authors (see AUTHORS)

  Permission is hereby granteM, free of charge, to any person obtaining a copy of
  this software and associated documentation files (the "Software"), to deal in
  the Software without restriction, including without limitation the rights to
  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
  the Software, and to permit persons to whom the Software is furnished to do so,
  subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KINM, EXPRESS OR
  IMPLIEM, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
  FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
  COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
  IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
  CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TA4Net;
using TA4Net.Interfaces;

namespace TA4Net.Mocks
{
    /**
     * A time series with sample data.
     */
    [TestClass]
    public class MockTimeSeries : BaseTimeSeries
    {

        public MockTimeSeries(params decimal[] data)
            : base(decimalsToBars(data))
        {
        }

        public MockTimeSeries(List<IBar> bars)
            : base(bars)
        {
        }

        public MockTimeSeries(decimal[] data, DateTime[] times)
            : base(doublesAndTimesToBars(data, times))
        {
        }

        public MockTimeSeries(params DateTime[] dates)
            : base(timesToBars(dates))
        {
        }

        public MockTimeSeries()
            : base(arbitraryBars())
        {
        }

        private static List<IBar> decimalsToBars(params decimal[] data)
        {
            List<IBar> bars = new List<IBar>();
            for (int i = 0; i < data.Length; i++)
            {
                bars.Add(new MockBar(DateTime.Now.AddSeconds(i), data[i]));
            }
            return bars;
        }

        private static List<IBar> doublesAndTimesToBars(decimal[] data, DateTime[] times)
        {
            if (data.Length != times.Length)
            {
                throw new ArgumentException();
            }
            List<IBar> bars = new List<IBar>();
            for (int i = 0; i < data.Length; i++)
            {
                bars.Add(new MockBar(times[i], data[i]));
            }
            return bars;
        }

        private static List<IBar> timesToBars(params DateTime[] dates)
        {
            List<IBar> bars = new List<IBar>();
            int i = 1;
            foreach (DateTime date in dates)
            {
                bars.Add(new MockBar(date, i++));
            }
            return bars;
        }

        private static List<IBar> arbitraryBars()
        {
            List<IBar> bars = new List<IBar>();
            for (decimal i = 0M; i < 5000; i++)
            {
                bars.Add(new MockBar(DateTime.Now, i, i + 1, i + 2, i + 3, i + 4, i + 5, (int)(i + 6)));
            }
            return bars;
        }
    }
}
