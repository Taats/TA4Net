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
    using TA4Net.Interfaces;

    [TestClass]
    public class AverageProfitableTradesCriterionTest
    {

        [TestMethod]
        public void Calculate()
        {
            ITimeSeries series = new MockTimeSeries(100M, 95M, 102M, 105M, 97M, 113M);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(1, series),
                    Order.buyAt(2, series), Order.sellAt(3, series),
                    Order.buyAt(4, series), Order.sellAt(5, series));

            AverageProfitableTradesCriterion average = new AverageProfitableTradesCriterion();

            Assert.AreEqual(2M / 3, average.Calculate(series, tradingRecord));
        }

        [TestMethod]
        public void CalculateWithOneTrade()
        {
            ITimeSeries series = new MockTimeSeries(100M, 95M, 102M, 105, 97, 113);
            Trade trade = new Trade(Order.buyAt(0, series), Order.sellAt(1, series));

            AverageProfitableTradesCriterion average = new AverageProfitableTradesCriterion();
            Assert.AreEqual(0M, average.Calculate(series, trade));

            trade = new Trade(Order.buyAt(1, series), Order.sellAt(2, series));
            Assert.AreEqual(1M, average.Calculate(series, trade));
        }

        [TestMethod]
        public void betterThan()
        {
            AbstractAnalysisCriterion criterion = new AverageProfitableTradesCriterion();
            Assert.IsTrue(criterion.BetterThan(12, 8));
            Assert.IsFalse(criterion.BetterThan(8, 12));
        }
    }
}
