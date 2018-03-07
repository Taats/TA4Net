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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TA4Net.Mocks;
using System.Collections.Generic;
using TA4Net;
using TA4Net.Analysis.Criteria;
using TA4Net.Interfaces;
using TA4Net.Trading.Rules;

namespace TA4Net.Test.Analysis.Criteria
{
    [TestClass]
    public class AbstractAbstractAnalysisCriterionTest
    {

        private IStrategy alwaysStrategy;

        private IStrategy buyAndHoldStrategy;

        private List<IStrategy> strategies;

        [TestInitialize]
        public void setUp()
        {
            alwaysStrategy = new BaseStrategy(BooleanRule.TRUE, BooleanRule.TRUE);
            buyAndHoldStrategy = new BaseStrategy(new FixedRule(0), new FixedRule(4));
            strategies = new List<IStrategy>();
            strategies.Add(alwaysStrategy);
            strategies.Add(buyAndHoldStrategy);
        }

        [TestMethod]
        public void bestShouldBeAlwaysOperateOnProfit()
        {
            MockTimeSeries series = new MockTimeSeries(6.0M, 9.0M, 6.0M, 6.0M);
            TimeSeriesManager manager = new TimeSeriesManager(series);
            IStrategy bestStrategy = new TotalProfitCriterion().ChooseBest(manager, strategies);
            Assert.AreEqual(alwaysStrategy, bestStrategy);
        }

        [TestMethod]
        public void bestShouldBeBuyAndHoldOnLoss()
        {
            MockTimeSeries series = new MockTimeSeries(6.0M, 3.0M, 6.0M, 6.0M);
            TimeSeriesManager manager = new TimeSeriesManager(series);
            IStrategy bestStrategy = new TotalProfitCriterion().ChooseBest(manager, strategies);
            Assert.AreEqual(buyAndHoldStrategy, bestStrategy);
        }

        [TestMethod]
        public void ToStringMethod()
        {
            AbstractAnalysisCriterion c1 = new AverageProfitCriterion();
            Assert.AreEqual("Average Profit", c1.ToString());
            AbstractAnalysisCriterion c2 = new BuyAndHoldCriterion();
            Assert.AreEqual("Buy And Hold", c2.ToString());
            AbstractAnalysisCriterion c3 = new RewardRiskRatioCriterion();
            Assert.AreEqual("Reward Risk Ratio", c3.ToString());
        }

    }
}
