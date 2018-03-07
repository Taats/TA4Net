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
namespace TA4Net.Test.Indicators.volume
{
    using TA4Net;
    using TA4Net.Indicators.Volume;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class AccumulationDistributionIndicatorTest
    {

        [TestMethod]
        public void accumulationDistribution()
        {
            DateTime now = DateTime.Now;
            List<IBar> bars = new List<IBar>();
            bars.Add(new MockBar(now, 0M, 10, 12, 8, 0, 200, 0));//2-2 * 200 / 4
            bars.Add(new MockBar(now, 0M, 8, 10, 7, 0, 100, 0));//1-2 *100 / 3
            bars.Add(new MockBar(now, 0M, 9, 15, 6, 0, 300, 0));//3-6 *300 /9
            bars.Add(new MockBar(now, 0M, 20, 40, 5, 0, 50, 0));//15-20 *50 / 35
            bars.Add(new MockBar(now, 0M, 30, 30, 3, 0, 600, 0));//27-0 *600 /27

            ITimeSeries series = new MockTimeSeries(bars);
            AccumulationDistributionIndicator ac = new AccumulationDistributionIndicator(series);
            Assert.AreEqual(ac.GetValue(0), 0);
            Assert.AreEqual(ac.GetValue(1), -33.333333333333333333333333330M);
            Assert.AreEqual(ac.GetValue(2), -133.33333333333333333333333332M);
            Assert.AreEqual(ac.GetValue(3), -140.47619047619047619047619046M);
            Assert.AreEqual(ac.GetValue(4), 459.52380952380952380952380954M);
        }
    }
}