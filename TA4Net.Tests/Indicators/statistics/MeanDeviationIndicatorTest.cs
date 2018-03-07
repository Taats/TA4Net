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
namespace TA4Net.Test.Indicators.statistics
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Indicators.Statistics;
    using TA4Net.Interfaces;

    [TestClass]
    public class MeanDeviationIndicatorTest
    {

        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            data = new MockTimeSeries(1, 2, 7, 6, 3, 4, 5, 11, 3, 0, 9);
        }

        [TestMethod]
        public void meanDeviationUsingTimeFrame5UsingClosePrice()
        {
            MeanDeviationIndicator meanDeviation = new MeanDeviationIndicator(new ClosePriceIndicator(data), 5);

            Assert.AreEqual(meanDeviation.GetValue(2), 2.4444444444444444444444444444M);
            Assert.AreEqual(meanDeviation.GetValue(3), 2.5M);
            Assert.AreEqual(meanDeviation.GetValue(7), 2.16M);
            Assert.AreEqual(meanDeviation.GetValue(8), 2.32M);
            Assert.AreEqual(meanDeviation.GetValue(9), 2.72M);
        }

        [TestMethod]
        public void firstValueShouldBeZero()
        {
            MeanDeviationIndicator meanDeviation = new MeanDeviationIndicator(new ClosePriceIndicator(data), 5);
            Assert.AreEqual(meanDeviation.GetValue(0), 0);
        }

        [TestMethod]
        public void meanDeviationShouldBeZeroWhenTimeFrameIs1()
        {
            MeanDeviationIndicator meanDeviation = new MeanDeviationIndicator(new ClosePriceIndicator(data), 1);
            Assert.AreEqual(meanDeviation.GetValue(2), 0);
            Assert.AreEqual(meanDeviation.GetValue(7), 0);
        }
    }
}