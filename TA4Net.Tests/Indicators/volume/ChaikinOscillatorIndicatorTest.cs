/**
 * The MIT License (MIT)
 *
 * Copyright (c) 2014-2017 Marc de Verdelhan & respective authors (see AUTHORS)
 *
 * Permission is hereby granteM, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
 * the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KINM, EXPRESS OR
 * IMPLIEM, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
namespace TA4Net.Test.Indicators.volume
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net;
    using TA4Net;
    using TA4Net.Test.Indicators.ChaikinOscillatorIndicator;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Mocks;
    using System.Collections.Generic;

    using TA4Net.Analysis.Criteria;

    [TestClass]
    public class ChaikinOscillatorIndicatorTest
    {

        [TestMethod]
        public void getValue()
        {

            List<IBar> bars = new List<IBar>();

            bars.Add(new MockBar(12.915, 13.600, 12.890, 13.550, 264266));
            bars.Add(new MockBar(13.550, 13.770, 13.310, 13.505, 305427));
            bars.Add(new MockBar(13.510, 13.590, 13.425, 13.490, 104077));
            bars.Add(new MockBar(13.515, 13.545, 13.400, 13.480, 136135));
            bars.Add(new MockBar(13.490, 13.495, 13.310, 13.345, 92090));
            bars.Add(new MockBar(13.350, 13.490, 13.325, 13.420, 80948));
            bars.Add(new MockBar(13.415, 13.460, 13.290, 13.300, 82983));
            bars.Add(new MockBar(13.320, 13.320, 13.090, 13.130, 126918));
            bars.Add(new MockBar(13.145, 13.225, 13.090, 13.150, 68560));
            bars.Add(new MockBar(13.150, 13.250, 13.110, 13.245, 41178));
            bars.Add(new MockBar(13.245, 13.250, 13.120, 13.210, 63606));
            bars.Add(new MockBar(13.210, 13.275, 13.185, 13.275, 34402));
            TimeSeries series = new MockTimeSeries(bars);

            ChaikinOscillatorIndicator co = new ChaikinOscillatorIndicator(series);
            Assert.AreEqual(co.getValue(0), 0.0);
            Assert.AreEqual(co.getValue(1), 0.0);
            Assert.AreEqual(co.getValue(2), -165349.14743589723);
            Assert.AreEqual(co.getValue(3), -337362.31490384537);
            Assert.AreEqual(co.getValue(4), -662329.9816620838);
            Assert.AreEqual(co.getValue(5), -836710.5421757463);
            Assert.AreEqual(co.getValue(6), -1847749.1562169262);
            Assert.AreEqual(co.getValue(7), -2710068.6997245993);
            Assert.AreEqual(co.getValue(8), -3069157.9046621257);
            Assert.AreEqual(co.getValue(9), -2795286.881074371);
            Assert.AreEqual(co.getValue(9), -2795286.881074371);
            Assert.AreEqual(co.getValue(9), -2795286.881074371);
        }
    }
