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

    using TA4Net;
    using TA4Net.Extensions;
    using TA4Net.Indicators;
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net.Interfaces;

    [TestClass]
    public class DPOIndicatorTest
    {

        private ITimeSeries series;

        [TestInitialize]
        public void setUp()
        {
            series = new MockTimeSeries(
                    22.27M, 22.19M, 22.08M, 22.17M, 22.18M, 22.13M,
                    22.23M, 22.43M, 22.24M, 22.29M, 22.15M, 22.39M,
                    22.38M, 22.61M, 23.36M, 24.05M, 23.75M, 23.83M,
                    23.95M, 23.63M, 23.82M, 23.87M, 23.65M, 23.19M,
                    23.10M, 23.33M, 22.68M, 23.10M, 22.40M, 22.17M,
                    22.27M, 22.19M, 22.08M, 22.17M, 22.18M, 22.13M,
                    22.23M, 22.43M, 22.24M, 22.29M, 22.15M, 22.39M,
                    22.38M, 22.61M, 23.36M, 24.05M, 23.75M, 23.83M,
                    23.95M, 23.63M, 23.82M, 23.87M, 23.65M, 23.19M,
                    23.10M, 23.33M, 22.68M, 23.10M, 22.40M, 22.17M,
                    22.27M, 22.19M, 22.08M, 22.17M, 22.18M, 22.13M,
                    22.23M, 22.43M, 22.24M, 22.29M, 22.15M, 22.39M,
                    22.38M, 22.61M, 23.36M, 24.05M, 23.75M, 23.83M,
                    23.95M, 23.63M, 23.82M, 23.87M, 23.65M, 23.19M,
                    23.10M, 23.33M, 22.68M, 23.10M, 22.40M, 22.17M
            );
        }

        [TestMethod]
        public void dpo()
        {
            DPOIndicator dpo = new DPOIndicator(series, 9);
            ClosePriceIndicator cp = new ClosePriceIndicator(series);
            SMAIndicator sma = new SMAIndicator(cp, 9);
            int timeShift = 9 / 2 + 1;
            for (int i = series.GetBeginIndex(); i <= series.GetEndIndex(); i++)
            {
                Assert.AreEqual(dpo.GetValue(i), cp.GetValue(i).Minus(sma.GetValue(i - timeShift)));
            }

            Assert.AreEqual(dpo.GetValue(9), 0.112M);
            Assert.AreEqual(dpo.GetValue(10), -0.02M);
            Assert.AreEqual(dpo.GetValue(11), 0.211428571428571428571428571M);
            Assert.AreEqual(dpo.GetValue(12), 0.17M);
        }


        // [TestMethod] (expected = IndexOutOfBoundsException.class)
        public void dpoIOOBE()
        {
            DPOIndicator dpo = new DPOIndicator(series, 9);
            dpo.GetValue(100);
        }
    }
}