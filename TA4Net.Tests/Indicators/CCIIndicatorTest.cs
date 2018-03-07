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
    using System.Collections.Generic;
    using TA4Net;
    using TA4Net.Indicators;
    using TA4Net.Interfaces;

    [TestClass]
    public class CCIIndicatorTest
    {

        private decimal[] typicalPrices = new decimal[] {
            23.98M, 23.92M, 23.79M, 23.67M, 23.54M,
            23.36M, 23.65M, 23.72M, 24.16M, 23.91M,
            23.81M, 23.92M, 23.74M, 24.68M, 24.94M,
            24.93M, 25.10M, 25.12M, 25.20M, 25.06M,
            24.50M, 24.31M, 24.57M, 24.62M, 24.49M,
            24.37M, 24.41M, 24.35M, 23.75M, 24.09M
        };

        private MockTimeSeries series;

        [TestInitialize]
        public void setUp()
        {
            List<IBar> bars = new List<IBar>();
            foreach (decimal price in typicalPrices)
            {
                bars.Add(new MockBar(price, price, price, price));
            }
            series = new MockTimeSeries(bars);
        }

        [TestMethod]
        public void getValueWhenTimeFrameIs20()
        {
            CCIIndicator cci = new CCIIndicator(series, 20);

            // Incomplete time frame 
            Assert.AreEqual(-66.666666666666666666666666667M, cci.GetValue(1));
            Assert.AreEqual(-99.99999999999999999999999719M, cci.GetValue(2));
            Assert.AreEqual(14.365001632386549134835129045M, cci.GetValue(10));
            Assert.AreEqual(54.254422914911541701769165368M, cci.GetValue(11));

            // Complete time frame
            var results20to30 = new decimal[] {
                101.91846522781774580335731415M,
                31.194611839773130095710740872M,
                6.5577715609301206268988960082M,
                33.607807258310460506251906069M,
                34.96855345911949685534591195M,
                13.60267993097147497715967922M,
                -10.678871090770404271548436308M,
                -11.470985155195681511470985155M,
                -29.25666070527812170403998716M,
                -128.60001740189680675193596102M,
                -72.727272727272727272727272727M
            };
            for (int i = 0; i < results20to30.Length; i++)
            {
                Assert.AreEqual(results20to30[i], cci.GetValue(i + 19));
            }
        }
    }
}
