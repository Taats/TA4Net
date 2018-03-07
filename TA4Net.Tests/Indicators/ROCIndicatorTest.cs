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

    using TA4Net.Indicators;
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;

    [TestClass]
    public class ROCIndicatorTest
    {

        private decimal[] closePriceValues = new decimal[] {
            11045.27M, 11167.32M, 11008.61M, 11151.83M,
            10926.77M, 10868.12M, 10520.32M, 10380.43M,
            10785.14M, 10748.26M, 10896.91M, 10782.95M,
            10620.16M, 10625.83M, 10510.95M, 10444.37M,
            10068.01M, 10193.39M, 10066.57M, 10043.75M
        };

        private ClosePriceIndicator closePrice;

        [TestInitialize]
        public void setUp()
        {
            closePrice = new ClosePriceIndicator(new MockTimeSeries(closePriceValues));
        }

        [TestMethod]
        public void GetValueWhenTimeFrameIs12()
        {
            ROCIndicator roc = new ROCIndicator(closePrice, 12);

            // Incomplete time frame
            Assert.AreEqual(roc.GetValue(0), 0);
            Assert.AreEqual(roc.GetValue(1), 1.104997885972909670836475700M);
            Assert.AreEqual(roc.GetValue(2), -0.3319067800062832325511282200M);
            Assert.AreEqual(roc.GetValue(3), 0.9647568597236645188392859600M);

            // Complete time frame
            decimal[] results13to20 = new decimal[] {
                -3.8487968152883542004858188200M,
                -4.848880483410522847021487700M,
                -4.5206433873122946493699022900M,
                -6.3438915406709033405279671600M,
                -7.8592301293062817282691957500M,
                -6.2083414610806652852563276800M,
                -4.3130817313541793405523786300M,
                -3.2434109184301613709644012800M };
            for (int i = 0; i < results13to20.Length; i++)
            {
                Assert.AreEqual(roc.GetValue(i + 12), results13to20[i]);
            }
        }
    }
}