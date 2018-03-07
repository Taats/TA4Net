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
    public class LowestValueIndicatorTest
    {

        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            data = new MockTimeSeries(1, 2, 3, 4, 3, 4, 5, 6, 4, 3, 2, 4, 3, 1);
        }

        [TestMethod]
        public void lowestValueIndicatorUsingTimeFrame5UsingClosePrice()
        {
            LowestValueIndicator lowestValue = new LowestValueIndicator(new ClosePriceIndicator(data), 5);
            Assert.AreEqual(lowestValue.GetValue(1), 1M);
            Assert.AreEqual(lowestValue.GetValue(2), 1M);
            Assert.AreEqual(lowestValue.GetValue(3), 1M);
            Assert.AreEqual(lowestValue.GetValue(4), 1M);
            Assert.AreEqual(lowestValue.GetValue(5), 2M);
            Assert.AreEqual(lowestValue.GetValue(6), 3M);
            Assert.AreEqual(lowestValue.GetValue(7), 3M);
            Assert.AreEqual(lowestValue.GetValue(8), 3M);
            Assert.AreEqual(lowestValue.GetValue(9), 3M);
            Assert.AreEqual(lowestValue.GetValue(10), 2M);
            Assert.AreEqual(lowestValue.GetValue(11), 2M);
            Assert.AreEqual(lowestValue.GetValue(12), 2M);

        }

        [TestMethod]
        public void lowestValueIndicatorValueShouldBeEqualsToFirstDataValue()
        {
            LowestValueIndicator lowestValue = new LowestValueIndicator(new ClosePriceIndicator(data), 5);
            Assert.AreEqual(lowestValue.GetValue(0), 1);
        }

        [TestMethod]
        public void lowestValueIndicatorWhenTimeFrameIsGreaterThanIndex()
        {
            LowestValueIndicator lowestValue = new LowestValueIndicator(new ClosePriceIndicator(data), 500);
            Assert.AreEqual(lowestValue.GetValue(12), 1);
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
            LowestValueIndicator lowestValue = new LowestValueIndicator(new ClosePriceIndicator(series), 5);
            for (int i = series.GetBeginIndex(); i <= series.GetEndIndex(); i++)
            {
                Assert.AreEqual(Decimals.NaN.ToString(), lowestValue.GetValue(i).ToString());
            }
        }

        [TestMethod]
        public void naNValuesInIntervall()
        {
            List<IBar> bars = new List<IBar>();
            for (long i = 0; i <= 10; i++)
            { // (NaN, 1, NaN, 2, NaN, 3, NaN, 4, ...)
                decimal closePrice = i % 2 == 0 ? i : Decimals.NaN;
                IBar bar = new BaseBar(DateTime.Now.AddDays(i), Decimals.NaN, Decimals.NaN, Decimals.NaN, Decimals.NaN, Decimals.NaN);
                bars.Add(bar);
            }

            BaseTimeSeries series = new BaseTimeSeries("NaN test", bars);
            LowestValueIndicator lowestValue = new LowestValueIndicator(new ClosePriceIndicator(series), 2);
            for (int i = series.GetBeginIndex(); i <= series.GetEndIndex(); i++)
            {
                if (i % 2 != 0)
                {
                    Assert.AreEqual(series.GetBar(i - 1).ClosePrice.ToString(), lowestValue.GetValue(i).ToString());
                }
                else
                    Assert.AreEqual(series.GetBar(Math.Max(0, i - 1)).ClosePrice.ToString(), lowestValue.GetValue(i).ToString());
            }
        }
    }
}