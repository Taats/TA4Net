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

    using TA4Net.Test.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net;
    using TA4Net.Indicators;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Interfaces;
    using TA4Net.Trading.Rules;

    [TestClass]
    public class CachedIndicatorTest
    {

        private ITimeSeries series;

        [TestInitialize]
        public void setUp()
        {
            series = new MockTimeSeries(1, 2, 3, 4, 3, 4, 5, 4, 3, 3, 4, 3, 2);
        }

        [TestMethod]
        public void ifCacheWorks()
        {
            SMAIndicator sma = new SMAIndicator(new ClosePriceIndicator(series), 3);
            decimal firstTime = sma.GetValue(4);
            decimal secondTime = sma.GetValue(4);
            Assert.AreEqual(firstTime, secondTime);
        }

        [TestMethod]
        public void getValueWithNullTimeSeries()
        {

            ConstantIndicator<decimal> constant = new ConstantIndicator<decimal>(Decimals.TEN);
            Assert.AreEqual(Decimals.TEN, constant.GetValue(0));
            Assert.AreEqual(Decimals.TEN, constant.GetValue(100));
            Assert.IsNull(constant.TimeSeries);

            SMAIndicator sma = new SMAIndicator(constant, 10);
            Assert.AreEqual(Decimals.TEN, sma.GetValue(0));
            Assert.AreEqual(Decimals.TEN, sma.GetValue(100));
            Assert.IsNull(sma.TimeSeries);
        }

        [TestMethod]
        public void getValueWithCacheLengthIncrease()
        {
            decimal[] data = new decimal[200];
            Arrays.fill(data, 10);
            SMAIndicator sma = new SMAIndicator(new ClosePriceIndicator(new MockTimeSeries(data)), 100);
            Assert.AreEqual(sma.GetValue(105), 10);
        }

        [TestMethod]
        public void getValueWithOldResultsRemoval()
        {
            decimal[] data = new decimal[20];
            Arrays.fill(data, 1);
            ITimeSeries timeSeries = new MockTimeSeries(data);
            SMAIndicator sma = new SMAIndicator(new ClosePriceIndicator(timeSeries), 10);
            Assert.AreEqual(sma.GetValue(5), 1);
            Assert.AreEqual(sma.GetValue(10), 1);
            timeSeries.SetMaximumBarCount(12);
            Assert.AreEqual(sma.GetValue(19), 1);
        }

        [TestMethod]
        public void strategyExecutionOnCachedIndicatorAndLimitedTimeSeries()
        {
            ITimeSeries timeSeries = new MockTimeSeries(0, 1, 2, 3, 4, 5, 6, 7);
            SMAIndicator sma = new SMAIndicator(new ClosePriceIndicator(timeSeries), 2);
            // Theoretical values for SMA(2) cache: 0, 0.5, 1.5, 2.5, 3.5, 4.5, 5.5, 6.5
            timeSeries.SetMaximumBarCount(6);
            // Theoretical values for SMA(2) cache: null, null, 2, 2.5, 3.5, 4.5, 5.5, 6.5

            IStrategy strategy = new BaseStrategy(
                    new OverIndicatorRule(sma, Decimals.THREE),
                    new UnderIndicatorRule(sma, Decimals.THREE)
            );
            // Theoretical shouldEnter results: false, false, false, false, true, true, true, true
            // Theoretical shouldExit results: false, false, true, true, false, false, false, false

            // As we return the first bar/result found for the removed bars:
            // -> Approximated values for ClosePrice cache: 2, 2, 2, 3, 4, 5, 6, 7
            // -> Approximated values for SMA(2) cache: 2, 2, 2, 2.5, 3.5, 4.5, 5.5, 6.5

            // Then enters/exits are also approximated:
            // -> shouldEnter results: false, false, false, false, true, true, true, true
            // -> shouldExit results: true, true, true, true, false, false, false, false

            Assert.IsFalse(strategy.ShouldEnter(0));
            Assert.IsTrue(strategy.ShouldExit(0));
            Assert.IsFalse(strategy.ShouldEnter(1));
            Assert.IsTrue(strategy.ShouldExit(1));
            Assert.IsFalse(strategy.ShouldEnter(2));
            Assert.IsTrue(strategy.ShouldExit(2));
            Assert.IsFalse(strategy.ShouldEnter(3));
            Assert.IsTrue(strategy.ShouldExit(3));
            Assert.IsTrue(strategy.ShouldEnter(4));
            Assert.IsFalse(strategy.ShouldExit(4));
            Assert.IsTrue(strategy.ShouldEnter(5));
            Assert.IsFalse(strategy.ShouldExit(5));
            Assert.IsTrue(strategy.ShouldEnter(6));
            Assert.IsFalse(strategy.ShouldExit(6));
            Assert.IsTrue(strategy.ShouldEnter(7));
            Assert.IsFalse(strategy.ShouldExit(7));
        }

        [TestMethod]
        public void getValueOnResultsCalculatedFromRemovedBarsShouldReturnFirstRemainingResult()
        {
            ITimeSeries timeSeries = new MockTimeSeries(1, 1, 1, 1, 1);
            timeSeries.SetMaximumBarCount(3);
            Assert.AreEqual(2, timeSeries.GetRemovedBarsCount());

            SMAIndicator sma = new SMAIndicator(new ClosePriceIndicator(timeSeries), 2);
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(1, sma.GetValue(i), $"failed for {i}");
            }
        }

        [TestMethod]
        public void recursiveCachedIndicatorOnMovingTimeSeriesShouldNotCauseStackOverflow()
        {
            // Added to check issue #120: https://github.com/mdeverdelhan/ta4j/issues/120
            // See also: CachedIndicator#getValue(int index)
            series = new MockTimeSeries();
            series.SetMaximumBarCount(5);
            Assert.AreEqual(5, series.GetBarCount());

            ZLEMAIndicator zlema = new ZLEMAIndicator(new ClosePriceIndicator(series), 1);

            Assert.AreEqual(zlema.GetValue(8), 4996);
        }
    }
}