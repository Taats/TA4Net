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
namespace TA4Net.Test.Indicators.helpers
{
    using TA4Net;
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SumIndicatorTest
    {

        private SumIndicator sumIndicator;

        [TestInitialize]
        public void setUp()
        {
            ConstantIndicator<decimal> constantIndicator = new ConstantIndicator<decimal>(6M);
            FixedIndicator<decimal> mockIndicator = new FixedIndicator<decimal>(
                    -2.0M, 0.00M, 1.00M, 2.53M, 5.87M, 6.00M, 10.0M
            );

            FixedIndicator<decimal> mockIndicator2 = new FixedIndicator<decimal>(
                    Decimals.ZERO,
                    Decimals.ONE,
                    Decimals.TWO,
                    Decimals.THREE,
                    Decimals.TEN,
                    -42M,
                    -1337M
            );
            sumIndicator = new SumIndicator(constantIndicator, mockIndicator, mockIndicator2);
        }

        [TestMethod]
        public void getValue()
        {
            Assert.AreEqual(sumIndicator.GetValue(0), 4M);
            Assert.AreEqual(sumIndicator.GetValue(1), 7M);
            Assert.AreEqual(sumIndicator.GetValue(2), 9M);
            Assert.AreEqual(sumIndicator.GetValue(3), 11.53M);
            Assert.AreEqual(sumIndicator.GetValue(4), 21.87M);
            Assert.AreEqual(sumIndicator.GetValue(5), -30M);
            Assert.AreEqual(sumIndicator.GetValue(6), -1321M);
        }
    }
}