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
    public class XorRuleTest
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
            Assert.IsTrue(satisfiedRule.Xor(BooleanRule.FALSE).IsSatisfied(0));
            Assert.IsTrue(BooleanRule.FALSE.Xor(satisfiedRule).IsSatisfied(0));
            Assert.IsFalse(unsatisfiedRule.Xor(BooleanRule.FALSE).IsSatisfied(0));
            Assert.IsFalse(BooleanRule.FALSE.Xor(unsatisfiedRule).IsSatisfied(0));

            Assert.IsFalse(satisfiedRule.Xor(BooleanRule.TRUE).IsSatisfied(10));
            Assert.IsFalse(BooleanRule.TRUE.Xor(satisfiedRule).IsSatisfied(10));
            Assert.IsTrue(unsatisfiedRule.Xor(BooleanRule.TRUE).IsSatisfied(10));
            Assert.IsTrue(BooleanRule.TRUE.Xor(unsatisfiedRule).IsSatisfied(10));
        }
    }
}
        