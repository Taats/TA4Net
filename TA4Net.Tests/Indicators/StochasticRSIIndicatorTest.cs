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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using TA4Net.Interfaces;

    [TestClass]
    public class StochasticRSIIndicatorTest
    {

        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            data = new MockTimeSeries(50.45M, 50.30M, 50.20M, 50.15M, 50.05M, 50.06M,
                    50.10M, 50.08M, 50.03M, 50.07M, 50.01M, 50.14M, 50.22M, 50.43M, 50.50M,
                    50.56M, 50.52M, 50.70M, 50.55M, 50.62M, 50.90M, 50.82M, 50.86M, 51.20M,
                    51.30M, 51.10M);
        }

        [TestMethod]
        public void stochasticRSI()
        {
            StochasticRSIIndicator srsi = new StochasticRSIIndicator(data, 14);
            Assert.AreEqual(srsi.GetValue(15), 1);
            Assert.AreEqual(srsi.GetValue(16), 0.9460960198147738017399730206M);
            Assert.AreEqual(srsi.GetValue(17), 1);
            Assert.AreEqual(srsi.GetValue(18), 0.8365385284737943074008452449M);
            Assert.AreEqual(srsi.GetValue(19), 0.8610454792049986214572313811M);
            Assert.AreEqual(srsi.GetValue(20), 1);
            Assert.AreEqual(srsi.GetValue(21), 0.9186173738993347000625354807M);
            Assert.AreEqual(srsi.GetValue(22), 0.9305832634464052122075460995M);
            Assert.AreEqual(srsi.GetValue(23), 1);
            Assert.AreEqual(srsi.GetValue(24), 1);
        }
    }
}
