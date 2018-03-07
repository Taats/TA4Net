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
namespace TA4Net.Test.Indicators.adx
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net;
    using TA4Net.Indicators.Adx;
    using TA4Net.Interfaces;

    [TestClass]
    public class MinusDIIndicatorTest : IndicatorTest<ITimeSeries, int, decimal>
    {

        private ExternalIndicatorTest xls;

        public MinusDIIndicatorTest()
            : base((ts,v) => new MinusDIIndicator(ts, v[0]))
        {
            xls = new XLSIndicatorTest(GetType(), @"TestData\Indicators\adx\ADX.xls", 13);
        }

        [TestMethod]
        public void xlsTest()
        {
            ITimeSeries xlsSeries = xls.getSeries();
            IIndicator<decimal> indicator;

            indicator = getIndicator(xlsSeries, 1);
           // Assert.AreEqual(xls.getIndicator(1), indicator);
            Assert.AreEqual(0.0M, indicator.GetValue(indicator.TimeSeries.GetEndIndex()));

            indicator = getIndicator(xlsSeries, 3);
           // Assert.AreEqual(xls.getIndicator(3), indicator);
            Assert.AreEqual(21.071139534914776853878480510M, indicator.GetValue(indicator.TimeSeries.GetEndIndex()));

            indicator = getIndicator(xlsSeries, 13);
           // Assert.AreEqual(xls.getIndicator(13), indicator);
            Assert.AreEqual(20.902069300832188108447493710M, indicator.GetValue(indicator.TimeSeries.GetEndIndex()));
        }

    }
}
