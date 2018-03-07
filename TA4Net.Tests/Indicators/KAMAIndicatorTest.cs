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

    /**
     * The Class KAMAIndicatorTest.
     *
     * @see <a href="http://stockcharts.com/school/data/media/chart_school/technical_indicators_and_overlays/kaufman_s_adaptive_moving_average/cs-kama.xls>
     *     http://stockcharts.com/school/data/media/chart_school/technical_indicators_and_overlays/kaufman_s_adaptive_moving_average/cs-kama.xls</a>
     */
    [TestClass]
    public class KAMAIndicatorTest
    {

        private ITimeSeries data;


        [TestInitialize]
        public void setUp()
        {

            data = new MockTimeSeries(
                    110.46M, 109.80M, 110.17M, 109.82M, 110.15M,
                    109.31M, 109.05M, 107.94M, 107.76M, 109.24M,
                    109.40M, 108.50M, 107.96M, 108.55M, 108.85M,
                    110.44M, 109.89M, 110.70M, 110.79M, 110.22M,
                    110.00M, 109.27M, 106.69M, 107.07M, 107.92M,
                    107.95M, 107.70M, 107.97M, 106.09M, 106.03M,
                    107.65M, 109.54M, 110.26M, 110.38M, 111.94M,
                    113.59M, 113.98M, 113.91M, 112.62M, 112.20M,
                    111.10M, 110.18M, 111.13M, 111.55M, 112.08M,
                    111.95M, 111.60M, 111.39M, 112.25M

            );
        }

        [TestMethod]
        public void kama()
        {
            ClosePriceIndicator closePrice = new ClosePriceIndicator(data);
            KAMAIndicator kama = new KAMAIndicator(closePrice, 10, 2, 30);

            Assert.AreEqual(109.2400M, kama.GetValue(9));
            Assert.AreEqual(109.24494010269916663279218767M, kama.GetValue(10));
            Assert.AreEqual(109.21649206276841440999776905M, kama.GetValue(11));
            Assert.AreEqual(109.11734972710342211716767785M, kama.GetValue(12));
            Assert.AreEqual(109.09810134973597420440184007M, kama.GetValue(13));
            Assert.AreEqual(109.08936998973940855209941847M, kama.GetValue(14));
            Assert.AreEqual(109.12403978156193935651315289M, kama.GetValue(15));
            Assert.AreEqual(109.13756204250952330536959206M, kama.GetValue(16));
            Assert.AreEqual(109.27686418803908486966927279M, kama.GetValue(17));
            Assert.AreEqual(109.43648216217104405819633459M, kama.GetValue(18));
            Assert.AreEqual(109.45685613079237171155132013M, kama.GetValue(19));
            Assert.AreEqual(109.46509570376888770557290122M, kama.GetValue(20));
            Assert.AreEqual(109.46116616313538190569128400M, kama.GetValue(21));
            Assert.AreEqual(109.39044547674048512711012086M, kama.GetValue(22));
            Assert.AreEqual(109.31652898432021430706081888M, kama.GetValue(23));
            Assert.AreEqual(109.29240858974055892248708537M, kama.GetValue(24));
            Assert.AreEqual(109.18361180648975107005053951M, kama.GetValue(25));
            Assert.AreEqual(109.07778091699571761795278118M, kama.GetValue(26));
            Assert.AreEqual(108.94981829788806808134609833M, kama.GetValue(27));
            Assert.AreEqual(108.42295278454941484155641075M, kama.GetValue(28));
            Assert.AreEqual(108.01574214187734934138252762M, kama.GetValue(29));
            Assert.AreEqual(107.99671169314084224582280455M, kama.GetValue(30));
            Assert.AreEqual(108.00685949100256851025453849M, kama.GetValue(31));
            Assert.AreEqual(108.25959109860509061377027655M, kama.GetValue(32));
            Assert.AreEqual(108.48177011138185589650375147M, kama.GetValue(33));
            Assert.AreEqual(108.91193568467020647487181761M, kama.GetValue(34));
            Assert.AreEqual(109.67339750026027475804243407M, kama.GetValue(35));
            Assert.AreEqual(110.49473967676459478770164988M, kama.GetValue(36));
            Assert.AreEqual(111.10765042335075159844801457M, kama.GetValue(37));
            Assert.AreEqual(111.46215852199056860124169871M, kama.GetValue(38));
            Assert.AreEqual(111.60915915140772719047221803M, kama.GetValue(39));
            Assert.AreEqual(111.56631600497264919232848064M, kama.GetValue(40));
            Assert.AreEqual(111.54914734893005196836010061M, kama.GetValue(41));
            Assert.AreEqual(111.54245421864753042795343867M, kama.GetValue(42));
            Assert.AreEqual(111.54261253765774928769157712M, kama.GetValue(43));
            Assert.AreEqual(111.54566829869648938056962958M, kama.GetValue(44));
            Assert.AreEqual(111.56582628748782023199328810M, kama.GetValue(45));
            Assert.AreEqual(111.56882879828005850001393743M, kama.GetValue(46));
            Assert.AreEqual(111.55223531573535020144693432M, kama.GetValue(47));
            Assert.AreEqual(111.55954365475172419965852203M, kama.GetValue(48));
        }

        [TestMethod]
        public void getValueOnDeepIndicesShouldNotCauseStackOverflow()
        {
            ITimeSeries series = new MockTimeSeries();
            series.SetMaximumBarCount(5000);
            Assert.AreEqual(5000, series.GetBarCount());

            KAMAIndicator kama = new KAMAIndicator(new ClosePriceIndicator(series), 10, 2, 30);
            Assert.AreEqual(kama.GetValue(3000), 2999.75M);
        }
    }
}
