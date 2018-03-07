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
    public class BollingerBandWidthIndicatorTest
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
        public void BollingerBandWidthUsingSMAAndStandardDeviation()
        {

            SMAIndicator sma = new SMAIndicator(closePrice, 5);
            StandardDeviationIndicator standardDeviation = new StandardDeviationIndicator(closePrice, 5);

            BollingerBandsMiddleIndicator bbmSMA = new BollingerBandsMiddleIndicator(sma);
            BollingerBandsUpperIndicator bbuSMA = new BollingerBandsUpperIndicator(bbmSMA, standardDeviation);
            BollingerBandsLowerIndicator bblSMA = new BollingerBandsLowerIndicator(bbmSMA, standardDeviation);

            BollingerBandWidthIndicator bandwidth = new BollingerBandWidthIndicator(bbuSMA, bbmSMA, bblSMA);

            Assert.AreEqual(bandwidth.GetValue(0), 0.0M);
            Assert.AreEqual(bandwidth.GetValue(1), 36.363636363636363636363636360M);
            Assert.AreEqual(bandwidth.GetValue(2), 66.642313545610556218920998730M);
            Assert.AreEqual(bandwidth.GetValue(3), 60.244280375440064123683819780M);
            Assert.AreEqual(bandwidth.GetValue(4), 71.076741021144541578134348630M);
            Assert.AreEqual(bandwidth.GetValue(5), 69.939393317876184065428705400M);
            Assert.AreEqual(bandwidth.GetValue(6), 62.704283664302785713832632430M);
            Assert.AreEqual(bandwidth.GetValue(7), 56.017820550978806498453596730M);
            Assert.AreEqual(bandwidth.GetValue(8), 27.682979522960278118630842150M);
            Assert.AreEqual(bandwidth.GetValue(9), 12.649110640673517327995574180M);
            Assert.AreEqual(bandwidth.GetValue(10), 12.649110640673517327995574180M);
            Assert.AreEqual(bandwidth.GetValue(11), 24.295632895188751961975636990M);
            Assert.AreEqual(bandwidth.GetValue(12), 68.333165356240492140358775280M);
            Assert.AreEqual(bandwidth.GetValue(13), 85.14693182963200583066660912M);
            Assert.AreEqual(bandwidth.GetValue(14), 112.84810090360856581393406310M);
            Assert.AreEqual(bandwidth.GetValue(15), 108.16818718178015127023901017M);
            Assert.AreEqual(bandwidth.GetValue(16), 66.932802122726043838253762060M);
            Assert.AreEqual(bandwidth.GetValue(17), 56.519416526043901158871302720M);
            Assert.AreEqual(bandwidth.GetValue(18), 28.109134757052260728879053730M);
            Assert.AreEqual(bandwidth.GetValue(19), 32.536151189338620744206510720M);
        }
    }
}
