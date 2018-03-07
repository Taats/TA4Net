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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class ParabolicSarIndicatorTest
    {

        [TestMethod]
        public void startUpAndDownTrendTest()
        {
            List<IBar> bars = new List<IBar>();
            bars.Add(new MockBar(0M, 75.1M, 74.06M, 75.11M));
            bars.Add(new MockBar(0M, 75.9M, 76.030000M, 74.640000M));
            bars.Add(new MockBar(0M, 75.24M, 76.269900M, 75.060000M));
            bars.Add(new MockBar(0M, 75.17M, 75.280000M, 74.500000M));
            bars.Add(new MockBar(0M, 74.6M, 75.310000M, 74.540000M));
            bars.Add(new MockBar(0M, 74.1M, 75.467000M, 74.010000M));
            bars.Add(new MockBar(0M, 73.740000M, 74.700000M, 73.546000M));
            bars.Add(new MockBar(0M, 73.390000M, 73.830000M, 72.720000M));
            bars.Add(new MockBar(0M, 73.25M, 73.890000M, 72.86M));
            bars.Add(new MockBar(0M, 74.36M, 74.410000M, 73M, 26M));

            bars.Add(new MockBar(0M, 76.510000M, 76.830000M, 74.820000M));
            bars.Add(new MockBar(0M, 75.590000M, 76.850000M, 74.540000M));
            bars.Add(new MockBar(0M, 75.910000M, 76.960000M, 75.510000M));
            bars.Add(new MockBar(0M, 74.610000M, 77.070000M, 74.560000M));
            bars.Add(new MockBar(0M, 75.330000M, 75.530000M, 74.010000M));
            bars.Add(new MockBar(0M, 75.010000M, 75.500000M, 74.510000M));
            bars.Add(new MockBar(0M, 75.620000M, 76.210000M, 75.250000M));
            bars.Add(new MockBar(0M, 76.040000M, 76.460000M, 75.092800M));
            bars.Add(new MockBar(0M, 76.450000M, 76.450000M, 75.435000M));
            bars.Add(new MockBar(0M, 76.260000M, 76.470000M, 75.840000M));
            bars.Add(new MockBar(0M, 76.850000M, 77.000000M, 76.190000M));


            ParabolicSarIndicator sar = new ParabolicSarIndicator(new MockTimeSeries(bars));

            Assert.AreEqual(sar.GetValue(0), Decimals.NaN);
            Assert.AreEqual(sar.GetValue(1), 74.64000000M);
            Assert.AreEqual(sar.GetValue(2), 74.64000000M); // start with up trend
            Assert.AreEqual(sar.GetValue(3), 76.26990000M); // switch to downtrend
            Assert.AreEqual(sar.GetValue(4), 76.23450200M); // hold trend...
            Assert.AreEqual(sar.GetValue(5), 76.20061196M);
            Assert.AreEqual(sar.GetValue(6), 76.112987481600M);
            Assert.AreEqual(sar.GetValue(7), 75.95896823270400M);
            Assert.AreEqual(sar.GetValue(8), 75.6998507740876800M);
            Assert.AreEqual(sar.GetValue(9), 75.461462712160665600M); // switch to up trend
            Assert.AreEqual(sar.GetValue(10), 72.720000M);// hold trend
            Assert.AreEqual(sar.GetValue(11), 72.80220000M);
            Assert.AreEqual(sar.GetValue(12), 72.9641120000M);
            Assert.AreEqual(sar.GetValue(13), 73.203865280000M);
            Assert.AreEqual(sar.GetValue(14), 73.51315605760000M);
            Assert.AreEqual(sar.GetValue(15), 73.7977035729920000M);
            Assert.AreEqual(sar.GetValue(16), 74.059487287152640000M);
            Assert.AreEqual(sar.GetValue(17), 74.30032830418042880000M);
            Assert.AreEqual(sar.GetValue(18), 74.5219020398459944960000M);
            Assert.AreEqual(sar.GetValue(19), 74.725749876658314936320000M);
            Assert.AreEqual(sar.GetValue(20), 74.91328988652564974141440000M);
        }

    }
}