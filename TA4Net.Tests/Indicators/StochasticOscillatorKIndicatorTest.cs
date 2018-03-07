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
    public class StochasticOscillatorKIndicatorTest
    {

        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {

            List<IBar> bars = new List<IBar>();
            bars.Add(new MockBar(44.98M, 119.13M, 119.50M, 116.00M));
            bars.Add(new MockBar(45.05M, 116.75M, 119.94M, 116.00M));
            bars.Add(new MockBar(45.11M, 113.50M, 118.44M, 111.63M));
            bars.Add(new MockBar(45.19M, 111.56M, 114.19M, 110.06M));
            bars.Add(new MockBar(45.12M, 112.25M, 112.81M, 109.63M));
            bars.Add(new MockBar(45.15M, 110.00M, 113.44M, 109.13M));
            bars.Add(new MockBar(45.13M, 113.50M, 115.81M, 110.38M));
            bars.Add(new MockBar(45.12M, 117.13M, 117.50M, 114.06M));
            bars.Add(new MockBar(45.15M, 115.63M, 118.44M, 114.81M));
            bars.Add(new MockBar(45.24M, 114.13M, 116.88M, 113.13M));
            bars.Add(new MockBar(45.43M, 118.81M, 119.00M, 116.19M));
            bars.Add(new MockBar(45.43M, 117.38M, 119.75M, 117.00M));
            bars.Add(new MockBar(45.58M, 119.13M, 119.13M, 116.88M));
            bars.Add(new MockBar(45.58M, 115.38M, 119.44M, 114.56M));

            data = new BaseTimeSeries(bars);
        }

        [TestMethod]
        public void stochasticOscilatorKParam14()
        {

            StochasticOscillatorKIndicator sof = new StochasticOscillatorKIndicator(data, 14);

            Assert.AreEqual(sof.GetValue(0), 313M / 3.5M);
            Assert.AreEqual(sof.GetValue(12), 1000M / 10.81M);
            Assert.AreEqual(sof.GetValue(13), 57.816836262719703977798334880M);
        }
    }
}