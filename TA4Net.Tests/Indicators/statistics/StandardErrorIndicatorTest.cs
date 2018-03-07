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
    using TA4Net.Indicators.Helpers;
    using TA4Net.Indicators.Statistics;
    using TA4Net.Interfaces;

    [TestClass]
    public class StandardErrorIndicatorTest
    {
        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            data = new MockTimeSeries(10, 20, 30, 40, 50, 40, 40, 50, 40, 30, 20, 10);
        }

        [TestMethod]
        public void usingTimeFrame5UsingClosePrice()
        {
            StandardErrorIndicator se = new StandardErrorIndicator(new ClosePriceIndicator(data), 5);

            Assert.AreEqual(se.GetValue(0), 0);
            Assert.AreEqual(se.GetValue(1), 3.5355339059327376220042218103M);
            Assert.AreEqual(se.GetValue(2), 4.714045207910316829338962414M);
            Assert.AreEqual(se.GetValue(3), 5.590169943749474241022934172M);
            Assert.AreEqual(se.GetValue(4), 6.3245553203367586639977870886M);
            Assert.AreEqual(se.GetValue(5), 4.5607017003965519165441961021M);
            Assert.AreEqual(se.GetValue(6), 2.8284271247461900976033774483M);
            Assert.AreEqual(se.GetValue(7), 2.1908902300206644538278791311M);
            Assert.AreEqual(se.GetValue(8), 2.1908902300206644538278791311M);
            Assert.AreEqual(se.GetValue(9), 2.8284271247461900976033774483M);
            Assert.AreEqual(se.GetValue(10), 4.5607017003965519165441961021M);
            Assert.AreEqual(se.GetValue(11), 6.3245553203367586639977870886M);
        }

        [TestMethod]
        public void shouldBeZeroWhenTimeFrameIs1()
        {
            StandardErrorIndicator se = new StandardErrorIndicator(new ClosePriceIndicator(data), 1);
            Assert.AreEqual(se.GetValue(1), 0);
            Assert.AreEqual(se.GetValue(3), 0);
        }
    }
}