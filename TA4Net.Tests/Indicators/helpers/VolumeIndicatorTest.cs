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
namespace TA4Net.Test.Indicators.helpers
{

    using TA4Net;
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class VolumeIndicatorTest
    {

        [TestMethod]
        public void indicatorShouldRetrieveBarVolume()
        {
            ITimeSeries series = new MockTimeSeries();
            VolumeIndicator volumeIndicator = new VolumeIndicator(series);
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(volumeIndicator.GetValue(i), series.GetBar(i).Volume);
            }
        }

        [TestMethod]
        public void sumOfVolume()
        {
            List<IBar> bars = new List<IBar>();
            bars.Add(new MockBar(0, 10));
            bars.Add(new MockBar(0, 11));
            bars.Add(new MockBar(0, 12));
            bars.Add(new MockBar(0, 13));
            bars.Add(new MockBar(0, 150));
            bars.Add(new MockBar(0, 155));
            bars.Add(new MockBar(0, 160));
            VolumeIndicator volumeIndicator = new VolumeIndicator(new MockTimeSeries(bars), 3);

            Assert.AreEqual(volumeIndicator.GetValue(0), 10);
            Assert.AreEqual(volumeIndicator.GetValue(1), 21);
            Assert.AreEqual(volumeIndicator.GetValue(2), 33);
            Assert.AreEqual(volumeIndicator.GetValue(3), 36);
            Assert.AreEqual(volumeIndicator.GetValue(4), 175);
            Assert.AreEqual(volumeIndicator.GetValue(5), 318);
            Assert.AreEqual(volumeIndicator.GetValue(6), 465);
        }
    }
}