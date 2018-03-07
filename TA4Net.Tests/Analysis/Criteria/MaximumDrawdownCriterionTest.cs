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

    using TA4Net.Analysis.Criteria;
    using TA4Net;
    using TA4Net.Mocks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Interfaces;

    [TestClass]
    public class MaximumDrawdownCriterionTest
    {

        [TestMethod]
        public void CalculateWithNoTrades()
        {
            MockTimeSeries series = new MockTimeSeries(1, 2, 3, 6, 5, 20, 3);
            MaximumDrawdownCriterion mdd = new MaximumDrawdownCriterion();

            Assert.AreEqual(0M, mdd.Calculate(series, new BaseTradingRecord()));
        }

        [TestMethod]
        public void CalculateWithOnlyGains()
        {
            MockTimeSeries series = new MockTimeSeries(1, 2, 3, 6, 8, 20, 3);
            MaximumDrawdownCriterion mdd = new MaximumDrawdownCriterion();
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(1, series),
                    Order.buyAt(2, series), Order.sellAt(5, series));

            Assert.AreEqual(0M, mdd.Calculate(series, tradingRecord));
        }

        [TestMethod]
        public void CalculateShouldWork()
        {
            MockTimeSeries series = new MockTimeSeries(1, 2, 3, 6, 5, 20, 3);
            MaximumDrawdownCriterion mdd = new MaximumDrawdownCriterion();
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(1, series),
                    Order.buyAt(3, series), Order.sellAt(4, series),
                    Order.buyAt(5, series), Order.sellAt(6, series));

            Assert.AreEqual(.875M, mdd.Calculate(series, tradingRecord));

        }

        [TestMethod]
        public void CalculateWithNullSeriesSizeShouldReturn0()
        {
            MockTimeSeries series = new MockTimeSeries(new decimal[0]);
            MaximumDrawdownCriterion mdd = new MaximumDrawdownCriterion();
            Assert.AreEqual(0M, mdd.Calculate(series, new BaseTradingRecord()));
        }

        [TestMethod]
        public void WithTradesThatSellBeforeBuying()
        {
            MockTimeSeries series = new MockTimeSeries(2, 1, 3, 5, 6, 3, 20);
            MaximumDrawdownCriterion mdd = new MaximumDrawdownCriterion();
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(1, series),
                    Order.buyAt(3, series), Order.sellAt(4, series),
                    Order.sellAt(5, series), Order.buyAt(6, series));
            Assert.AreEqual(.91M, mdd.Calculate(series, tradingRecord));
        }

        [TestMethod]
        public void WithSimpleTrades()
        {
            MockTimeSeries series = new MockTimeSeries(1, 10, 5, 6, 1);
            MaximumDrawdownCriterion mdd = new MaximumDrawdownCriterion();
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(1, series),
                    Order.buyAt(1, series), Order.sellAt(2, series),
                    Order.buyAt(2, series), Order.sellAt(3, series),
                    Order.buyAt(3, series), Order.sellAt(4, series));
            Assert.AreEqual(.9M, mdd.Calculate(series, tradingRecord));
        }

        [TestMethod]
        public void BetterThan()
        {
            AbstractAnalysisCriterion criterion = new MaximumDrawdownCriterion();
            Assert.IsTrue(criterion.BetterThan(0.9M, 1.5M));
            Assert.IsFalse(criterion.BetterThan(1.2M, 0.4M));
        }
    }
}