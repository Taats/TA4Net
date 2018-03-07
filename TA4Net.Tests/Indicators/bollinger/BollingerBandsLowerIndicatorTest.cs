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
    using TA4Net;
    using TA4Net.Indicators;
    using TA4Net.Indicators.Bollinger;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Indicators.Statistics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net.Interfaces;

    [TestClass]
    public class BollingerBandsLowerIndicatorTest
    {

        private int timeFrame;

        private ClosePriceIndicator closePrice;

        private SMAIndicator sma;

        [TestInitialize]
        public void setUp()
        {
            ITimeSeries data = new MockTimeSeries(1, 2, 3, 4, 3, 4, 5, 4, 3, 3, 4, 3, 2);
            timeFrame = 3;
            closePrice = new ClosePriceIndicator(data);
            sma = new SMAIndicator(closePrice, timeFrame);
        }

        [TestMethod]
        public void bollingerBandsLowerUsingSMAAndStandardDeviation()
        {

            BollingerBandsMiddleIndicator bbmSMA = new BollingerBandsMiddleIndicator(sma);
            StandardDeviationIndicator standardDeviation = new StandardDeviationIndicator(closePrice, timeFrame);
            BollingerBandsLowerIndicator bblSMA = new BollingerBandsLowerIndicator(bbmSMA, standardDeviation);

            Assert.AreEqual(bblSMA.getK(), 2);

            Assert.AreEqual(bblSMA.GetValue(0), 1);
            Assert.AreEqual(bblSMA.GetValue(1), 0.5M);
            Assert.AreEqual(bblSMA.GetValue(2), 0.3670068381445479345351439502M);
            Assert.AreEqual(bblSMA.GetValue(3), 1.3670068381445479345351439502M);
            Assert.AreEqual(bblSMA.GetValue(4), 2.3905242917512699674655408505M);
            Assert.AreEqual(bblSMA.GetValue(5), 2.7238576250846033007988741839M);
            Assert.AreEqual(bblSMA.GetValue(6), 2.3670068381445479345351439502M);

            BollingerBandsLowerIndicator bblSMAwithK = new BollingerBandsLowerIndicator(bbmSMA, standardDeviation, 1.5M);

            Assert.AreEqual(bblSMAwithK.getK(), 1.5M);

            Assert.AreEqual(bblSMAwithK.GetValue(0), 1);
            Assert.AreEqual(bblSMAwithK.GetValue(1), 0.75M);
            Assert.AreEqual(bblSMAwithK.GetValue(2), 0.7752551286084109509013579626M);
            Assert.AreEqual(bblSMAwithK.GetValue(3), 1.7752551286084109509013579626M);
            Assert.AreEqual(bblSMAwithK.GetValue(4), 2.6262265521467858089324889712M);
            Assert.AreEqual(bblSMAwithK.GetValue(5), 2.9595598854801191422658223046M);
            Assert.AreEqual(bblSMAwithK.GetValue(6), 2.7752551286084109509013579626M);
        }
    }
}