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

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net.Indicators;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Interfaces;

    [TestClass]
    public class HMAIndicatorTest
    {

        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            data = new MockTimeSeries(
                    84.53M, 87.39M, 84.55M,
                    82.83M, 82.58M, 83.74M,
                    83.33M, 84.57M, 86.98M,
                    87.10M, 83.11M, 83.60M,
                    83.66M, 82.76M, 79.22M,
                    79.03M, 78.18M, 77.42M,
                    74.65M, 77.48M, 76.87M
            );
        }

        [TestMethod]
        public void hmaUsingTimeFrame9UsingClosePrice()
        {
            // Example from http://traders.com/Documentation/FEEDbk_docs/2010/12/TradingIndexesWithHullMA.xls
            HMAIndicator hma = new HMAIndicator(new ClosePriceIndicator(data), 9);
            Assert.AreEqual(hma.GetValue(10), 86.32044444444444444444444444M);
            Assert.AreEqual(hma.GetValue(11), 85.37048148148148148148148148M);
            Assert.AreEqual(hma.GetValue(12), 84.10444444444444444444444444M);
            Assert.AreEqual(hma.GetValue(13), 83.01974074074074074074074074M);
            Assert.AreEqual(hma.GetValue(14), 81.39133333333333333333333334M);
            Assert.AreEqual(hma.GetValue(15), 79.65111111111111111111111111M);
            Assert.AreEqual(hma.GetValue(16), 78.044296296296296296296296297M);
            Assert.AreEqual(hma.GetValue(17), 76.88322222222222222222222222M);
            Assert.AreEqual(hma.GetValue(18), 75.536333333333333333333333332M);
            Assert.AreEqual(hma.GetValue(19), 75.171296296296296296296296297M);
            Assert.AreEqual(hma.GetValue(20), 75.35974074074074074074074074M);
        }

    }
}