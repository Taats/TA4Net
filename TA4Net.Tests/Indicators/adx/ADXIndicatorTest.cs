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
    public class ADXIndicatorTest : IndicatorTest<ITimeSeries, decimal, decimal>
    {

        private ExternalIndicatorTest xls;

        public ADXIndicatorTest()
             : base((t, v) => new ADXIndicator(t, (int)v[0], (int)v[1]))
        {
            xls = new XLSIndicatorTest(GetType(), @"TestData\indicators\adx\ADX.xls", 15);
        }
        

        [TestMethod]
        public void externalData()
        {
            ITimeSeries series = xls.getSeries();
            IIndicator<decimal> actualIndicator;

            actualIndicator = getIndicator(series, 1, 1);
            // Assert.AreEqual(xls.getIndicator(1, 1), actualIndicator);
            Assert.AreEqual(100.0M, actualIndicator.GetValue(actualIndicator.TimeSeries.GetEndIndex()));

            actualIndicator = getIndicator(series, 3, 2);
            // Assert.AreEqual(xls.getIndicator(3, 2), actualIndicator);
            Assert.AreEqual(12.133072686370729785734629677M, actualIndicator.GetValue(actualIndicator.TimeSeries.GetEndIndex()));

            actualIndicator = getIndicator(series, 13, 8);
            // Assert.AreEqual(xls.getIndicator(13, 8), actualIndicator);
            Assert.AreEqual(7.3884961797389535319097127910M, actualIndicator.GetValue(actualIndicator.TimeSeries.GetEndIndex()));
        }

    }
}