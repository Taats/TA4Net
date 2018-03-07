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
    using TA4Net.Indicators;
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net.Interfaces;

    [TestClass]
    public class CMOIndicatorTest
    {

        private ITimeSeries series;

        [TestInitialize]
        public void setUp()
        {
            series = new MockTimeSeries(
                    21.27M, 22.19M, 22.08M, 22.47M, 22.48M, 22.53M,
                    22.23M, 21.43M, 21.24M, 21.29M, 22.15M, 22.39M,
                    22.38M, 22.61M, 23.36M, 24.05M, 24.75M, 24.83M,
                    23.95M, 23.63M, 23.82M, 23.87M, 23.15M, 23.19M,
                    23.10M, 22.65M, 22.48M, 22.87M, 22.93M, 22.91M
            );
        }

        [TestMethod]
        public void dpo()
        {
            CMOIndicator cmo = new CMOIndicator(new ClosePriceIndicator(series), 9);

            Assert.AreEqual(85.13513513513513513513513514M, cmo.GetValue(5));
            Assert.AreEqual(53.932584269662921348314606740M, cmo.GetValue(6));
            Assert.AreEqual(6.2015503875968992248062015500M, cmo.GetValue(7));
            Assert.AreEqual(-1.0830324909747292418772563200M, cmo.GetValue(8));
            Assert.AreEqual(0.7092198581560283687943262400M, cmo.GetValue(9));
            Assert.AreEqual(-1.4492753623188405797101449300M, cmo.GetValue(10));
            Assert.AreEqual(10.726643598615916955017301040M, cmo.GetValue(11));
            Assert.AreEqual(-3.5856573705179282868525896400M, cmo.GetValue(12));
            Assert.AreEqual(4.7619047619047619047619047600M, cmo.GetValue(13));
            Assert.AreEqual(24.198250728862973760932944610M, cmo.GetValue(14));
            Assert.AreEqual(47.643979057591623036649214660M, cmo.GetValue(15));
        }
    }
}