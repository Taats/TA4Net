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
namespace TA4Net.Test.Indicators.helpers
{

    using TA4Net;
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net.Interfaces;

    [TestClass]
    public class GainIndicatorTest
    {

        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            data = new MockTimeSeries(1, 2, 3, 4, 3, 4, 7, 4, 3, 3, 5, 3, 2);
        }

        [TestMethod]
        public void gainUsingClosePrice()
        {
            GainIndicator gain = new GainIndicator(new ClosePriceIndicator(data));
            Assert.AreEqual(gain.GetValue(0), 0);
            Assert.AreEqual(gain.GetValue(1), 1);
            Assert.AreEqual(gain.GetValue(2), 1);
            Assert.AreEqual(gain.GetValue(3), 1);
            Assert.AreEqual(gain.GetValue(4), 0);
            Assert.AreEqual(gain.GetValue(5), 1);
            Assert.AreEqual(gain.GetValue(6), 3);
            Assert.AreEqual(gain.GetValue(7), 0);
            Assert.AreEqual(gain.GetValue(8), 0);
            Assert.AreEqual(gain.GetValue(9), 0);
            Assert.AreEqual(gain.GetValue(10), 2);
            Assert.AreEqual(gain.GetValue(11), 0);
            Assert.AreEqual(gain.GetValue(12), 0);
        }
    }
}