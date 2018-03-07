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
    public class MassIndexIndicatorTest
    {

        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            List<IBar> bars = new List<IBar>();
            // open, close, high, low
            bars.Add(new MockBar(44.98M, 45.05M, 45.17M, 44.96M));
            bars.Add(new MockBar(45.05M, 45.10M, 45.15M, 44.99M));
            bars.Add(new MockBar(45.11M, 45.19M, 45.32M, 45.11M));
            bars.Add(new MockBar(45.19M, 45.14M, 45.25M, 45.04M));
            bars.Add(new MockBar(45.12M, 45.15M, 45.20M, 45.10M));
            bars.Add(new MockBar(45.15M, 45.14M, 45.20M, 45.10M));
            bars.Add(new MockBar(45.13M, 45.10M, 45.16M, 45.07M));
            bars.Add(new MockBar(45.12M, 45.15M, 45.22M, 45.10M));
            bars.Add(new MockBar(45.15M, 45.22M, 45.27M, 45.14M));
            bars.Add(new MockBar(45.24M, 45.43M, 45.45M, 45.20M));
            bars.Add(new MockBar(45.43M, 45.44M, 45.50M, 45.39M));
            bars.Add(new MockBar(45.43M, 45.55M, 45.60M, 45.35M));
            bars.Add(new MockBar(45.58M, 45.55M, 45.61M, 45.39M));
            bars.Add(new MockBar(45.45M, 45.01M, 45.55M, 44.80M));
            bars.Add(new MockBar(45.03M, 44.23M, 45.04M, 44.17M));
            bars.Add(new MockBar(44.23M, 43.95M, 44.29M, 43.81M));
            bars.Add(new MockBar(43.91M, 43.08M, 43.99M, 43.08M));
            bars.Add(new MockBar(43.07M, 43.55M, 43.65M, 43.06M));
            bars.Add(new MockBar(43.56M, 43.95M, 43.99M, 43.53M));
            bars.Add(new MockBar(43.93M, 44.47M, 44.58M, 43.93M));

            data = new BaseTimeSeries(bars);
        }

        [TestMethod]
        public void massIndexUsing3And8TimeFrames()
        {
            MassIndexIndicator massIndex = new MassIndexIndicator(data, 3, 8);

            Assert.AreEqual(massIndex.GetValue(0), 1M);
            Assert.AreEqual(massIndex.GetValue(14), 9.115829365463941053935910885M);
            Assert.AreEqual(massIndex.GetValue(15), 9.246164705004149799504218758M);
            Assert.AreEqual(massIndex.GetValue(16), 9.402602394695373641121440309M);
            Assert.AreEqual(massIndex.GetValue(17), 9.212873588690283960659225404M);
            Assert.AreEqual(massIndex.GetValue(18), 9.157597903430717027923668930M);
            Assert.AreEqual(massIndex.GetValue(19), 9.018409083490046411626762552M);
        }
    }
}