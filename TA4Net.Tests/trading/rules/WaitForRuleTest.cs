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
namespace TA4Net.Test.Trading.Rules
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net;
    using TA4Net.Interfaces;
    using TA4Net.Trading.Rules;
    using TA4Net.Trading.Rules.Types;

    [TestClass]
    public class WaitForRuleTest
    {

        private ITradingRecord tradingRecord;
        private WaitForRule rule;

        [TestInitialize]
        public void SetUp()
        {
            tradingRecord = new BaseTradingRecord();
        }

        [TestMethod]
        public void WaitForSinceLastBuyRuleIsSatisfied()
        {
            // Waits for 3 bars since last buy order
            rule = new WaitForRule(OrderType.BUY, 3);

            Assert.IsFalse(rule.IsSatisfied(0, null));
            Assert.IsFalse(rule.IsSatisfied(1, tradingRecord));

            tradingRecord.Enter(10);
            Assert.IsFalse(rule.IsSatisfied(10, tradingRecord));
            Assert.IsFalse(rule.IsSatisfied(11, tradingRecord));
            Assert.IsFalse(rule.IsSatisfied(12, tradingRecord));
            Assert.IsTrue(rule.IsSatisfied(13, tradingRecord));
            Assert.IsTrue(rule.IsSatisfied(14, tradingRecord));

            tradingRecord.Exit(15);
            Assert.IsTrue(rule.IsSatisfied(15, tradingRecord));
            Assert.IsTrue(rule.IsSatisfied(16, tradingRecord));

            tradingRecord.Enter(17);
            Assert.IsFalse(rule.IsSatisfied(17, tradingRecord));
            Assert.IsFalse(rule.IsSatisfied(18, tradingRecord));
            Assert.IsFalse(rule.IsSatisfied(19, tradingRecord));
            Assert.IsTrue(rule.IsSatisfied(20, tradingRecord));
        }

        [TestMethod]
        public void WaitForSinceLastSellRuleIsSatisfied()
        {
            // Waits for 2 bars since last sell order
            rule = new WaitForRule(OrderType.SELL, 2);

            Assert.IsFalse(rule.IsSatisfied(0, null));
            Assert.IsFalse(rule.IsSatisfied(1, tradingRecord));

            tradingRecord.Enter(10);
            Assert.IsFalse(rule.IsSatisfied(10, tradingRecord));
            Assert.IsFalse(rule.IsSatisfied(11, tradingRecord));
            Assert.IsFalse(rule.IsSatisfied(12, tradingRecord));
            Assert.IsFalse(rule.IsSatisfied(13, tradingRecord));

            tradingRecord.Exit(15);
            Assert.IsFalse(rule.IsSatisfied(15, tradingRecord));
            Assert.IsFalse(rule.IsSatisfied(16, tradingRecord));
            Assert.IsTrue(rule.IsSatisfied(17, tradingRecord));

            tradingRecord.Enter(17);
            Assert.IsTrue(rule.IsSatisfied(17, tradingRecord));
            Assert.IsTrue(rule.IsSatisfied(18, tradingRecord));

            tradingRecord.Exit(20);
            Assert.IsFalse(rule.IsSatisfied(20, tradingRecord));
            Assert.IsFalse(rule.IsSatisfied(21, tradingRecord));
            Assert.IsTrue(rule.IsSatisfied(22, tradingRecord));
        }
    }
}