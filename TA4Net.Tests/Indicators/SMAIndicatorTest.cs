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
    using TA4Net.Interfaces;

    [TestClass]
    public class SMAIndicatorTest : IndicatorTest<IIndicator<decimal>, decimal, decimal>
    {

        private ExternalIndicatorTest xls;

        public SMAIndicatorTest()
            : base((t, v) => new SMAIndicator(t, (int)v[0]))
        { // throws exception
            xls = new XLSIndicatorTest(GetType(), @"TestData\indicators\SMA.xls", 6);
        }

        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            data = new MockTimeSeries(1, 2, 3, 4, 3, 4, 5, 4, 3, 3, 4, 3, 2);
        }

        [TestMethod]
        public void usingTimeFrame3UsingClosePrice()
        { // throws exception
            IIndicator<decimal> indicator = getIndicator(new ClosePriceIndicator(data), 3);

            Assert.AreEqual(indicator.GetValue(0), 1);
            Assert.AreEqual(indicator.GetValue(1), 1.5M);
            Assert.AreEqual(indicator.GetValue(2), 2);
            Assert.AreEqual(indicator.GetValue(3), 3);
            Assert.AreEqual(indicator.GetValue(4), 10M / 3);
            Assert.AreEqual(indicator.GetValue(5), 11M / 3);
            Assert.AreEqual(indicator.GetValue(6), 4);
            Assert.AreEqual(indicator.GetValue(7), 13M / 3);
            Assert.AreEqual(indicator.GetValue(8), 4);
            Assert.AreEqual(indicator.GetValue(9), 10M / 3);
            Assert.AreEqual(indicator.GetValue(10), 10M / 3);
            Assert.AreEqual(indicator.GetValue(11), 10M / 3);
            Assert.AreEqual(indicator.GetValue(12), 3);
        }

        [TestMethod]
        public void whenTimeFrameIs1ResultShouldBeIndicatorValue()
        { // throws exception
            IIndicator<decimal> indicator = getIndicator(new ClosePriceIndicator(data), 1);
            for (int i = 0; i < data.GetBarCount(); i++)
            {
                Assert.AreEqual(data.GetBar(i).ClosePrice, indicator.GetValue(i));
            }
        }

        [TestMethod]
        public void externalData()
        { // throws exception
            IIndicator<decimal> xlsClose = new ClosePriceIndicator(xls.getSeries());
            IIndicator<decimal> actualIndicator;

            actualIndicator = getIndicator(xlsClose, 1);
          //  Assert.AreEqual(xls.getIndicator(1), actualIndicator);
            Assert.AreEqual(329.0M, actualIndicator.GetValue(actualIndicator.TimeSeries.GetEndIndex()));

            actualIndicator = getIndicator(xlsClose, 3);
            //Assert.AreEqual(xls.getIndicator(3), actualIndicator);
            Assert.AreEqual(326.63333333333333333333333333M, actualIndicator.GetValue(actualIndicator.TimeSeries.GetEndIndex()));

            actualIndicator = getIndicator(xlsClose, 13);
            //Assert.AreEqual(xls.getIndicator(13), actualIndicator);
            Assert.AreEqual(327.78461538461538461538461538M, actualIndicator.GetValue(actualIndicator.TimeSeries.GetEndIndex()));
        }

    }
}
