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

    using TA4Net;
    using TA4Net.Analysis.Criteria;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net.Extensions;
    using TA4Net.Interfaces;

    [TestClass]
    public class VersusBuyAndHoldCriterionTest
    {

        [TestMethod]
        public void CalculateOnlyWithGainTrades()
        {
            MockTimeSeries series = new MockTimeSeries(100, 105, 110, 100, 95, 105);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(2, series),
                    Order.buyAt(3, series), Order.sellAt(5, series));

            AbstractAnalysisCriterion buyAndHold = new VersusBuyAndHoldCriterion(new TotalProfitCriterion());
            Assert.AreEqual(1.10M * 1.05M / 1.05M, buyAndHold.Calculate(series, tradingRecord));
        }

        [TestMethod]
        public void CalculateOnlyWithLossTrades()
        {
            MockTimeSeries series = new MockTimeSeries(100, 95, 100, 80, 85, 70);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(1, series),
                    Order.buyAt(2, series), Order.sellAt(5, series));

            AbstractAnalysisCriterion buyAndHold = new VersusBuyAndHoldCriterion(new TotalProfitCriterion());
            Assert.AreEqual(0.95M * 0.7M / 0.7M, buyAndHold.Calculate(series, tradingRecord));
        }

        [TestMethod]
        public void CalculateWithOnlyOneTrade()
        {
            MockTimeSeries series = new MockTimeSeries(100, 95, 100, 80, 85, 70);
            Trade trade = new Trade(Order.buyAt(0, series), Order.sellAt(1, series));

            AbstractAnalysisCriterion buyAndHold = new VersusBuyAndHoldCriterion(new TotalProfitCriterion());
            Assert.AreEqual(1.3571428571428571428571428571M, buyAndHold.Calculate(series, trade)); // (100M / 70) / (100M / 95)
        }

        [TestMethod]
        public void CalculateWithNoTrades()
        {
            MockTimeSeries series = new MockTimeSeries(100, 95, 100, 80, 85, 70);

            AbstractAnalysisCriterion buyAndHold = new VersusBuyAndHoldCriterion(new TotalProfitCriterion());
            Assert.AreEqual(1M / 0.7M, buyAndHold.Calculate(series, new BaseTradingRecord()));
        }

        [TestMethod]
        public void CalculateWithAverageProfit()
        {
            MockTimeSeries series = new MockTimeSeries(100, 95, 100, 80, 85, 130);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, Decimals.NaN, Decimals.NaN), Order.sellAt(1, Decimals.NaN, Decimals.NaN),
                    Order.buyAt(2, Decimals.NaN, Decimals.NaN), Order.sellAt(5, Decimals.NaN, Decimals.NaN));

            AbstractAnalysisCriterion buyAndHold = new VersusBuyAndHoldCriterion(new AverageProfitCriterion());

            Assert.AreEqual((95M / 100 * 130M / 100).Pow(1M / 6) / (130M / 100).Pow(1M / 6), buyAndHold.Calculate(series, tradingRecord));
        }

        [TestMethod]
        public void CalculateWithNumberOfBars()
        {
            MockTimeSeries series = new MockTimeSeries(100, 95, 100, 80, 85, 130);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(1, series),
                    Order.buyAt(2, series), Order.sellAt(5, series));

            AbstractAnalysisCriterion buyAndHold = new VersusBuyAndHoldCriterion(new NumberOfBarsCriterion());

            Assert.AreEqual(6M / 6M, buyAndHold.Calculate(series, tradingRecord));
        }

        [TestMethod]
        public void BetterThan()
        {
            AbstractAnalysisCriterion criterion = new VersusBuyAndHoldCriterion(new TotalProfitCriterion());
            Assert.IsTrue(criterion.BetterThan(2.0M, 1.5M));
            Assert.IsFalse(criterion.BetterThan(1.5M, 2.0M));
        }
    }
}
