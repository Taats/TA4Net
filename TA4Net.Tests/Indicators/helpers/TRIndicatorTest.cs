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
    using TA4Net.Mocks;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class TRIndicatorTest
    {

        [TestMethod]
        public void getValue()
        {
            List<IBar> bars = new List<IBar>();
            bars.Add(new MockBar(0, 12, 15, 8));
            bars.Add(new MockBar(0, 8, 11, 6));
            bars.Add(new MockBar(0, 15, 17, 14));
            bars.Add(new MockBar(0, 15, 17, 14));
            bars.Add(new MockBar(0, 0, 0, 2));
            TRIndicator tr = new TRIndicator(new MockTimeSeries(bars));

            Assert.AreEqual(tr.GetValue(0), 7);
            Assert.AreEqual(tr.GetValue(1), 6);
            Assert.AreEqual(tr.GetValue(2), 9);
            Assert.AreEqual(tr.GetValue(3), 3);
            Assert.AreEqual(tr.GetValue(4), 15);
        }
    }
}