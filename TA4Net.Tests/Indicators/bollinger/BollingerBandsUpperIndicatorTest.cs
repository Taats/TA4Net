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
    public class BollingerBandsUpperIndicatorTest
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
        public void BollingerBandsUpperUsingSMAAndStandardDeviation()
        {

            BollingerBandsMiddleIndicator bbmSMA = new BollingerBandsMiddleIndicator(sma);
            StandardDeviationIndicator standardDeviation = new StandardDeviationIndicator(closePrice, timeFrame);
            BollingerBandsUpperIndicator bbuSMA = new BollingerBandsUpperIndicator(bbmSMA, standardDeviation);

            Assert.AreEqual(bbuSMA.getK(), 2);

            Assert.AreEqual(bbuSMA.GetValue(0), 1);
            Assert.AreEqual(bbuSMA.GetValue(1), 2.5M);
            Assert.AreEqual(bbuSMA.GetValue(2), 3.6329931618554520654648560498M);
            Assert.AreEqual(bbuSMA.GetValue(3), 4.6329931618554520654648560498M);
            Assert.AreEqual(bbuSMA.GetValue(4), 4.2761423749153966992011258161M);
            Assert.AreEqual(bbuSMA.GetValue(5), 4.6094757082487300325344591495M);
            Assert.AreEqual(bbuSMA.GetValue(6), 5.6329931618554520654648560498M);
            Assert.AreEqual(bbuSMA.GetValue(7), 5.2761423749153966992011258161M);
            Assert.AreEqual(bbuSMA.GetValue(8), 5.6329931618554520654648560498M);
            Assert.AreEqual(bbuSMA.GetValue(9), 4.2761423749153966992011258161M);

            BollingerBandsUpperIndicator bbuSMAwithK = new BollingerBandsUpperIndicator(bbmSMA, standardDeviation, 1.5M);

            Assert.AreEqual(bbuSMAwithK.getK(), 1.5M);

            Assert.AreEqual(bbuSMAwithK.GetValue(0), 1);
            Assert.AreEqual(bbuSMAwithK.GetValue(1), 2.25M);
            Assert.AreEqual(bbuSMAwithK.GetValue(2), 3.2247448713915890490986420374M);
            Assert.AreEqual(bbuSMAwithK.GetValue(3), 4.2247448713915890490986420374M);
            Assert.AreEqual(bbuSMAwithK.GetValue(4), 4.0404401145198808577341776954M);
            Assert.AreEqual(bbuSMAwithK.GetValue(5), 4.3737734478532141910675110288M);
            Assert.AreEqual(bbuSMAwithK.GetValue(6), 5.2247448713915890490986420374M);
            Assert.AreEqual(bbuSMAwithK.GetValue(7), 5.0404401145198808577341776954M);
            Assert.AreEqual(bbuSMAwithK.GetValue(8), 5.2247448713915890490986420374M);
            Assert.AreEqual(bbuSMAwithK.GetValue(9), 4.0404401145198808577341776954M);
        }
    }
}
