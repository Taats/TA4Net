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
   using TA4Net.Trading.Rules;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net.Interfaces;

    [TestClass]
    public class PeriodicalGrowthRateIndicatorTest
    {

        private TimeSeriesManager seriesManager;

        private ClosePriceIndicator closePrice;

        [TestInitialize]
        public void setUp()
        {
            ITimeSeries mockSeries = new MockTimeSeries(
                    29.49M, 28.30M, 27.74M, 27.65M, 27.60M, 28.70M, 28.60M,
                    28.19M, 27.40M, 27.20M, 27.28M, 27.00M, 27.59M, 26.20M,
                    25.75M, 24.75M, 23.33M, 24.45M, 24.25M, 25.02M, 23.60M,
                    24.20M, 24.28M, 25.70M, 25.46M, 25.10M, 25.00M, 25.00M,
                    25.85M);
            seriesManager = new TimeSeriesManager(mockSeries);
            closePrice = new ClosePriceIndicator(mockSeries);
        }

        [TestMethod]
        public void testGetTotalReturn()
        {
            PeriodicalGrowthRateIndicator gri = new PeriodicalGrowthRateIndicator(this.closePrice, 5);
            decimal expResult = 0.9564173504382084670954600099M;
            decimal result = gri.getTotalReturn();
            Assert.AreEqual(expResult, result);
        }

        [TestMethod]
        public void testCalculation()
        {
            PeriodicalGrowthRateIndicator gri = new PeriodicalGrowthRateIndicator(this.closePrice, 5);

            Assert.AreEqual(gri.GetValue(0), Decimals.NaN);
            Assert.AreEqual(gri.GetValue(4), Decimals.NaN);
            Assert.AreEqual(gri.GetValue(5), -0.0267887419464225161071549678M);
            Assert.AreEqual(gri.GetValue(6), 0.0541392592141212378648022663M);
            Assert.AreEqual(gri.GetValue(10), -0.0494773519163763066202090592M);
            Assert.AreEqual(gri.GetValue(21), 0.2008897443121698431008904856M);
            Assert.AreEqual(gri.GetValue(24), 0.0220305258907783215778947842M);
            Assert.AreEqual(gri.GetValue(25), Decimals.NaN);
            Assert.AreEqual(gri.GetValue(26), Decimals.NaN);
        }

        [TestMethod]
        public void testStrategies()
        {

            PeriodicalGrowthRateIndicator gri = new PeriodicalGrowthRateIndicator(this.closePrice, 5);

            // Rules
            IRule buyingRule = new CrossedUpIndicatorRule(gri, Decimals.ZERO);
            IRule sellingRule = new CrossedDownIndicatorRule(gri, Decimals.ZERO);

            IStrategy strategy = new BaseStrategy(buyingRule, sellingRule);

            // Check trades
            int result = seriesManager.Run(strategy).GetTradeCount();
            int expResult = 3;

            Assert.AreEqual(expResult, result);
        }
    }
}