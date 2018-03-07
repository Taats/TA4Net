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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net;
    using TA4Net.Extensions;
    using TA4Net.Indicators;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Interfaces;

    [TestClass]
    public class RSIIndicatorTest : IndicatorTest<IIndicator<decimal>, decimal, decimal>
    {

        private ITimeSeries data;
        private ExternalIndicatorTest xls;
        //private ExternalIndicatorTest sql;

        public RSIIndicatorTest()
            : base((t, v) => new RSIIndicator(t, (int)v[0]))
        {
            xls = new XLSIndicatorTest(GetType(), @"TestData\indicators\RSI.xls", 10);
        }

        [TestInitialize]
        public void setUp()
        { // throws exception
            data = new MockTimeSeries(
                    50.45M, 50.30M, 50.20M,
                    50.15M, 50.05M, 50.06M,
                    50.10M, 50.08M, 50.03M,
                    50.07M, 50.01M, 50.14M,
                    50.22M, 50.43M, 50.50M,
                    50.56M, 50.52M, 50.70M,
                    50.55M, 50.62M, 50.90M,
                    50.82M, 50.86M, 51.20M,
                    51.30M, 51.10M);
        }

        [TestMethod]
        public void firstValueShouldBeZero()
        { // throws exception
            IIndicator<decimal> indicator = getIndicator(new ClosePriceIndicator(data), 14);
            Assert.AreEqual(Decimals.ZERO, indicator.GetValue(0));
        }

        [TestMethod]
        public void hundredIfNoLoss()
        { // throws exception
            IIndicator<decimal> indicator = getIndicator(new ClosePriceIndicator(data), 1);
            Assert.AreEqual(Decimals.HUNDRED, indicator.GetValue(14));
            Assert.AreEqual(Decimals.HUNDRED, indicator.GetValue(15));
        }

        [TestMethod]
        public void zeroIfNoGain()
        { // throws exception
            IIndicator<decimal> indicator = getIndicator(new ClosePriceIndicator(data), 1);
            Assert.AreEqual(Decimals.ZERO, indicator.GetValue(1));
            Assert.AreEqual(Decimals.ZERO, indicator.GetValue(2));
        }

        [TestMethod]
        public void usingTimeFrame14UsingClosePrice()
        { // throws exception
            IIndicator<decimal> indicator = getIndicator(new ClosePriceIndicator(data), 14);
            Assert.AreEqual(68.474671406868918994222374415M, indicator.GetValue(15));
            Assert.AreEqual(64.783614076163181860337288507M, indicator.GetValue(16));
            Assert.AreEqual(72.077677961842542979044678157M, indicator.GetValue(17));
            Assert.AreEqual(60.780006136522229339860823723M, indicator.GetValue(18));
            Assert.AreEqual(63.643900017666783471839927687M, indicator.GetValue(19));
            Assert.AreEqual(72.343378237209128766345723604M, indicator.GetValue(20));
            Assert.AreEqual(67.382275421947465231614723781M, indicator.GetValue(21));
            Assert.AreEqual(68.543830908978912500395561329M, indicator.GetValue(22));
            Assert.AreEqual(76.277027004802154537637053949M, indicator.GetValue(23));
            Assert.AreEqual(77.990836319395234894711865667M, indicator.GetValue(24));
            Assert.AreEqual(67.489506140259023217609761123M, indicator.GetValue(25));
        }

        [TestMethod]
        public void xlsTest()
        {
            IIndicator<decimal> xlsClose = new ClosePriceIndicator(xls.getSeries());
            IIndicator<decimal> indicator;

            indicator = getIndicator(xlsClose, 1);
          //  Assert.AreEqual(xls.getIndicator(1), indicator);
            Assert.AreEqual(100.0M, indicator.GetValue(indicator.TimeSeries.GetEndIndex()));

            indicator = getIndicator(xlsClose, 3);
          //  Assert.AreEqual(xls.getIndicator(3), indicator);
            Assert.AreEqual(67.045374582390542029793809567M, indicator.GetValue(indicator.TimeSeries.GetEndIndex()));

            indicator = getIndicator(xlsClose, 13);
          //  Assert.AreEqual(xls.getIndicator(13), indicator);
            Assert.AreEqual(52.587681858903097357018319486M, indicator.GetValue(indicator.TimeSeries.GetEndIndex()));
        }

        [TestMethod]
        public void onlineExampleTest()
        { // throws exception
          // from http://cns.bu.edu/~gsc/CN710/fincast/Technical%20_indicators/Relative%20Strength%20Index%20(RSI).htm
          // which uses a different calculation of RSI than ta4j
            ITimeSeries series = new MockTimeSeries(
                    46.1250M,
                    47.1250M, 46.4375M, 46.9375M, 44.9375M, 44.2500M, 44.6250M, 45.7500M,
                    47.8125M, 47.5625M, 47.0000M, 44.5625M, 46.3125M, 47.6875M, 46.6875M,
                    45.6875M, 43.0625M, 43.5625M, 44.8750M, 43.6875M);
            // ta4j RSI uses MMA for average gain and loss
            // then uses simple division of the two for RS
            IIndicator<decimal> indicator = getIndicator(new ClosePriceIndicator(
                    series), 14);
            IIndicator<decimal> close = new ClosePriceIndicator(series);
            IIndicator<decimal> gain = new GainIndicator(close);
            IIndicator<decimal> loss = new LossIndicator(close);
            // this site uses SMA for average gain and loss
            // then uses ratio of MMAs for RS (except for first calculation)
            IIndicator<decimal> avgGain = new SMAIndicator(gain, 14);
            IIndicator<decimal> avgLoss = new SMAIndicator(loss, 14);

            // first online calculation is simple division
            decimal onlineRs = avgGain.GetValue(14).DividedBy(avgLoss.GetValue(14));
            Assert.AreEqual(0.5848214285714285714285714286M, avgGain.GetValue(14));
            Assert.AreEqual(0.5446428571428571428571428571M, avgLoss.GetValue(14));
            Assert.AreEqual(1.0737704918032786885245901641M, onlineRs);
            decimal onlineRsi = 100M - (100M / (1M + onlineRs));
            // difference in RSI values:
            Assert.AreEqual(51.778656126482213438735177869M, onlineRsi);
            Assert.AreEqual(52.130477585417047385335308781M, indicator.GetValue(14));

            // strange, online average gain and loss is not a simple moving average!
            // but they only use them for the first RS calculation
            // Assert.AreEqual(0.5430, avgGain.getValue(15));
            // Assert.AreEqual(0.5772, avgLoss.getValue(15));
            // second online calculation uses MMAs
            // MMA of average gain
            decimal dividend = avgGain.GetValue(14).MultipliedBy(13M).Plus(gain.GetValue(15)).DividedBy(14M);
            // MMA of average loss
            decimal divisor = avgLoss.GetValue(14).MultipliedBy(13M).Plus(loss.GetValue(15)).DividedBy(14M);
            onlineRs = dividend / divisor;
            Assert.AreEqual(0.940883977900552486187845304M, onlineRs);
            onlineRsi = 100M - (100M / (1M + onlineRs));
            // difference in RSI values:
            Assert.AreEqual(48.477085112439510389980074014M, onlineRsi);
            Assert.AreEqual(47.37103140045740279363506511M, indicator.GetValue(15));
        }
    }
}