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
    public class ThreeBlackCrowsIndicatorTest
    {

        private ITimeSeries series;

        [TestInitialize]
        public void setUp()
        {
            List<IBar> bars = new List<IBar>();
            // open, close, high, low
            bars.Add(new MockBar(19, 19, 22, 15));
            bars.Add(new MockBar(10, 18, 20, 8));
            bars.Add(new MockBar(17, 20, 21, 17));
            bars.Add(new MockBar(19, 17, 20, 16.9M));
            bars.Add(new MockBar(17.5M, 14, 18, 13.9M));
            bars.Add(new MockBar(15, 11, 15, 11));
            bars.Add(new MockBar(12, 14, 15, 8));
            bars.Add(new MockBar(13, 16, 16, 11));
            series = new MockTimeSeries(bars);
        }

        [TestMethod]
        public void getValue()
        {
            ThreeBlackCrowsIndicator tbc = new ThreeBlackCrowsIndicator(series, 3, 0.1M);
            Assert.IsFalse(tbc.GetValue(0));
            Assert.IsFalse(tbc.GetValue(1));
            Assert.IsFalse(tbc.GetValue(2));
            Assert.IsFalse(tbc.GetValue(3));
            Assert.IsFalse(tbc.GetValue(4));
            Assert.IsTrue(tbc.GetValue(5));
            Assert.IsFalse(tbc.GetValue(6));
            Assert.IsFalse(tbc.GetValue(7));
        }
    }
}