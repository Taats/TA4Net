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

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System.Collections.Generic;
    using TA4Net;
    using TA4Net.Indicators;
    using TA4Net.Interfaces;

    /**
     * The Class RandomWalkIndexLowIndicatorTest.
     */
    [TestClass]
    public class RandomWalkIndexLowIndicatorTest
    {

        protected ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {

            List<IBar> bars = new List<IBar>();
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
            data = new MockTimeSeries(bars);
        }

        [TestMethod]
        public void randomWalkIndexLow()
        {
            RandomWalkIndexLowIndicator rwil = new RandomWalkIndexLowIndicator(data, 5);

            Assert.AreEqual(rwil.GetValue(6), 0.2355888464820563559020980749M);
            Assert.AreEqual(rwil.GetValue(7), 0.6762461001421318167059521473M);
            Assert.AreEqual(rwil.GetValue(8), 0.3454795045766873576394229284M);
            Assert.AreEqual(rwil.GetValue(9), 0.0000M);
            Assert.AreEqual(rwil.GetValue(10), -0.5548887714433306122342565818M);
            Assert.AreEqual(rwil.GetValue(11), -0.4925697521008913780435649751M);
            Assert.AreEqual(rwil.GetValue(12), -0.4177184078424059762621387867M);
            Assert.AreEqual(rwil.GetValue(13), 0.7110563738803661980608200975M);
            Assert.AreEqual(rwil.GetValue(14), 1.3945382648692284751837452284M);
            Assert.AreEqual(rwil.GetValue(15), 1.7809049561817756861328874028M);
            Assert.AreEqual(rwil.GetValue(16), 2.1609959611098659975768740505M);
            Assert.AreEqual(rwil.GetValue(17), 2.1307544585105761209601444773M);
            Assert.AreEqual(rwil.GetValue(18), 1.7366997825226263947042190399M);
        }
    }
}