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
    public class PlusDMIndicatorTest
    {

        [TestMethod]
        public void zeroDirectionalMovement()
        {
            MockBar yesterdayBar = new MockBar(0, 0, 10, 2);
            MockBar todayBar = new MockBar(0, 0, 6, 6);
            List<IBar> bars = new List<IBar>();
            bars.Add(yesterdayBar);
            bars.Add(todayBar);
            MockTimeSeries series = new MockTimeSeries(bars);
            PlusDMIndicator dup = new PlusDMIndicator(series);
            Assert.AreEqual(dup.GetValue(1), 0);
        }

        [TestMethod]
        public void zeroDirectionalMovement2()
        {
            MockBar yesterdayBar = new MockBar(0, 0, 6, 12);
            MockBar todayBar = new MockBar(0, 0, 12, 6);
            List<IBar> bars = new List<IBar>();
            bars.Add(yesterdayBar);
            bars.Add(todayBar);
            MockTimeSeries series = new MockTimeSeries(bars);
            PlusDMIndicator dup = new PlusDMIndicator(series);
            Assert.AreEqual(dup.GetValue(1), 0);
        }

        [TestMethod]
        public void zeroDirectionalMovement3()
        {
            MockBar yesterdayBar = new MockBar(0, 0, 6, 20);
            MockBar todayBar = new MockBar(0, 0, 12, 4);
            List<IBar> bars = new List<IBar>();
            bars.Add(yesterdayBar);
            bars.Add(todayBar);
            MockTimeSeries series = new MockTimeSeries(bars);
            PlusDMIndicator dup = new PlusDMIndicator(series);
            Assert.AreEqual(dup.GetValue(1), 0);
        }

        [TestMethod]
        public void positiveDirectionalMovement()
        {
            MockBar yesterdayBar = new MockBar(0, 0, 6, 6);
            MockBar todayBar = new MockBar(0, 0, 12, 4);
            List<IBar> bars = new List<IBar>();
            bars.Add(yesterdayBar);
            bars.Add(todayBar);
            MockTimeSeries series = new MockTimeSeries(bars);
            PlusDMIndicator dup = new PlusDMIndicator(series);
            Assert.AreEqual(dup.GetValue(1), 6);
        }
    }
}