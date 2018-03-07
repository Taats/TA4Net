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
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class ROCVIndicatorTest
    {

        ITimeSeries series;

        [TestInitialize]
        public void setUp()
        {
            List<IBar> bars = new List<IBar>();
            bars.Add(new MockBar(1355.69M, 1000));
            bars.Add(new MockBar(1325.51M, 3000));
            bars.Add(new MockBar(1335.02M, 3500));
            bars.Add(new MockBar(1313.72M, 2200));
            bars.Add(new MockBar(1319.99M, 2300));
            bars.Add(new MockBar(1331.85M, 200));
            bars.Add(new MockBar(1329.04M, 2700));
            bars.Add(new MockBar(1362.16M, 5000));
            bars.Add(new MockBar(1365.51M, 1000));
            bars.Add(new MockBar(1374.02M, 2500));
            series = new MockTimeSeries(bars);
        }

        [TestMethod]
        public void test()
        {
            ROCVIndicator roc = new ROCVIndicator(series, 3);

            Assert.AreEqual(roc.GetValue(0), 0);
            Assert.AreEqual(roc.GetValue(1), 200);
            Assert.AreEqual(roc.GetValue(2), 250);
            Assert.AreEqual(roc.GetValue(3), 120);
            Assert.AreEqual(roc.GetValue(4), -23.333333333333333333333333330M);
            Assert.AreEqual(roc.GetValue(5), -94.28571428571428571428571429M);
            Assert.AreEqual(roc.GetValue(6), 22.727272727272727272727272730M);
            Assert.AreEqual(roc.GetValue(7), 117.39130434782608695652173913M);
            Assert.AreEqual(roc.GetValue(8), 400M);
            Assert.AreEqual(roc.GetValue(9), -7.4074074074074074074074074100M);
        }
    }
}