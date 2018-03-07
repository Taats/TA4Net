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
    using System.Collections.Generic;
    using TA4Net.Extensions;
    using TA4Net.Interfaces;

    [TestClass]
    public class MedianPriceIndicatorTest
    {
        private MedianPriceIndicator average;

        private ITimeSeries timeSeries;

        [TestInitialize]
        public void setUp()
        {
            List<IBar> bars = new List<IBar>();

            bars.Add(new MockBar(0, 0, 16, 8));
            bars.Add(new MockBar(0, 0, 12, 6));
            bars.Add(new MockBar(0, 0, 18, 14));
            bars.Add(new MockBar(0, 0, 10, 6));
            bars.Add(new MockBar(0, 0, 32, 6));
            bars.Add(new MockBar(0, 0, 2, 2));
            bars.Add(new MockBar(0, 0, 0, 0));
            bars.Add(new MockBar(0, 0, 8, 1));
            bars.Add(new MockBar(0, 0, 83, 32));
            bars.Add(new MockBar(0, 0, 9, 3));


            this.timeSeries = new MockTimeSeries(bars);
            average = new MedianPriceIndicator(timeSeries);
        }

        [TestMethod]
        public void indicatorShouldRetrieveBarClosePrice()
        {
            decimal result;
            for (int i = 0; i < 10; i++)
            {
                result = timeSeries.GetBar(i).MaxPrice.Plus(timeSeries.GetBar(i).MinPrice)
                        .DividedBy(Decimals.TWO);
                Assert.AreEqual(average.GetValue(i), result);
            }
        }
    }
}