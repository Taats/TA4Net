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
    public class MVWAPIndicatorTest
    {
        protected ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {

            List<IBar> bars = new List<IBar>();
            bars.Add(new MockBar(44.98M, 45.05M, 45.17M, 44.96M, 1));
            bars.Add(new MockBar(45.05M, 45.10M, 45.15M, 44.99M, 2));
            bars.Add(new MockBar(45.11M, 45.19M, 45.32M, 45.11M, 1));
            bars.Add(new MockBar(45.19M, 45.14M, 45.25M, 45.04M, 3));
            bars.Add(new MockBar(45.12M, 45.15M, 45.20M, 45.10M, 1));
            bars.Add(new MockBar(45.15M, 45.14M, 45.20M, 45.10M, 2));
            bars.Add(new MockBar(45.13M, 45.10M, 45.16M, 45.07M, 1));
            bars.Add(new MockBar(45.12M, 45.15M, 45.22M, 45.10M, 5));
            bars.Add(new MockBar(45.15M, 45.22M, 45.27M, 45.14M, 1));
            bars.Add(new MockBar(45.24M, 45.43M, 45.45M, 45.20M, 1));
            bars.Add(new MockBar(45.43M, 45.44M, 45.50M, 45.39M, 1));
            bars.Add(new MockBar(45.43M, 45.55M, 45.60M, 45.35M, 5));
            bars.Add(new MockBar(45.58M, 45.55M, 45.61M, 45.39M, 7));
            bars.Add(new MockBar(45.45M, 45.01M, 45.55M, 44.80M, 6));
            bars.Add(new MockBar(45.03M, 44.23M, 45.04M, 44.17M, 1));
            bars.Add(new MockBar(44.23M, 43.95M, 44.29M, 43.81M, 2));
            bars.Add(new MockBar(43.91M, 43.08M, 43.99M, 43.08M, 1));
            bars.Add(new MockBar(43.07M, 43.55M, 43.65M, 43.06M, 7));
            bars.Add(new MockBar(43.56M, 43.95M, 43.99M, 43.53M, 6));
            bars.Add(new MockBar(43.93M, 44.47M, 44.58M, 43.93M, 1));
            data = new MockTimeSeries(bars);
        }

        [TestMethod]
        public void mvwap()
        {
            VWAPIndicator vwap = new VWAPIndicator(data, 5);
            MVWAPIndicator mvwap = new MVWAPIndicator(vwap, 8);

            Assert.AreEqual(mvwap.GetValue(8), 45.127078869047619047619047619M);
            Assert.AreEqual(mvwap.GetValue(9), 45.139870535714285714285714286M);
            Assert.AreEqual(mvwap.GetValue(10), 45.153018683862433862433862435M);
            Assert.AreEqual(mvwap.GetValue(11), 45.179035167378917378917378918M);
            Assert.AreEqual(mvwap.GetValue(12), 45.222722667378917378917378918M);
            Assert.AreEqual(mvwap.GetValue(13), 45.253250445156695156695156695M);
            Assert.AreEqual(mvwap.GetValue(14), 45.276906695156695156695156695M);
            Assert.AreEqual(mvwap.GetValue(15), 45.284396774521774521774521774M);
            Assert.AreEqual(mvwap.GetValue(16), 45.266779127462950992362757069M);
            Assert.AreEqual(mvwap.GetValue(17), 45.138619813737460796284325696M);
            Assert.AreEqual(mvwap.GetValue(18), 44.94873146951088127558715794M);
        }
    }
}