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
    public class PPOIndicatorTest
    {

        private ClosePriceIndicator closePriceIndicator;

        [TestInitialize]
        public void setUp()
        {
            ITimeSeries series = new MockTimeSeries(
                    22.27M, 22.19M, 22.08M, 22.17M, 22.18M, 22.13M,
                    22.23M, 22.43M, 22.24M, 22.29M, 22.15M, 22.39M,
                    22.38M, 22.61M, 23.36M, 24.05M, 23.75M, 23.83M,
                    23.95M, 23.63M, 23.82M, 23.87M, 23.65M, 23.19M,
                    23.10M, 23.33M, 22.68M, 23.10M, 21.40M, 20.17M
            );
            closePriceIndicator = new ClosePriceIndicator(series);
        }

        [TestMethod]
        public void getValueWithEma10AndEma20()
        {
            PPOIndicator ppo = new PPOIndicator(closePriceIndicator, 10, 20);

            Assert.AreEqual(ppo.GetValue(21), 1.67780570148937817231578600M);
            Assert.AreEqual(ppo.GetValue(22), 1.5669258195342456701776811500M);
            Assert.AreEqual(ppo.GetValue(23), 1.2884438234398944188665800700M);

            Assert.AreEqual(ppo.GetValue(28), -0.2925826973018672921767644400M);
            Assert.AreEqual(ppo.GetValue(29), -1.3088299887349524021082730300M);
        }
    }
}