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
    using System;
    using TA4Net.Interfaces;

    [TestClass]
    public class RewardRiskRatioCriterionTest
    {

        private RewardRiskRatioCriterion rrc;

        [TestInitialize]
        public void SetUp()
        {
            this.rrc = new RewardRiskRatioCriterion();
        }

        [TestMethod]
        public void RewardRiskRatioCriterion()
        {
            MockTimeSeries series = new MockTimeSeries(100, 105, 95, 100, 90, 95, 80, 120);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(1, series),
                    Order.buyAt(2, series), Order.sellAt(4, series),
                    Order.buyAt(5, series), Order.sellAt(7, series));

            // double totalProfit = (105d / 100) * (90d / 95d) * (120d / 95);
            // double peak = (105d / 100) * (100d / 95);
            // double low = (105d / 100) * (90d / 95) * (80d / 95);
            // totalProfit / ((peak - low) / peak)

            Assert.AreEqual(5.1899313501144164759725400496M, rrc.Calculate(series, tradingRecord)); 
        }

        [TestMethod]
        public void RewardRiskRatioCriterionOnlyWithGain()
        {
            MockTimeSeries series = new MockTimeSeries(1, 2, 3, 6, 8, 20, 3);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, series), Order.sellAt(1, series),
                    Order.buyAt(2, series), Order.sellAt(5, series));
            Assert.AreEqual(Decimals.NaN, rrc.Calculate(series, tradingRecord));
        }

        [TestMethod]
        public void RewardRiskRatioCriterionWithNoTrades()
        {
            MockTimeSeries series = new MockTimeSeries(1, 2, 3, 6, 8, 20, 3);
            Assert.AreEqual(Decimals.NaN, rrc.Calculate(series, new BaseTradingRecord()));
        }

        [TestMethod]
        public void WithOneTrade()
        {
            MockTimeSeries series = new MockTimeSeries(100, 95, 95, 100, 90, 95, 80, 120);
            Trade trade = new Trade(Order.buyAt(0, series), Order.sellAt(1, series));

            RewardRiskRatioCriterion ratioCriterion = new RewardRiskRatioCriterion();
            Assert.AreEqual((95M / 100) / ((1M - 0.95M)), ratioCriterion.Calculate(series, trade));
        }

        [TestMethod]
        public void BetterThan()
        {
            AbstractAnalysisCriterion criterion = new RewardRiskRatioCriterion();
            Assert.IsTrue(criterion.BetterThan(3.5M, 2.2M));
            Assert.IsFalse(criterion.BetterThan(1.5M, 2.7M));
        }
    }
}
