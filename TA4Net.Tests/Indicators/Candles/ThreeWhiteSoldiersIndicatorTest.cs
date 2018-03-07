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
namespace TA4Net.Test.Indicators.candles
{

    using TA4Net;
    using TA4Net.Indicators.Candles;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class ThreeWhiteSoldiersIndicatorTest
    {

        private ITimeSeries series;

        [TestInitialize]
        public void setUp()
        {
            List<IBar> bars = new List<IBar>();
            // open, close, high, low
            bars.Add(new MockBar(19, 19, 22, 15));
            bars.Add(new MockBar(10, 18, 20, 8));
            bars.Add(new MockBar(17, 16, 21, 15));
            bars.Add(new MockBar(15.6M, 18, 18.1M, 14));
            bars.Add(new MockBar(16, 19.9M, 20, 15));
            bars.Add(new MockBar(16.8M, 23, 23, 16.7M));
            bars.Add(new MockBar(17, 25, 25, 17));
            bars.Add(new MockBar(23, 16.8M, 24, 15));
            series = new MockTimeSeries(bars);
        }

        [TestMethod]
        public void getValue()
        {
            ThreeWhiteSoldiersIndicator tws = new ThreeWhiteSoldiersIndicator(series, 3, 0.1M);
            Assert.IsFalse(tws.GetValue(0));
            Assert.IsFalse(tws.GetValue(1));
            Assert.IsFalse(tws.GetValue(2));
            Assert.IsFalse(tws.GetValue(3));
            Assert.IsFalse(tws.GetValue(4));
            Assert.IsTrue(tws.GetValue(5));
            Assert.IsFalse(tws.GetValue(6));
            Assert.IsFalse(tws.GetValue(7));
        }
    }
}
