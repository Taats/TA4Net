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
    public class CovarianceIndicatorTest
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
        public void usingTimeFrame5UsingClosePriceAndVolume()
        {
            CovarianceIndicator covar = new CovarianceIndicator(close, volume, 5);

            Assert.AreEqual(covar.GetValue(0), 0M);
            Assert.AreEqual(covar.GetValue(1), 26.25M);
            Assert.AreEqual(covar.GetValue(2), 63.333333333333333333333333333M);
            Assert.AreEqual(covar.GetValue(3), 143.75M);
            Assert.AreEqual(covar.GetValue(4), 156M);
            Assert.AreEqual(covar.GetValue(5), 60.8M);
            Assert.AreEqual(covar.GetValue(6), 15.2M);
            Assert.AreEqual(covar.GetValue(7), -17.6M);
            Assert.AreEqual(covar.GetValue(8), 4M);
            Assert.AreEqual(covar.GetValue(9), 11.6M);
            Assert.AreEqual(covar.GetValue(10), -14.4M);
            Assert.AreEqual(covar.GetValue(11), -100.2M);
            Assert.AreEqual(covar.GetValue(12), -70.0M);
            Assert.AreEqual(covar.GetValue(13), 24.6M);
            Assert.AreEqual(covar.GetValue(14), 35.0M);
            Assert.AreEqual(covar.GetValue(15), -19.0M);
            Assert.AreEqual(covar.GetValue(16), -47.8M);
            Assert.AreEqual(covar.GetValue(17), 11.4M);
            Assert.AreEqual(covar.GetValue(18), 55.8M);
            Assert.AreEqual(covar.GetValue(19), 33.4M);
        }

        [TestMethod]
        public void firstValueShouldBeZero()
        {
            CovarianceIndicator covar = new CovarianceIndicator(close, volume, 5);
            Assert.AreEqual(covar.GetValue(0), 0);
        }

        [TestMethod]
        public void shouldBeZeroWhenTimeFrameIs1()
        {
            CovarianceIndicator covar = new CovarianceIndicator(close, volume, 1);
            Assert.AreEqual(covar.GetValue(3), 0);
            Assert.AreEqual(covar.GetValue(8), 0);
        }
    }
}
