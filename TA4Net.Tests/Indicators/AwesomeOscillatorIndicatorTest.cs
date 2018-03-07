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
namespace TA4Net.Test.Indicators
{

    using TA4Net;
    using TA4Net.Indicators;
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class AwesomeOscillatorIndicatorTest
    {
        private ITimeSeries series;

        [TestInitialize]
        public void setUp()
        {

            List<IBar> bars = new List<IBar>();

            bars.Add(new MockBar(0, 0, 16, 8));
            bars.Add(new MockBar(0, 0, 12, 6));
            bars.Add(new MockBar(0, 0, 18, 14));
            bars.Add(new MockBar(0, 0, 10, 6));
            bars.Add(new MockBar(0, 0, 8, 4));

            this.series = new MockTimeSeries(bars);
        }

        [TestMethod]
        public void CalculateWithSma2AndSma3()
        {
            AwesomeOscillatorIndicator awesome = new AwesomeOscillatorIndicator(new MedianPriceIndicator(series), 2, 3);

            Assert.AreEqual(awesome.GetValue(0), 0);
            Assert.AreEqual(awesome.GetValue(1), 0);
            Assert.AreEqual(awesome.GetValue(2), 0.166666666666666666666666667M);
            Assert.AreEqual(awesome.GetValue(3), 1);
            Assert.AreEqual(awesome.GetValue(4), -3);
        }

        [TestMethod]
        public void withSma1AndSma2()
        {
            AwesomeOscillatorIndicator awesome = new AwesomeOscillatorIndicator(new MedianPriceIndicator(series), 1, 2);

            Assert.AreEqual(awesome.GetValue(0), 0);
            Assert.AreEqual(awesome.GetValue(1), -1.5M);
            Assert.AreEqual(awesome.GetValue(2), 3.5M);
            Assert.AreEqual(awesome.GetValue(3), -4);
            Assert.AreEqual(awesome.GetValue(4), -1);
        }

        [TestMethod]
        public void withSma()
        {
            AwesomeOscillatorIndicator awesome = new AwesomeOscillatorIndicator(new MedianPriceIndicator(series));

            Assert.AreEqual(awesome.GetValue(0), 0);
            Assert.AreEqual(awesome.GetValue(1), 0);
            Assert.AreEqual(awesome.GetValue(2), 0);
            Assert.AreEqual(awesome.GetValue(3), 0);
            Assert.AreEqual(awesome.GetValue(4), 0);
        }

    }
}
