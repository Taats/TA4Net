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
namespace TA4Net.Test.Indicators.helpers
{

    using TA4Net;
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net.Extensions;
    using TA4Net.Interfaces;

    [TestClass]
    public class TypicalPriceIndicatorTest
    {

        private TypicalPriceIndicator typicalPriceIndicator;

        private ITimeSeries timeSeries;

        [TestInitialize]
        public void setUp()
        {
            timeSeries = new MockTimeSeries();
            typicalPriceIndicator = new TypicalPriceIndicator(timeSeries);
        }

        [TestMethod]
        public void indicatorShouldRetrieveBarMaxPrice()
        {
            for (int i = 0; i < 10; i++)
            {
                IBar bar = timeSeries.GetBar(i);
                decimal typicalPrice = bar.MaxPrice.Plus(bar.MinPrice).Plus(bar.ClosePrice)
                        .DividedBy(Decimals.THREE);
                Assert.AreEqual(typicalPrice, typicalPriceIndicator.GetValue(i));
            }
        }
    }
}