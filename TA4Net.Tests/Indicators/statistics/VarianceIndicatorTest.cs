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

    using TA4Net;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Indicators.Statistics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net.Interfaces;

    [TestClass]
    public class VarianceIndicatorTest
    {
        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            data = new MockTimeSeries(1, 2, 3, 4, 3, 4, 5, 4, 3, 0, 9);
        }

        [TestMethod]
        public void varianceUsingTimeFrame4UsingClosePrice()
        {
            VarianceIndicator var = new VarianceIndicator(new ClosePriceIndicator(data), 4);

            Assert.AreEqual(var.GetValue(0), 0M);
            Assert.AreEqual(var.GetValue(1), 0.25M);
            Assert.AreEqual(var.GetValue(2), 2.0M / 3M);
            Assert.AreEqual(var.GetValue(3), 1.25M);
            Assert.AreEqual(var.GetValue(4), 0.5M);
            Assert.AreEqual(var.GetValue(5), 0.25M);
            Assert.AreEqual(var.GetValue(6), 0.5M);
            Assert.AreEqual(var.GetValue(7), 0.5M);
            Assert.AreEqual(var.GetValue(8), 0.5M);
            Assert.AreEqual(var.GetValue(9), 3.5M);
            Assert.AreEqual(var.GetValue(10), 10.5M);
        }

        [TestMethod]
        public void firstValueShouldBeZero()
        {
            VarianceIndicator var = new VarianceIndicator(new ClosePriceIndicator(data), 4);
            Assert.AreEqual(var.GetValue(0), 0);
        }

        [TestMethod]
        public void varianceShouldBeZeroWhenTimeFrameIs1()
        {
            VarianceIndicator var = new VarianceIndicator(new ClosePriceIndicator(data), 1);
            Assert.AreEqual(var.GetValue(3), 0);
            Assert.AreEqual(var.GetValue(8), 0);
        }

        [TestMethod]
        public void varianceUsingTimeFrame2UsingClosePrice()
        {
            VarianceIndicator var = new VarianceIndicator(new ClosePriceIndicator(data), 2);

            Assert.AreEqual(var.GetValue(0), 0);
            Assert.AreEqual(var.GetValue(1), 0.25M);
            Assert.AreEqual(var.GetValue(2), 0.25M);
            Assert.AreEqual(var.GetValue(3), 0.25M);
            Assert.AreEqual(var.GetValue(9), 2.25M);
            Assert.AreEqual(var.GetValue(10), 20.25M);
        }
    }
}