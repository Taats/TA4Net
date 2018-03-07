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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class ATRIndicatorTest : IndicatorTest<ITimeSeries, decimal, decimal>
    {

        private ExternalIndicatorTest xls;

        public ATRIndicatorTest()
            : base((t, v) => new ATRIndicator(t, (int)v[0]))
        {
            xls = new XLSIndicatorTest(GetType(), @"TestData\indicators\ATR.xls", 7);
        }

        [TestMethod]
        public void testDummy() // throws exception
        {
            List<IBar> bars = new List<IBar>();
            bars.Add(new MockBar(0, 12, 15, 8));
            bars.Add(new MockBar(0, 8, 11, 6));
            bars.Add(new MockBar(0, 15, 17, 14));
            bars.Add(new MockBar(0, 15, 17, 14));
            bars.Add(new MockBar(0, 0, 0, 2));
            IIndicator<decimal> indicator = getIndicator(new MockTimeSeries(bars), 3);

            Assert.AreEqual(7M, indicator.GetValue(0));
            Assert.AreEqual(6.6666666666666666666666666667M, indicator.GetValue(1)); // 6M / 3 + (1 - 1M / 3) * indicator.getValue(0)
            Assert.AreEqual(7.4444444444444444444444444444M, indicator.GetValue(2)); // 9M / 3 + (1 - 1M / 3) * indicator.getValue(1)
            Assert.AreEqual(5.9629629629629629629629629631M, indicator.GetValue(3)); // 3M / 3 + (1 - 1M / 3) * indicator.getValue(2)
            Assert.AreEqual(8.975308641975308641975308642M, indicator.GetValue(4)); // 15M / 3 + (1 - 1M / 3) * indicator.getValue(3)
        }

        [TestMethod]
        public void testXls()
        { // throws exception
            ITimeSeries xlsSeries = xls.getSeries();
            IIndicator<decimal> indicator;

            indicator = getIndicator(xlsSeries, 1);
            var x = xls.getIndicator(1);
            //Assert.AreEqual(xls.getIndicator(1), indicator);
            Assert.AreEqual(4.8M, indicator.GetValue(indicator.TimeSeries.GetEndIndex()));

            indicator = getIndicator(xlsSeries, 3);
          //  Assert.AreEqual(xls.getIndicator(3), indicator);
            Assert.AreEqual(7.4225453774091577325485045875M, indicator.GetValue(indicator.TimeSeries.GetEndIndex()));

            indicator = getIndicator(xlsSeries, 13);
           // Assert.AreEqual(xls.getIndicator(13), indicator);
            Assert.AreEqual(8.808250018233967850897443076M, indicator.GetValue(indicator.TimeSeries.GetEndIndex()));
        }

    }
}