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
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class HighestValueIndicatorTest
    {

        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            data = new MockTimeSeries(1, 2, 3, 4, 3, 4, 5, 6, 4, 3, 3, 4, 3, 2);
        }

        [TestMethod]
        public void highestValueUsingTimeFrame5UsingClosePrice()
        {
            HighestValueIndicator highestValue = new HighestValueIndicator(new ClosePriceIndicator(data), 5);

            Assert.AreEqual(highestValue.GetValue(4), 4M);
            Assert.AreEqual(highestValue.GetValue(5), 4M);
            Assert.AreEqual(highestValue.GetValue(6), 5M);
            Assert.AreEqual(highestValue.GetValue(7), 6M);
            Assert.AreEqual(highestValue.GetValue(8), 6M);
            Assert.AreEqual(highestValue.GetValue(9), 6M);
            Assert.AreEqual(highestValue.GetValue(10), 6M);
            Assert.AreEqual(highestValue.GetValue(11), 6M);
            Assert.AreEqual(highestValue.GetValue(12), 4M);
        }

        [TestMethod]
        public void firstHighestValueIndicatorValueShouldBeEqualsToFirstDataValue()
        {
            HighestValueIndicator highestValue = new HighestValueIndicator(new ClosePriceIndicator(data), 5);
            Assert.AreEqual(highestValue.GetValue(0), 1M);
        }

        [TestMethod]
        public void highestValueIndicatorWhenTimeFrameIsGreaterThanIndex()
        {
            HighestValueIndicator highestValue = new HighestValueIndicator(new ClosePriceIndicator(data), 500);
            Assert.AreEqual(highestValue.GetValue(12), 6M);
        }

        [TestMethod]
        public void onlyNaNValues()
        {
            List<IBar> bars = new List<IBar>();
            for (long i = 0; i <= 10000; i++)
            {
                IBar bar = new BaseBar(DateTime.Now.AddDays(i), Decimals.NaN, Decimals.NaN, Decimals.NaN, Decimals.NaN, Decimals.NaN);
                bars.Add(bar);
            }

            BaseTimeSeries series = new BaseTimeSeries("NaN test", bars);
            HighestValueIndicator highestValue = new HighestValueIndicator(new ClosePriceIndicator(series), 5);
            for (int i = series.GetBeginIndex(); i <= series.GetEndIndex(); i++)
            {
                Assert.AreEqual(Decimals.NaN.ToString(), highestValue.GetValue(i).ToString());
            }
        }

        [TestMethod]
        public void naNValuesInIntervall()
        {
            List<IBar> bars = new List<IBar>();
            for (long i = 0; i <= 10; i++)
            { // (0, NaN, 2, NaN, 3, NaN, 4, NaN, 5, ...)
                decimal closePrice = i % 2 == 0 ? i : Decimals.NaN;
                IBar bar = new BaseBar(DateTime.Now.AddDays(i), Decimals.NaN, Decimals.NaN, Decimals.NaN, closePrice, Decimals.NaN);
                bars.Add(bar);
            }

            BaseTimeSeries series = new BaseTimeSeries("NaN test", bars);
            HighestValueIndicator highestValue = new HighestValueIndicator(new ClosePriceIndicator(series), 2);

            // index is the biggest of (index, index-1)
            for (int i = series.GetBeginIndex(); i <= series.GetEndIndex(); i++)
            {
                if (i % 2 != 0) // current is NaN take the previous as highest
                    Assert.AreEqual(series.GetBar(i - 1).ClosePrice.ToString(), highestValue.GetValue(i).ToString());
                else // current is not NaN but previous, take the current
                    Assert.AreEqual(series.GetBar(i).ClosePrice.ToString(), highestValue.GetValue(i).ToString());
            }
        }
    }
}
