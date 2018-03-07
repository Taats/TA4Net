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
    public class WMAIndicatorTest
    {

        [TestMethod]
        public void Calculate()
        {
            MockTimeSeries series = new MockTimeSeries(1, 2, 3, 4, 5, 6);
            IIndicator<decimal> close = new ClosePriceIndicator(series);
            IIndicator<decimal> wmaIndicator = new WMAIndicator(close, 3);

            Assert.AreEqual(wmaIndicator.GetValue(0), 1);
            Assert.AreEqual(wmaIndicator.GetValue(1), 1.6666666666666666666666666667M);
            Assert.AreEqual(wmaIndicator.GetValue(2), 2.3333333333333333333333333333M);
            Assert.AreEqual(wmaIndicator.GetValue(3), 3.3333333333333333333333333333M);
            Assert.AreEqual(wmaIndicator.GetValue(4), 4.3333333333333333333333333333M);
            Assert.AreEqual(wmaIndicator.GetValue(5), 5.3333333333333333333333333333M);
        }

        [TestMethod]
        public void wmaWithTimeFrameGreaterThanSeriesSize()
        {
            MockTimeSeries series = new MockTimeSeries(1, 2, 3, 4, 5, 6);
            IIndicator<decimal> close = new ClosePriceIndicator(series);
            IIndicator<decimal> wmaIndicator = new WMAIndicator(close, 55);

            Assert.AreEqual(1, wmaIndicator.GetValue(0));
            Assert.AreEqual(1.6666666666666666666666666667M, wmaIndicator.GetValue(1));
            Assert.AreEqual(2.3333333333333333333333333333M, wmaIndicator.GetValue(2));
            Assert.AreEqual(3, wmaIndicator.GetValue(3));
            Assert.AreEqual(3.6666666666666666666666666667M, wmaIndicator.GetValue(4));
            Assert.AreEqual(4.3333333333333333333333333333M, wmaIndicator.GetValue(5));
        }

        [TestMethod]
        public void wmaUsingTimeFrame9UsingClosePrice()
        {
            // Example from http://traders.com/Documentation/FEEDbk_docs/2010/12/TradingIndexesWithHullMA.xls
            ITimeSeries data = new MockTimeSeries(
                    84.53M, 87.39M, 84.55M,
                    82.83M, 82.58M, 83.74M,
                    83.33M, 84.57M, 86.98M,
                    87.10M, 83.11M, 83.60M,
                    83.66M, 82.76M, 79.22M,
                    79.03M, 78.18M, 77.42M,
                    74.65M, 77.48M, 76.87M
            );

            WMAIndicator wma = new WMAIndicator(new ClosePriceIndicator(data), 9);
            Assert.AreEqual(84.49577777777777777777777778M, wma.GetValue(8));
            Assert.AreEqual(85.01577777777777777777777778M, wma.GetValue(9));
            Assert.AreEqual(84.68066666666666666666666667M, wma.GetValue(10));
            Assert.AreEqual(84.53866666666666666666666667M, wma.GetValue(11));
            Assert.AreEqual(84.42977777777777777777777778M, wma.GetValue(12));
            Assert.AreEqual(84.12244444444444444444444444M, wma.GetValue(13));
            Assert.AreEqual(83.10311111111111111111111111M, wma.GetValue(14));
            Assert.AreEqual(82.14622222222222222222222222M, wma.GetValue(15));
            Assert.AreEqual(81.11488888888888888888888889M, wma.GetValue(16));
            Assert.AreEqual(80.07355555555555555555555556M, wma.GetValue(17));
            Assert.AreEqual(78.690666666666666666666666667M, wma.GetValue(18));
            Assert.AreEqual(78.150444444444444444444444444M, wma.GetValue(19));
            Assert.AreEqual(77.613333333333333333333333333M, wma.GetValue(20));
        }
    }
}
