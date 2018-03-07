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
    using TA4Net.Indicators;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Interfaces;

    [TestClass]
    public class UlcerIndexIndicatorTest
    {

        private ITimeSeries ibmData;

        [TestInitialize]
        public void setUp()
        {
            ibmData = new MockTimeSeries(
                    194.75M, 195.00M, 195.10M, 194.46M, 190.60M,
                    188.86M, 185.47M, 184.46M, 182.31M, 185.22M,
                    184.00M, 182.87M, 187.45M, 194.51M, 191.63M,
                    190.02M, 189.53M, 190.27M, 193.13M, 195.55M,
                    195.84M, 195.15M, 194.35M, 193.62M, 197.68M,
                    197.91M, 199.08M, 199.03M, 198.42M, 199.29M,
                    199.01M, 198.29M, 198.40M, 200.84M, 201.22M,
                    200.50M, 198.65M, 197.25M, 195.70M, 197.77M,
                    195.69M, 194.87M, 195.08M
            );
        }

        [TestMethod]
        public void ulcerIndexUsingTimeFrame14UsingIBMData()
        {
            UlcerIndexIndicator ulcer = new UlcerIndexIndicator(new ClosePriceIndicator(ibmData), 14);

            Assert.AreEqual(ulcer.GetValue(0), 0);

            // From: http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:ulcer_index
            Assert.AreEqual(ulcer.GetValue(26), 1.3047326664928352567595893019M);
            Assert.AreEqual(ulcer.GetValue(27), 1.3022442772854439559911234689M);
            Assert.AreEqual(ulcer.GetValue(28), 1.2156227590957400106436266852M);
            Assert.AreEqual(ulcer.GetValue(29), 0.9967305155530988753000776083M);
            Assert.AreEqual(ulcer.GetValue(30), 0.7257169838104788684671694592M);
            Assert.AreEqual(ulcer.GetValue(31), 0.4530380971758671142314514174M);
            Assert.AreEqual(ulcer.GetValue(32), 0.4284100872255782787980421088M);
            Assert.AreEqual(ulcer.GetValue(33), 0.4284100872255782787980421088M);
            Assert.AreEqual(ulcer.GetValue(34), 0.4284100872255782787980421088M);
            Assert.AreEqual(ulcer.GetValue(35), 0.4287349119903383338327059684M);
            Assert.AreEqual(ulcer.GetValue(36), 0.5089063000184522707318409976M);
            Assert.AreEqual(ulcer.GetValue(37), 0.6672646295927154897520641717M);
            Assert.AreEqual(ulcer.GetValue(38), 0.9913518177402269613359840276M);
            Assert.AreEqual(ulcer.GetValue(39), 1.0921325741850082888500073224M);
            Assert.AreEqual(ulcer.GetValue(40), 1.3161456073667125991227406808M);
            Assert.AreEqual(ulcer.GetValue(41), 1.5631807417469005351409171744M);
            Assert.AreEqual(ulcer.GetValue(42), 1.7608952153624351692835645306M);
        }
    }
}
