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
namespace TA4Net.Test.Indicators.statistics
{

    using TA4Net;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Indicators.Statistics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class PearsonCorrelationIndicatorTest
    {

        private IIndicator<decimal> close, volume;

        [TestInitialize]
        public void setUp()
        {
            List<IBar> bars = new List<IBar>();
            // close, volume
            bars.Add(new MockBar(6, 100));
            bars.Add(new MockBar(7, 105));
            bars.Add(new MockBar(9, 130));
            bars.Add(new MockBar(12, 160));
            bars.Add(new MockBar(11, 150));
            bars.Add(new MockBar(10, 130));
            bars.Add(new MockBar(11, 95));
            bars.Add(new MockBar(13, 120));
            bars.Add(new MockBar(15, 180));
            bars.Add(new MockBar(12, 160));
            bars.Add(new MockBar(8, 150));
            bars.Add(new MockBar(4, 200));
            bars.Add(new MockBar(3, 150));
            bars.Add(new MockBar(4, 85));
            bars.Add(new MockBar(3, 70));
            bars.Add(new MockBar(5, 90));
            bars.Add(new MockBar(8, 100));
            bars.Add(new MockBar(9, 95));
            bars.Add(new MockBar(11, 110));
            bars.Add(new MockBar(10, 95));

            ITimeSeries data = new BaseTimeSeries(bars);
            close = new ClosePriceIndicator(data);
            volume = new VolumeIndicator(data, 2);
        }

        [TestMethod]
        public void test()
        {
            PearsonCorrelationIndicator coef = new PearsonCorrelationIndicator(close, volume, 5);

            Assert.AreEqual(coef.GetValue(1), 0.9494746905847681902385330589M);
            Assert.AreEqual(coef.GetValue(2), 0.9640797490298871967708656559M);
            Assert.AreEqual(coef.GetValue(3), 0.9666189661412723170171123172M);
            Assert.AreEqual(coef.GetValue(4), 0.9219445959464780324897279404M);
            Assert.AreEqual(coef.GetValue(5), 0.9204686084830691879429314265M);
            Assert.AreEqual(coef.GetValue(6), 0.4565079193666946530704545434M);
            Assert.AreEqual(coef.GetValue(7), -0.4622368993375032370867375609M);
            Assert.AreEqual(coef.GetValue(8), 0.0574674682540454585943187237M);
            Assert.AreEqual(coef.GetValue(9), 0.1442072366721843231551720267M);
            Assert.AreEqual(coef.GetValue(10), -0.1262800365682874150793719821M);
            Assert.AreEqual(coef.GetValue(11), -0.5345125968416236818993319855M);
            Assert.AreEqual(coef.GetValue(12), -0.7275265284764913369741980078M);
            Assert.AreEqual(coef.GetValue(13), 0.1676203622600160056069374939M);
            Assert.AreEqual(coef.GetValue(14), 0.2506093635823103767503572818M);
            Assert.AreEqual(coef.GetValue(15), -0.2937644826001439430561912446M);
            Assert.AreEqual(coef.GetValue(16), -0.3585679550668379351786236965M);
            Assert.AreEqual(coef.GetValue(17), 0.1713296125780531390600177618M);
            Assert.AreEqual(coef.GetValue(18), 0.9841040262836171107084622525M);
            Assert.AreEqual(coef.GetValue(19), 0.9799146648100918552300686205M);
        }
    }
}