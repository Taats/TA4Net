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
    public class FixedIndicatorTest
    {

        [TestMethod]
        public void getValueOnFixeddecimalIndicator()
        {
            FixeddecimalIndicator fixeddecimalIndicator = new FixeddecimalIndicator(13.37M, 42, -17);
            Assert.AreEqual(fixeddecimalIndicator.GetValue(0), 13.37M);
            Assert.AreEqual(fixeddecimalIndicator.GetValue(1), 42M);
            Assert.AreEqual(fixeddecimalIndicator.GetValue(2), -17M);

            fixeddecimalIndicator = new FixeddecimalIndicator(3.0M, -123.456M, 0M);
            Assert.AreEqual(fixeddecimalIndicator.GetValue(0), 3M);
            Assert.AreEqual(fixeddecimalIndicator.GetValue(1), -123.456M);
            Assert.AreEqual(fixeddecimalIndicator.GetValue(2), 0.0M);

        }

        [TestMethod]
        public void getValueOnFixedBooleanIndicator()
        {
            FixedBooleanIndicator fixedBooleanIndicator = new FixedBooleanIndicator(false, false, true, false, true);
            Assert.IsFalse(fixedBooleanIndicator.GetValue(0));
            Assert.IsFalse(fixedBooleanIndicator.GetValue(1));
            Assert.IsTrue(fixedBooleanIndicator.GetValue(2));
            Assert.IsFalse(fixedBooleanIndicator.GetValue(3));
            Assert.IsTrue(fixedBooleanIndicator.GetValue(4));
        }
    }
}