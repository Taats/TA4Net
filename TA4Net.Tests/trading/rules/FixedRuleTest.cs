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
    using TA4Net.Trading.Rules;

    [TestClass]
    public class FixedRuleTest
    {

        [TestMethod]
        public void isSatisfied()
        {
            FixedRule fixedRule = new FixedRule();
            Assert.IsFalse(fixedRule.IsSatisfied(0));
            Assert.IsFalse(fixedRule.IsSatisfied(1));
            Assert.IsFalse(fixedRule.IsSatisfied(2));
            Assert.IsFalse(fixedRule.IsSatisfied(9));

            fixedRule = new FixedRule(1, 2, 3);
            Assert.IsFalse(fixedRule.IsSatisfied(0));
            Assert.IsTrue(fixedRule.IsSatisfied(1));
            Assert.IsTrue(fixedRule.IsSatisfied(2));
            Assert.IsTrue(fixedRule.IsSatisfied(3));
            Assert.IsFalse(fixedRule.IsSatisfied(4));
            Assert.IsFalse(fixedRule.IsSatisfied(5));
            Assert.IsFalse(fixedRule.IsSatisfied(6));
            Assert.IsFalse(fixedRule.IsSatisfied(7));
            Assert.IsFalse(fixedRule.IsSatisfied(8));
            Assert.IsFalse(fixedRule.IsSatisfied(9));
            Assert.IsFalse(fixedRule.IsSatisfied(10));
        }
    }
}