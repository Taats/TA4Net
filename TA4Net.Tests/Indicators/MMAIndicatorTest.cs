/**
 * The MIT License (MIT)
 *
 * Copyright (c) 2014-2017 Marc de Verdelhan & respective authors (see AUTHORS)
 *
 * Permission is hereby granteM, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
 * the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KINM, EXPRESS OR
 * IMPLIEM, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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
    public class MMAIndicatorTest : IndicatorTest<IIndicator<decimal>, decimal, decimal>
    {

        private ExternalIndicatorTest xls;

        public MMAIndicatorTest()
            : base((t, v) => new MMAIndicator(t, (int)v[0]))
        {
            xls = new XLSIndicatorTest(GetType(), @"TestData\indicators\MMA.xls", 6);
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
            IIndicator<decimal> actualIndicator = getIndicator(new ClosePriceIndicator(data), 1);
            Assert.AreEqual(64.75M, actualIndicator.GetValue(0));
        }

        [TestMethod]
        public void mmaUsingTimeFrame10UsingClosePrice()
        { // throws exception
            IIndicator<decimal> actualIndicator = getIndicator(new ClosePriceIndicator(data), 10);
            Assert.AreEqual(63.99833548204M, actualIndicator.GetValue(9));
            Assert.AreEqual(63.731501933836M, actualIndicator.GetValue(10));
            Assert.AreEqual(63.5093517404524M, actualIndicator.GetValue(11));
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
            ClosePriceIndicator closePrice = new ClosePriceIndicator(bigSeries);
            IIndicator<decimal> actualIndicator = getIndicator(closePrice, 10);
            // if a StackOverflowError is thrown here, then the RecursiveCachedIndicator does not work as intended.
            Assert.AreEqual(9990.0M, actualIndicator.GetValue(9999));
        }

        [TestMethod, TestCategory("external data")]
        public void testAgainstExternalData()
        { // throws exception
            IIndicator<decimal> xlsClose = new ClosePriceIndicator(xls.getSeries());
            IIndicator<decimal> actualIndicator;

            actualIndicator = getIndicator(xlsClose, 1);
            //Assert.AreEqual(xls.getIndicator(1), actualIndicator);
            Assert.AreEqual(329.0M, actualIndicator.GetValue(actualIndicator.TimeSeries.GetEndIndex()));

            actualIndicator = getIndicator(xlsClose, 3);
            //Assert.AreEqual(xls.getIndicator(3), actualIndicator);
            Assert.AreEqual(327.29004419236330590585252094M, actualIndicator.GetValue(actualIndicator.TimeSeries.GetEndIndex()));

            actualIndicator = getIndicator(xlsClose, 13);
            //Assert.AreEqual(xls.getIndicator(13), actualIndicator);
            Assert.AreEqual(326.96964401585614734632198306M, actualIndicator.GetValue(actualIndicator.TimeSeries.GetEndIndex()));
        }
    }
}
