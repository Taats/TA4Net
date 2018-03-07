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
    public class RAVIIndicatorTest
    {

        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {

            data = new MockTimeSeries(
                    110.00M, 109.27M, 104.69M, 107.07M, 107.92M,
                    107.95M, 108.70M, 107.97M, 106.09M, 106.03M,
                    108.65M, 109.54M, 112.26M, 114.38M, 117.94M

            );
        }

        [TestMethod]
        public void ravi()
        {
            ClosePriceIndicator closePrice = new ClosePriceIndicator(data);
            RAVIIndicator ravi = new RAVIIndicator(closePrice, 3, 8);

            Assert.AreEqual(ravi.GetValue(0), 0);
            Assert.AreEqual(ravi.GetValue(1), 0);
            Assert.AreEqual(ravi.GetValue(2), 0);
            Assert.AreEqual(ravi.GetValue(3), -0.6936872143470292091037746800M);
            Assert.AreEqual(ravi.GetValue(4), -1.1411077094350125243529084300M);
            Assert.AreEqual(ravi.GetValue(5), -0.15767506569794404081001700M);
            Assert.AreEqual(ravi.GetValue(6), 0.2289571201694017998941238800M);
            Assert.AreEqual(ravi.GetValue(7), 0.2412466080726904979715985200M);
            Assert.AreEqual(ravi.GetValue(8), 0.1202025607022931546580431100M);
            Assert.AreEqual(ravi.GetValue(9), -0.3323914278819582292177514100M);
            Assert.AreEqual(ravi.GetValue(10), -0.580363715257599355323616700M);
            Assert.AreEqual(ravi.GetValue(11), 0.2012709818238009696548260600M);
            Assert.AreEqual(ravi.GetValue(12), 1.6155629100889078517971840100M);
            Assert.AreEqual(ravi.GetValue(13), 2.6166983356608136260616744100M);
            Assert.AreEqual(ravi.GetValue(14), 4.0799220714496069592007792900M);
        }
    }
}
