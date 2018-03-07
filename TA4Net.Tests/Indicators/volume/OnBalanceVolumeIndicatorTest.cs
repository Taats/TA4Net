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
    using System;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class OnBalanceVolumeIndicatorTest
    {

        [TestMethod]
        public void getValue()
        {
            DateTime now = DateTime.Now;
            List<IBar> bars = new List<IBar>();
            bars.Add(new MockBar(now, 0, 10, 0, 0, 0, 4, 0));
            bars.Add(new MockBar(now, 0, 5, 0, 0, 0, 2, 0));
            bars.Add(new MockBar(now, 0, 6, 0, 0, 0, 3, 0));
            bars.Add(new MockBar(now, 0, 7, 0, 0, 0, 8, 0));
            bars.Add(new MockBar(now, 0, 7, 0, 0, 0, 6, 0));
            bars.Add(new MockBar(now, 0, 6, 0, 0, 0, 10, 0));

            OnBalanceVolumeIndicator obv = new OnBalanceVolumeIndicator(new MockTimeSeries(bars));
            Assert.AreEqual(obv.GetValue(0), 0);
            Assert.AreEqual(obv.GetValue(1), -2);
            Assert.AreEqual(obv.GetValue(2), 1);
            Assert.AreEqual(obv.GetValue(3), 9);
            Assert.AreEqual(obv.GetValue(4), 9);
            Assert.AreEqual(obv.GetValue(5), -1);
        }

        [TestMethod]
        public void stackOverflowError()
        {
            List<IBar> bigListOfBars = new List<IBar>();
            for (int i = 0; i < 10000; i++)
            {
                bigListOfBars.Add(new MockBar(i, i, i,i,i));
            }
            MockTimeSeries bigSeries = new MockTimeSeries(bigListOfBars);
            OnBalanceVolumeIndicator obv = new OnBalanceVolumeIndicator(bigSeries);
            // If a StackOverflowError is thrown here, then the RecursiveCachedIndicator
            // does not work as intended.
            Assert.AreEqual(obv.GetValue(9999), 49995000);
        }
    }
}