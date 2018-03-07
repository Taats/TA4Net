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
    public class BearishHaramiIndicatorTest
    {

        private ITimeSeries series;

        [TestInitialize]
        public void setUp()
        {
            List<IBar> bars = new List<IBar>();
            // open, close, high, low
            bars.Add(new MockBar(10, 18, 20, 10));
            bars.Add(new MockBar(15, 18, 19, 14));
            bars.Add(new MockBar(17, 16, 19, 15));
            bars.Add(new MockBar(15, 11, 15, 8));
            bars.Add(new MockBar(11, 12, 12, 10));
            series = new MockTimeSeries(bars);
        }

        [TestMethod]
        public void getValue()
        {
            BearishHaramiIndicator bhp = new BearishHaramiIndicator(series);
            Assert.IsFalse(bhp.GetValue(0));
            Assert.IsFalse(bhp.GetValue(1));
            Assert.IsTrue(bhp.GetValue(2));
            Assert.IsFalse(bhp.GetValue(3));
            Assert.IsFalse(bhp.GetValue(4));
        }
    }
}
