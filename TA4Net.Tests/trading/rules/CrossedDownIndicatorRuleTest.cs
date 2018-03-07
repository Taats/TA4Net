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
    using TA4Net.Indicators.Helpers;
    using TA4Net.Interfaces;
    using TA4Net.Trading.Rules;

    [TestClass]
    public class CrossedDownIndicatorRuleTest
    {

        private CrossedDownIndicatorRule rule;

        [TestInitialize]
        public void setUp()
        {
            IIndicator<decimal> evaluatedIndicator = new FixeddecimalIndicator(12, 11, 10, 9, 11, 8, 7, 6);
            rule = new CrossedDownIndicatorRule(evaluatedIndicator, Decimals.TEN);
        }

        [TestMethod]
        public void isSatisfied()
        {
            Assert.IsFalse(rule.IsSatisfied(0));
            Assert.IsFalse(rule.IsSatisfied(1));
            Assert.IsFalse(rule.IsSatisfied(2));
            Assert.IsTrue(rule.IsSatisfied(3));
            Assert.IsFalse(rule.IsSatisfied(4));
            Assert.IsTrue(rule.IsSatisfied(5));
            Assert.IsFalse(rule.IsSatisfied(6));
            Assert.IsFalse(rule.IsSatisfied(7));
        }
    }
}