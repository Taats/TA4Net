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
    public class BuyAndHoldCriterionTest
    {

        [TestMethod]
        public void CalculateOnlyWithGainTrades()
        {
            MockTimeSeries series = new MockTimeSeries(100, 105, 110, 100, 95, 105);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(2, series),
                    Order.buyAt(3, series), Order.sellAt(5, series));

            AbstractAnalysisCriterion buyAndHold = new BuyAndHoldCriterion();
            Assert.AreEqual(1.05M, buyAndHold.Calculate(series, tradingRecord));
        }

        [TestMethod]
        public void CalculateOnlyWithLossTrades()
        {
            MockTimeSeries series = new MockTimeSeries(100, 95, 100, 80, 85, 70);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(1, series),
                    Order.buyAt(2, series), Order.sellAt(5, series));

            AbstractAnalysisCriterion buyAndHold = new BuyAndHoldCriterion();
            Assert.AreEqual(0.7M, buyAndHold.Calculate(series, tradingRecord));
        }

        [TestMethod]
        public void CalculateWithNoTrades()
        {
            MockTimeSeries series = new MockTimeSeries(100, 95, 100, 80, 85, 70);

            AbstractAnalysisCriterion buyAndHold = new BuyAndHoldCriterion();
            Assert.AreEqual(0.7M, buyAndHold.Calculate(series, new BaseTradingRecord()));
        }

        [TestMethod]
        public void CalculateWithOneTrade()
        {
            MockTimeSeries series = new MockTimeSeries(100, 105);
            Trade trade = new Trade(Order.buyAt(0, series), Order.sellAt(1, series));
            AbstractAnalysisCriterion buyAndHold = new BuyAndHoldCriterion();
            Assert.AreEqual(105M / 100, buyAndHold.Calculate(series, trade));
        }

        [TestMethod]
        public void BetterThan()
        {
            AbstractAnalysisCriterion criterion = new BuyAndHoldCriterion();
            Assert.IsTrue(criterion.BetterThan(1.3M, 1.1M));
            Assert.IsFalse(criterion.BetterThan(0.6M, 0.9M));
        }
    }
}
