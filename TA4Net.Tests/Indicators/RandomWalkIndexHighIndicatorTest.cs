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
     * The Class RandomWalkIndexHighIndicatorTest.
     */
    [TestClass]
    public class RandomWalkIndexHighIndicatorTest
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
        public void randomWalkIndexHigh()
        {
            RandomWalkIndexHighIndicator rwih = new RandomWalkIndexHighIndicator(data, 5);


            Assert.AreEqual(rwih.GetValue(6), 0.5006262987743697562919584092M);
            Assert.AreEqual(rwih.GetValue(7), 0.3381230500710659083529760736M);
            Assert.AreEqual(rwih.GetValue(8), 0.7223662368421644750642479413M);
            Assert.AreEqual(rwih.GetValue(9), 0.9549226380057807567194967401M);
            Assert.AreEqual(rwih.GetValue(10), 1.1681868872491170783879085932M);
            Assert.AreEqual(rwih.GetValue(11), 1.3740103611235391071741549306M);
            Assert.AreEqual(rwih.GetValue(12), 1.25315522352721792878641636M);
            Assert.AreEqual(rwih.GetValue(13), 0.6202832197679790238402898723M);
            Assert.AreEqual(rwih.GetValue(14), -0.1743172831086535593979681536M);
            Assert.AreEqual(rwih.GetValue(15), -1.1591689063905048844651929841M);
            Assert.AreEqual(rwih.GetValue(16), -1.1662517885354832367875193289M);
            Assert.AreEqual(rwih.GetValue(17), -1.4539265716895695884198632904M);
            Assert.AreEqual(rwih.GetValue(18), -0.6963994177442214751041670408M);
        }
    }
}
