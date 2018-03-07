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
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net.Interfaces;

    [TestClass]
    public class DoubleEMAIndicatorTest
    {

        private ClosePriceIndicator closePrice;

        [TestInitialize]
        public void setUp()
        {
            ITimeSeries data = new MockTimeSeries(
                    0.73M, 0.72M, 0.86M, 0.72M, 0.62M,
                    0.76M, 0.84M, 0.69M, 0.65M, 0.71M,
                    0.53M, 0.73M, 0.77M, 0.67M, 0.68M
            );
            closePrice = new ClosePriceIndicator(data);
        }

        [TestMethod]
        public void doubleEMAUsingTimeFrame5UsingClosePrice()
        {
            DoubleEMAIndicator doubleEma = new DoubleEMAIndicator(closePrice, 5);

            Assert.AreEqual(doubleEma.GetValue(0), 0.73M);
            Assert.AreEqual(doubleEma.GetValue(1), 0.7244444444444444444444444445M);
            Assert.AreEqual(doubleEma.GetValue(2), 0.7992592592592592592592592592M);
            Assert.AreEqual(doubleEma.GetValue(6), 0.7858984910836762688614540467M);
            Assert.AreEqual(doubleEma.GetValue(7), 0.7374500838286846517299192197M);
            Assert.AreEqual(doubleEma.GetValue(8), 0.6884230046232789717014682721M);
            Assert.AreEqual(doubleEma.GetValue(12), 0.7184276147305157110572951654M);
            Assert.AreEqual(doubleEma.GetValue(13), 0.6939062055388609041789733531M);
            Assert.AreEqual(doubleEma.GetValue(14), 0.6859071119493631117687221752M);
        }
    }
}
