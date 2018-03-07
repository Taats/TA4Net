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
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class WilliamsRIndicatorTest
    {
        private ITimeSeries data;

        [TestInitialize]
        public void setUp()
        {

            List<IBar> bars = new List<IBar>();
            bars.Add(new MockBar(44.98M, 45.05M, 45.17M, 44.96M));
            bars.Add(new MockBar(45.05M, 45.10M, 45.15M, 44.99M));
            bars.Add(new MockBar(45.11M, 45.19M, 45.32M, 45.11M));
            bars.Add(new MockBar(45.19M, 45.14M, 45.25M, 45.04M));
            bars.Add(new MockBar(45.12M, 45.15M, 45.20M, 45.10M));
            bars.Add(new MockBar(45.15M, 45.14M, 45.20M, 45.10M));
            bars.Add(new MockBar(45.13M, 45.10M, 45.16M, 45.07M));
            bars.Add(new MockBar(45.12M, 45.15M, 45.22M, 45.10M));
            bars.Add(new MockBar(45.15M, 45.22M, 45.27M, 45.14M));
            bars.Add(new MockBar(45.24M, 45.43M, 45.45M, 45.20M));
            bars.Add(new MockBar(45.43M, 45.44M, 45.50M, 45.39M));
            bars.Add(new MockBar(45.43M, 45.55M, 45.60M, 45.35M));
            bars.Add(new MockBar(45.58M, 45.55M, 45.61M, 45.39M));

            data = new BaseTimeSeries(bars);
        }

        [TestMethod]
        public void williamsRUsingTimeFrame5UsingClosePrice()
        {
            WilliamsRIndicator wr = new WilliamsRIndicator(new ClosePriceIndicator(data), 5,
                    new MaxPriceIndicator(data),
                    new MinPriceIndicator(data));

            Assert.AreEqual(wr.GetValue(4), -47.222222222222222222222222220M);
            Assert.AreEqual(wr.GetValue(5), -54.545454545454545454545454550M);
            Assert.AreEqual(wr.GetValue(6), -78.571428571428571428571428570M);
            Assert.AreEqual(wr.GetValue(7), -47.619047619047619047619047620M);
            Assert.AreEqual(wr.GetValue(8), -25M);
            Assert.AreEqual(wr.GetValue(9), -5.2631578947368421052631578900M);
            Assert.AreEqual(wr.GetValue(10), -13.953488372093023255813953490M);

        }

        [TestMethod]
        public void williamsRUsingTimeFrame10UsingClosePrice()
        {
            WilliamsRIndicator wr = new WilliamsRIndicator(new ClosePriceIndicator(data), 10, new MaxPriceIndicator(data),
                    new MinPriceIndicator(data));

            Assert.AreEqual(wr.GetValue(9), -4.0816326530612244897959183700M);
            Assert.AreEqual(wr.GetValue(10), -11.764705882352941176470588240M);
            Assert.AreEqual(wr.GetValue(11), -8.928571428571428571428571430M);
            Assert.AreEqual(wr.GetValue(12), -10.526315789473684210526315790M);

        }

        [TestMethod]
        public void valueLessThenTimeFrame()
        {
            WilliamsRIndicator wr = new WilliamsRIndicator(new ClosePriceIndicator(data), 100, new MaxPriceIndicator(data),
                    new MinPriceIndicator(data));

            Assert.AreEqual(-100M * (0.12M / 0.21M), wr.GetValue(0));
            Assert.AreEqual(-100M * (0.07M / 0.21M), wr.GetValue(1));
            Assert.AreEqual(-100M * (0.13M / 0.36M), wr.GetValue(2));
            Assert.AreEqual(-100M * (0.18M / 0.36M), wr.GetValue(3));
        }
    }
}