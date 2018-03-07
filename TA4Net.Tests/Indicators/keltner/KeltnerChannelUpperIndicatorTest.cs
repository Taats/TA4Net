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
namespace TA4Net.Test.Indicators.keltner
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System.Collections.Generic;
    using TA4Net;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Indicators.Keltner;
    using TA4Net.Interfaces;

    [TestClass]
    public class KeltnerChannelUpperIndicatorTest
    {

        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {

            List<IBar> bars = new List<IBar>();
            bars.Add(new MockBar(11577.43M, 11670.75M, 11711.47M, 11577.35M));
            bars.Add(new MockBar(11670.90M, 11691.18M, 11698.22M, 11635.74M));
            bars.Add(new MockBar(11688.61M, 11722.89M, 11742.68M, 11652.89M));
            bars.Add(new MockBar(11716.93M, 11697.31M, 11736.74M, 11667.46M));
            bars.Add(new MockBar(11696.86M, 11674.76M, 11726.94M, 11599.68M));
            bars.Add(new MockBar(11672.34M, 11637.45M, 11677.33M, 11573.87M));
            bars.Add(new MockBar(11638.51M, 11671.88M, 11704.12M, 11635.48M));
            bars.Add(new MockBar(11673.62M, 11755.44M, 11782.23M, 11673.62M));
            bars.Add(new MockBar(11753.70M, 11731.90M, 11757.25M, 11700.53M));
            bars.Add(new MockBar(11732.13M, 11787.38M, 11794.15M, 11698.83M));
            bars.Add(new MockBar(11783.82M, 11837.93M, 11858.78M, 11777.99M));
            bars.Add(new MockBar(11834.21M, 11825.29M, 11861.24M, 11798.46M));
            bars.Add(new MockBar(11823.70M, 11822.80M, 11845.16M, 11744.77M));
            bars.Add(new MockBar(11822.95M, 11871.84M, 11905.48M, 11822.80M));
            bars.Add(new MockBar(11873.43M, 11980.52M, 11982.94M, 11867.98M));
            bars.Add(new MockBar(11980.52M, 11977.19M, 11985.97M, 11898.74M));
            bars.Add(new MockBar(11978.85M, 11985.44M, 12020.52M, 11961.83M));
            bars.Add(new MockBar(11985.36M, 11989.83M, 12019.53M, 11971.93M));
            bars.Add(new MockBar(11824.39M, 11891.93M, 11891.93M, 11817.88M));
            bars.Add(new MockBar(11892.50M, 12040.16M, 12050.75M, 11892.50M));
            bars.Add(new MockBar(12038.27M, 12041.97M, 12057.91M, 12018.51M));
            bars.Add(new MockBar(12040.68M, 12062.26M, 12080.54M, 11981.05M));
            bars.Add(new MockBar(12061.73M, 12092.15M, 12092.42M, 12025.78M));
            bars.Add(new MockBar(12092.38M, 12161.63M, 12188.76M, 12092.30M));
            bars.Add(new MockBar(12152.70M, 12233.15M, 12238.79M, 12150.05M));
            bars.Add(new MockBar(12229.29M, 12239.89M, 12254.23M, 12188.19M));
            bars.Add(new MockBar(12239.66M, 12229.29M, 12239.66M, 12156.94M));
            bars.Add(new MockBar(12227.78M, 12273.26M, 12285.94M, 12180.48M));
            bars.Add(new MockBar(12266.83M, 12268.19M, 12276.21M, 12235.91M));
            bars.Add(new MockBar(12266.75M, 12226.64M, 12267.66M, 12193.27M));
            bars.Add(new MockBar(12219.79M, 12288.17M, 12303.16M, 12219.79M));
            bars.Add(new MockBar(12287.72M, 12318.14M, 12331.31M, 12253.24M));

            data = new MockTimeSeries(bars);
        }

        [TestMethod]
        public void keltnerChannelUpperIndicatorTest()
        {
            KeltnerChannelMiddleIndicator km = new KeltnerChannelMiddleIndicator(new ClosePriceIndicator(data), 14);
            KeltnerChannelUpperIndicator ku = new KeltnerChannelUpperIndicator(km, 2M, 14);

            Assert.AreEqual(ku.GetValue(13), 11971.913257254422225172757246M);
            Assert.AreEqual(ku.GetValue(14), 12002.340260044316534210405010M);
            Assert.AreEqual(ku.GetValue(15), 12024.403226479571320681555782M);
            Assert.AreEqual(ku.GetValue(16), 12040.393392549089969597086111M);
            Assert.AreEqual(ku.GetValue(17), 12052.857255790282692109135888M);
            Assert.AreEqual(ku.GetValue(18), 12067.905057581620809824936557M);
            Assert.AreEqual(ku.GetValue(19), 12099.502526093872715893129128M);
            Assert.AreEqual(ku.GetValue(20), 12110.572274314743319196644777M);
            Assert.AreEqual(ku.GetValue(21), 12130.867526222827344529505992M);
            Assert.AreEqual(ku.GetValue(22), 12147.734490603877561635194247M);
            Assert.AreEqual(ku.GetValue(23), 12175.593766790495331004674848M);
            Assert.AreEqual(ku.GetValue(24), 12208.132767466483008916259954M);
            Assert.AreEqual(ku.GetValue(25), 12233.903270225097063912571205M);
            Assert.AreEqual(ku.GetValue(26), 12256.959691300123640896149582M);
            Assert.AreEqual(ku.GetValue(27), 12285.909480724310518174399327M);
            Assert.AreEqual(ku.GetValue(28), 12301.110887634972238287139367M);
            Assert.AreEqual(ku.GetValue(29), 12313.204201885600267917990738M);
        }
    }
}