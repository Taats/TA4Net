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

    [TestClass]
    public class OrRuleTest
    {

        private IRule satisfiedRule;
        private IRule unsatisfiedRule;

        [TestInitialize]
        public void setUp()
        {
            satisfiedRule = new BooleanRule(true);
            unsatisfiedRule = new BooleanRule(false);
        }

        [TestMethod]
        public void isSatisfied()
        {
            Assert.IsTrue(satisfiedRule.Or(BooleanRule.FALSE).IsSatisfied(0));
            Assert.IsTrue(BooleanRule.FALSE.Or(satisfiedRule).IsSatisfied(0));
            Assert.IsFalse(unsatisfiedRule.Or(BooleanRule.FALSE).IsSatisfied(0));
            Assert.IsFalse(BooleanRule.FALSE.Or(unsatisfiedRule).IsSatisfied(0));

            Assert.IsTrue(satisfiedRule.Or(BooleanRule.TRUE).IsSatisfied(10));
            Assert.IsTrue(BooleanRule.TRUE.Or(satisfiedRule).IsSatisfied(10));
            Assert.IsTrue(unsatisfiedRule.Or(BooleanRule.TRUE).IsSatisfied(10));
            Assert.IsTrue(BooleanRule.TRUE.Or(unsatisfiedRule).IsSatisfied(10));
        }
    }
}