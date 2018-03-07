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

    using TA4Net.Analysis.Criteria;
    using TA4Net;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Mocks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Indicators;
    using TA4Net.Interfaces;

    [TestClass]
    public class CoppockCurveIndicatorTest
    {

        [TestMethod]
        public void coppockCurveWithRoc14Roc11Wma10()
        {
            // Example from http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:coppock_curve
            ITimeSeries data = new MockTimeSeries(
                    872.81M, 919.14M, 919.32M, 987.48M, 1020.62M,
                    1057.08M, 1036.19M, 1095.63M, 1115.1M, 1073.87M,
                    1104.49M, 1169.43M, 1186.69M, 1089.41M, 1030.71M,
                    1101.6M, 1049.33M, 1141.2M, 1183.26M, 1180.55M,
                    1257.64M, 1286.12M, 1327.22M, 1325.83M, 1363.61M,
                    1345.2M, 1320.64M, 1292.28M, 1218.89M, 1131.42M,
                    1253.3M, 1246.96M, 1257.6M, 1312.41M, 1365.68M,
                    1408.47M, 1397.91M, 1310.33M, 1362.16M, 1379.32M
            );

            CoppockCurveIndicator cc = new CoppockCurveIndicator(new ClosePriceIndicator(data), 14, 11, 10);

            Assert.AreEqual(cc.GetValue(31), 23.892855086922197744287818351M);
            Assert.AreEqual(cc.GetValue(32), 19.318661466425348019588229135M);
            Assert.AreEqual(cc.GetValue(33), 16.350542803958196462308468202M);
            Assert.AreEqual(cc.GetValue(34), 14.119931234084269587181699349M);
            Assert.AreEqual(cc.GetValue(35), 12.782034421286169059841703773M);
            Assert.AreEqual(cc.GetValue(36), 11.39242630956381711118490345M);
            Assert.AreEqual(cc.GetValue(37), 8.366161426264314349758720221M);
            Assert.AreEqual(cc.GetValue(38), 7.4532394830272277872914349091M);
            Assert.AreEqual(cc.GetValue(39), 8.790068424954794622463894475M);
        }
    }
}