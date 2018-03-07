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
    public class TripleEMAIndicatorTest
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
        public void tripleEMAUsingTimeFrame5UsingClosePrice()
        {
            TripleEMAIndicator tripleEma = new TripleEMAIndicator(closePrice, 5);

            Assert.AreEqual(tripleEma.GetValue(0), 0.73M);
            Assert.AreEqual(tripleEma.GetValue(1), 0.7229629629629629629629629630M);
            Assert.AreEqual(tripleEma.GetValue(2), 0.8185185185185185185185185184M);

            Assert.AreEqual(tripleEma.GetValue(6), 0.8027617741197988111568358483M);
            Assert.AreEqual(tripleEma.GetValue(7), 0.7328755779098714626835340143M);
            Assert.AreEqual(tripleEma.GetValue(8), 0.6725656658029771884367220444M);

            Assert.AreEqual(tripleEma.GetValue(12), 0.7386749548240852073262444309M);
            Assert.AreEqual(tripleEma.GetValue(13), 0.6994356970882869336319484124M);
            Assert.AreEqual(tripleEma.GetValue(14), 0.6876244023325260941477981564M);
        }
    }
}
