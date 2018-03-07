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
    public class ZLEMAIndicatorTest
    {

        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {
            data = new MockTimeSeries(
                    10, 15, 20,
                    18, 17, 18,
                    15, 12, 10,
                    8, 5, 2);
        }

        [TestMethod]
        public void ZLEMAUsingTimeFrame10UsingClosePrice()
        {
            ZLEMAIndicator zlema = new ZLEMAIndicator(new ClosePriceIndicator(data), 10);

            Assert.AreEqual(zlema.GetValue(9), 11.909090909090909090909090909M);
            Assert.AreEqual(zlema.GetValue(10), 8.834710743801652892561983471M);
            Assert.AreEqual(zlema.GetValue(11), 5.7738542449286250939143501129M);
        }

        [TestMethod]
        public void ZLEMAFirstValueShouldBeEqualsToFirstDataValue()
        {
            ZLEMAIndicator zlema = new ZLEMAIndicator(new ClosePriceIndicator(data), 10);
            Assert.AreEqual(zlema.GetValue(0), 10M);
        }

        [TestMethod]
        public void valuesLessThanTimeFrameMustBeEqualsToSMAValues()
        {
            ZLEMAIndicator zlema = new ZLEMAIndicator(new ClosePriceIndicator(data), 10);
            SMAIndicator sma = new SMAIndicator(new ClosePriceIndicator(data), 10);

            for (int i = 0; i < 9; i++)
            {
                Assert.AreEqual(sma.GetValue(i), zlema.GetValue(i));
            }
        }

        [TestMethod]
        public void smallTimeFrame()
        {
            ZLEMAIndicator zlema = new ZLEMAIndicator(new ClosePriceIndicator(data), 1);
            Assert.AreEqual(zlema.GetValue(0), 10M);
        }
    }
}