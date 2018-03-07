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
namespace TA4Net.Test.Indicators.helpers
{

    using TA4Net;
    using TA4Net.Indicators;
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class PreviousValueIndicatorTest
    {

        private PreviousValueIndicator prevValueIndicator;

        private OpenPriceIndicator openPriceIndicator;
        private MinPriceIndicator minPriceIndicator;
        private MaxPriceIndicator maxPriceIndicator;

        private VolumeIndicator volumeIndicator;
        private EMAIndicator emaIndicator;

        private ITimeSeries series;

        [TestInitialize]
        public void setUp()
        {
            Random r = new Random();
            List<IBar> bars = new List<IBar>();
            for (int i = 0; i < 1000; i++)
            {
                double open = r.NextDouble();
                double close = r.NextDouble();
                double max = Math.Max(close + r.NextDouble(), open + r.NextDouble());
                double min = Math.Min(0, Math.Min(close - r.NextDouble(), open - r.NextDouble()));
                DateTime dateTime = DateTime.Now;
                IBar bar = new BaseBar(dateTime, (decimal)open, (decimal)close, (decimal)max, (decimal)min, i);
                bars.Add(bar);
            }
            this.series = new BaseTimeSeries("test", bars);

            this.openPriceIndicator = new OpenPriceIndicator(this.series);
            this.minPriceIndicator = new MinPriceIndicator(this.series);
            this.maxPriceIndicator = new MaxPriceIndicator(this.series);
            this.volumeIndicator = new VolumeIndicator(this.series);
            ClosePriceIndicator closePriceIndicator = new ClosePriceIndicator(this.series);
            this.emaIndicator = new EMAIndicator(closePriceIndicator, 20);
        }

        [TestMethod]
        public void shouldBePreviousValueFromIndicator()
        {

            //test 1 with openPrice-indicator
            prevValueIndicator = new PreviousValueIndicator(openPriceIndicator);
            Assert.AreEqual(prevValueIndicator.GetValue(0), openPriceIndicator.GetValue(0));
            for (int i = 1; i < this.series.GetBarCount(); i++)
            {
                Assert.AreEqual(prevValueIndicator.GetValue(i), openPriceIndicator.GetValue(i - 1));
            }

            //test 2 with minPrice-indicator
            prevValueIndicator = new PreviousValueIndicator(minPriceIndicator);
            Assert.AreEqual(prevValueIndicator.GetValue(0), minPriceIndicator.GetValue(0));
            for (int i = 1; i < this.series.GetBarCount(); i++)
            {
                Assert.AreEqual(prevValueIndicator.GetValue(i), minPriceIndicator.GetValue(i - 1));
            }

            //test 3 with maxPrice-indicator
            prevValueIndicator = new PreviousValueIndicator(maxPriceIndicator);
            Assert.AreEqual(prevValueIndicator.GetValue(0), maxPriceIndicator.GetValue(0));
            for (int i = 1; i < this.series.GetBarCount(); i++)
            {
                Assert.AreEqual(prevValueIndicator.GetValue(i), maxPriceIndicator.GetValue(i - 1));
            }
        }

        [TestMethod]
        public void shouldBeNthPreviousValueFromIndicator()
        {
            for (int i = 0; i < this.series.GetBarCount(); i++)
            {
                testWithN(i);
            }
        }

        private void testWithN(int n)
        {

            // test 1 with volume-indicator
            prevValueIndicator = new PreviousValueIndicator(volumeIndicator, n);
            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(prevValueIndicator.GetValue(i), volumeIndicator.GetValue(0));
            }
            for (int i = n; i < this.series.GetBarCount(); i++)
            {
                Assert.AreEqual(prevValueIndicator.GetValue(i), volumeIndicator.GetValue(i - n));
            }

            // test 2 with ema-indicator
            prevValueIndicator = new PreviousValueIndicator(emaIndicator, n);
            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(prevValueIndicator.GetValue(i), emaIndicator.GetValue(0));
            }
            for (int i = n; i < this.series.GetBarCount(); i++)
            {
                Assert.AreEqual(prevValueIndicator.GetValue(i), emaIndicator.GetValue(i - n));
            }
        }
    }
}