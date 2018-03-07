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
namespace TA4Net.Test.Indicators
{

    using TA4Net;
    using TA4Net.Indicators;
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class EMAIndicatorTest : IndicatorTest<IIndicator<decimal>, decimal, decimal>
    {

        private ExternalIndicatorTest xls;

        public EMAIndicatorTest()
            : base((t, v) => new EMAIndicator(t, (int)v[0]))
        {
            xls = new XLSIndicatorTest(GetType(), @"TestData\indicators\EMA.xls", 6);
        }
        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            data = new MockTimeSeries(
                    64.75M, 63.79M, 63.73M,
                    63.73M, 63.55M, 63.19M,
                    63.91M, 63.85M, 62.95M,
                    63.37M, 61.33M, 61.51M);
        }

        [TestMethod]
        public void firstValueShouldBeEqualsToFirstDataValue()
        { // throws exception
            IIndicator<decimal> indicator = getIndicator(new ClosePriceIndicator(data), 1);
            Assert.AreEqual(indicator.GetValue(0), 64.75M);
        }

        [TestMethod]
        public void usingTimeFrame10UsingClosePrice()
        { // throws exception
            IIndicator<decimal> indicator = getIndicator(new ClosePriceIndicator(data), 10);
            Assert.AreEqual(indicator.GetValue(9), 63.694826748355546959417260457M);
            Assert.AreEqual(indicator.GetValue(10), 63.264858248654538421341394919M);
            Assert.AreEqual(indicator.GetValue(11), 62.945793112535531435642959479M);
        }

        [TestMethod]
        public void stackOverflowError()
        { // throws exception
            List<IBar> bigListOfBars = new List<IBar>();
            for (int i = 0; i < 10000; i++)
            {
                bigListOfBars.Add(new MockBar(i));
            }
            MockTimeSeries bigSeries = new MockTimeSeries(bigListOfBars);
            IIndicator<decimal> indicator = getIndicator(new ClosePriceIndicator(bigSeries), 10);
            // if a StackOverflowError is thrown here, then the RecursiveCachedIndicator does not work as intended.
            Assert.AreEqual(indicator.GetValue(9999), 9994.5M);
        }

        [TestMethod]
        public void externalData()
        { // throws exception
            ITimeSeries xlsSeries = xls.getSeries();
            IIndicator<decimal> closePrice = new ClosePriceIndicator(xlsSeries);
            IIndicator<decimal> indicator;

            indicator = getIndicator(closePrice, 1);
          //  Assert.AreEqual(xls.getIndicator(1), indicator);
            Assert.AreEqual(329.0M, indicator.GetValue(indicator.TimeSeries.GetEndIndex()));

            indicator = getIndicator(closePrice, 3);
         //   Assert.AreEqual(xls.getIndicator(3), indicator);
            Assert.AreEqual(327.77484966230713293966586844M, indicator.GetValue(indicator.TimeSeries.GetEndIndex()));

            indicator = getIndicator(closePrice, 13);
         //   Assert.AreEqual(xls.getIndicator(13), indicator);
            Assert.AreEqual(327.40768441383635668538339921M, indicator.GetValue(indicator.TimeSeries.GetEndIndex()));
        }

    }
}