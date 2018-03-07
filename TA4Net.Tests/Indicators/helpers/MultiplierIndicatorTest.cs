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

    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MultiplierIndicatorTest
    {
        private MultiplierIndicator multiplierIndicator;

        [TestInitialize]
        public void setUp()
        {
            ConstantIndicator<decimal> constantIndicator = new ConstantIndicator<decimal>(6);
            multiplierIndicator = new MultiplierIndicator(constantIndicator, 0.75M);
        }

        [TestMethod]
        public void constantIndicator()
        {
            Assert.AreEqual(multiplierIndicator.GetValue(10), 4.5M);
            Assert.AreEqual(multiplierIndicator.GetValue(1), 4.5M);
            Assert.AreEqual(multiplierIndicator.GetValue(0), 4.5M);
            Assert.AreEqual(multiplierIndicator.GetValue(30), 4.5M);
        }
    }
}