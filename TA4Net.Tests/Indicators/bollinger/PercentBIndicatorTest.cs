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
namespace TA4Net.Test.Indicators.bollinger
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net;
    using TA4Net.Extensions;
    using TA4Net.Indicators.Bollinger;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Interfaces;

    [TestClass]
    public class PercentBIndicatorTest
    {

        private ClosePriceIndicator closePrice;

        [TestInitialize]
        public void setUp()
        {
            ITimeSeries data = new MockTimeSeries(
                    10, 12, 15, 14, 17,
                    20, 21, 20, 20, 19,
                    20, 17, 12, 12, 9,
                    8, 9, 10, 9, 10
            );
            closePrice = new ClosePriceIndicator(data);
        }

        [TestMethod]
        public void percentBUsingSMAAndStandardDeviation()
        {

            PercentBIndicator pcb = new PercentBIndicator(closePrice, 5, Decimals.TWO);

            Assert.IsTrue(pcb.GetValue(0).IsNaN());
            Assert.AreEqual(pcb.GetValue(1), 0.75M);
            Assert.AreEqual(pcb.GetValue(2), 0.8244428422615250763289574938M);
            Assert.AreEqual(pcb.GetValue(3), 0.6627361387260298342324086233M);
            Assert.AreEqual(pcb.GetValue(4), 0.8517325026560063787000141567M);
            Assert.AreEqual(pcb.GetValue(5), 0.9032795663087215452159800351M);
            Assert.AreEqual(pcb.GetValue(6), 0.8299560087980449006312563924M);
            Assert.AreEqual(pcb.GetValue(7), 0.6552301051412665722246304488M);
            Assert.AreEqual(pcb.GetValue(8), 0.5737209780774485667289625688M);
            Assert.AreEqual(pcb.GetValue(9), 0.104715292478952583500138307M);
            Assert.AreEqual(pcb.GetValue(10), 0.5M);
            Assert.AreEqual(pcb.GetValue(11), 0.0283788908581006972087082231M);
            Assert.AreEqual(pcb.GetValue(12), 0.0343669263335824871851658675M);
            Assert.AreEqual(pcb.GetValue(13), 0.2063898902426482557563220375M);
            Assert.AreEqual(pcb.GetValue(14), 0.1835189477863541824128451756M);
            Assert.AreEqual(pcb.GetValue(15), 0.2130904791214977694966221377M);
            Assert.AreEqual(pcb.GetValue(16), 0.3505964238332007950038978526M);
            Assert.AreEqual(pcb.GetValue(17), 0.5737209780774485667289625687M);
            Assert.AreEqual(pcb.GetValue(18), 0.5M);
            Assert.AreEqual(pcb.GetValue(19), 0.7672612419124243846845534809M);
        }
    }
}
