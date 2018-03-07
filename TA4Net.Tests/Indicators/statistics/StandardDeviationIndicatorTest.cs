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
namespace TA4Net.Test.Indicators.statistics
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Indicators.Statistics;
    using TA4Net.Interfaces;

    [TestClass]
    public class StandardDeviationIndicatorTest
    {
        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            data = new MockTimeSeries(1, 2, 3, 4, 3, 4, 5, 4, 3, 0, 9);
        }

        [TestMethod]
        public void standardDeviationUsingTimeFrame4UsingClosePrice()
        {
            StandardDeviationIndicator sdv = new StandardDeviationIndicator(new ClosePriceIndicator(data), 4);

            Assert.AreEqual(sdv.GetValue(0),  0M);
            Assert.AreEqual(sdv.GetValue(1),  0.5M); // Math.Sqrt(0.25));
            Assert.AreEqual(sdv.GetValue(2), 0.8164965809277260327324280249M); // Math.Sqrt(2.0 / 3));
            Assert.AreEqual(sdv.GetValue(3), 1.1180339887498948482045868344M); // Math.Sqrt(1.25));
            Assert.AreEqual(sdv.GetValue(4), 0.7071067811865475244008443621M); // Math.Sqrt(0.5));
            Assert.AreEqual(sdv.GetValue(5),  0.5M); // Math.Sqrt(0.25));
            Assert.AreEqual(sdv.GetValue(6), 0.7071067811865475244008443621M); // Math.Sqrt(0.5));
            Assert.AreEqual(sdv.GetValue(7), 0.7071067811865475244008443621M); // Math.Sqrt(0.5));
            Assert.AreEqual(sdv.GetValue(8), 0.7071067811865475244008443621M); // Math.Sqrt(0.5));
            Assert.AreEqual(sdv.GetValue(9), 1.8708286933869706927918743662M); // Math.Sqrt(3.5));
            Assert.AreEqual(sdv.GetValue(10), 3.2403703492039301154829837180M); // Math.Sqrt(10.5));
        }

        [TestMethod]
        public void standardDeviationShouldBeZeroWhenTimeFrameIs1()
        {
            StandardDeviationIndicator sdv = new StandardDeviationIndicator(new ClosePriceIndicator(data), 1);
            Assert.AreEqual(sdv.GetValue(3), 0);
            Assert.AreEqual(sdv.GetValue(8), 0);
        }
    }
}
