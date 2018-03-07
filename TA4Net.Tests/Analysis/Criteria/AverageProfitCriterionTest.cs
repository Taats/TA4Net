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
namespace TA4Net.Test.Analysis.Criteria
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net;
    using TA4Net.Analysis.Criteria;
    using TA4Net.Interfaces;

    [TestClass]
    public class AverageProfitCriterionTest
    {
        private MockTimeSeries series;

        [TestMethod]
        public void CalculateOnlyWithGainTrades()
        {
            series = new MockTimeSeries(100M, 105M, 110M, 100M, 95M, 105M);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(2, series),
                    Order.buyAt(3, series), Order.sellAt(5, series));
            AbstractAnalysisCriterion averageProfit = new AverageProfitCriterion();
            Assert.AreEqual(1.0243074482606615553441174081M, averageProfit.Calculate(series, tradingRecord));
        }

        [TestMethod]
        public void CalculateWithASimpleTrade()
        {
            series = new MockTimeSeries(100M, 105M, 110M, 100M, 95M, 105M);
            ITradingRecord tradingRecord = new BaseTradingRecord(Order.buyAt(0, series), Order.sellAt(2, series));
            AbstractAnalysisCriterion averageProfit = new AverageProfitCriterion();
            Assert.AreEqual(1.0322801154563671592135852251M, averageProfit.Calculate(series, tradingRecord)); // (decimal)Math.Pow(110d / 100, 1d / 3)
        }

        [TestMethod]
        public void CalculateOnlyWithLossTrades()
        {
            series = new MockTimeSeries(100, 95, 100, 80, 85, 70);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(1, series),
                    Order.buyAt(2, series), Order.sellAt(5, series));
            AbstractAnalysisCriterion averageProfit = new AverageProfitCriterion();
            Assert.AreEqual(0.934265419203025064794086149M, averageProfit.Calculate(series, tradingRecord)); // (decimal)Math.Pow(95d / 100 * 70d / 100, 1d / 6)
        }

        [TestMethod]
        public void CalculateWithNoBarsShouldReturn1()
        {
            series = new MockTimeSeries(100, 95, 100, 80, 85, 70);
            AbstractAnalysisCriterion averageProfit = new AverageProfitCriterion();
            Assert.AreEqual(1M, averageProfit.Calculate(series, new BaseTradingRecord()));
        }

        [TestMethod]
        public void CalculateWithOneTrade()
        {
            series = new MockTimeSeries(100, 105);
            Trade trade = new Trade(Order.buyAt(0, series), Order.sellAt(1, series));
            AbstractAnalysisCriterion average = new AverageProfitCriterion();
            Assert.AreEqual(1.0246950765959598383221038678M, average.Calculate(series, trade));
        }

        [TestMethod]
        public void BetterThan()
        {
            AbstractAnalysisCriterion criterion = new AverageProfitCriterion();
            Assert.IsTrue(criterion.BetterThan(2.0M, 1.5M));
            Assert.IsFalse(criterion.BetterThan(1.5M, 2.0M));
        }
    }
}
