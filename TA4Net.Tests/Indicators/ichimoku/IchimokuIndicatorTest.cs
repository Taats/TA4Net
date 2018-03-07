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
namespace TA4Net.Test.Indicators.ichimoku
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System.Collections.Generic;
    using TA4Net;
    using TA4Net.Indicators.Ichimoku;
    using TA4Net.Interfaces;

    [TestClass]
    public class IchimokuIndicatorTest
    {

        protected ITimeSeries data;

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
            bars.Add(new MockBar(45.45M, 45.01M, 45.55M, 44.80M));
            bars.Add(new MockBar(45.03M, 44.23M, 45.04M, 44.17M));
            bars.Add(new MockBar(44.23M, 43.95M, 44.29M, 43.81M));
            bars.Add(new MockBar(43.91M, 43.08M, 43.99M, 43.08M));
            bars.Add(new MockBar(43.07M, 43.55M, 43.65M, 43.06M));
            bars.Add(new MockBar(43.56M, 43.95M, 43.99M, 43.53M));
            bars.Add(new MockBar(43.93M, 44.47M, 44.58M, 43.93M));
            data = new MockTimeSeries(bars);
        }

        [TestMethod]
        public void ichimoku()
        {
            IchimokuTenkanSenIndicator tenkanSen = new IchimokuTenkanSenIndicator(data, 3);
            IchimokuKijunSenIndicator kijunSen = new IchimokuKijunSenIndicator(data, 5);
            IchimokuSenkouSpanAIndicator senkouSpanA = new IchimokuSenkouSpanAIndicator(data, tenkanSen, kijunSen);
            IchimokuSenkouSpanBIndicator senkouSpanB = new IchimokuSenkouSpanBIndicator(data, 9);
            IchimokuChikouSpanIndicator chikouSpan = new IchimokuChikouSpanIndicator(data, 5);

            Assert.AreEqual(tenkanSen.GetValue(3), 45.155M);
            Assert.AreEqual(tenkanSen.GetValue(4), 45.18M);
            Assert.AreEqual(tenkanSen.GetValue(5), 45.145M);
            Assert.AreEqual(tenkanSen.GetValue(6), 45.135M);
            Assert.AreEqual(tenkanSen.GetValue(7), 45.145M);
            Assert.AreEqual(tenkanSen.GetValue(8), 45.17M);
            Assert.AreEqual(tenkanSen.GetValue(16), 44.06M);
            Assert.AreEqual(tenkanSen.GetValue(17), 43.675M);
            Assert.AreEqual(tenkanSen.GetValue(18), 43.525M);

            Assert.AreEqual(kijunSen.GetValue(3), 45.14M);
            Assert.AreEqual(kijunSen.GetValue(4), 45.14M);
            Assert.AreEqual(kijunSen.GetValue(5), 45.155M);
            Assert.AreEqual(kijunSen.GetValue(6), 45.18M);
            Assert.AreEqual(kijunSen.GetValue(7), 45.145M);
            Assert.AreEqual(kijunSen.GetValue(8), 45.17M);
            Assert.AreEqual(kijunSen.GetValue(16), 44.345M);
            Assert.AreEqual(kijunSen.GetValue(17), 44.305M);
            Assert.AreEqual(kijunSen.GetValue(18), 44.05M);

            Assert.AreEqual(senkouSpanA.GetValue(3), 45.1475M);
            Assert.AreEqual(senkouSpanA.GetValue(4), 45.16M);
            Assert.AreEqual(senkouSpanA.GetValue(5), 45.15M);
            Assert.AreEqual(senkouSpanA.GetValue(6), 45.1575M);
            Assert.AreEqual(senkouSpanA.GetValue(7), 45.145M);
            Assert.AreEqual(senkouSpanA.GetValue(8), 45.17M);
            Assert.AreEqual(senkouSpanA.GetValue(16), 44.2025M);
            Assert.AreEqual(senkouSpanA.GetValue(17), 43.99M);
            Assert.AreEqual(senkouSpanA.GetValue(18), 43.7875M);

            Assert.AreEqual(senkouSpanB.GetValue(3), 45.14M);
            Assert.AreEqual(senkouSpanB.GetValue(4), 45.14M);
            Assert.AreEqual(senkouSpanB.GetValue(5), 45.14M);
            Assert.AreEqual(senkouSpanB.GetValue(6), 45.14M);
            Assert.AreEqual(senkouSpanB.GetValue(7), 45.14M);
            Assert.AreEqual(senkouSpanB.GetValue(8), 45.14M);
            Assert.AreEqual(senkouSpanB.GetValue(16), 44.345M);
            Assert.AreEqual(senkouSpanB.GetValue(17), 44.335M);
            Assert.AreEqual(senkouSpanB.GetValue(18), 44.335M);

            Assert.AreEqual(chikouSpan.GetValue(5), 45.05M);
            Assert.AreEqual(chikouSpan.GetValue(6), 45.10M);
            Assert.AreEqual(chikouSpan.GetValue(7), 45.19M);
            Assert.AreEqual(chikouSpan.GetValue(8), 45.14M);
            Assert.AreEqual(chikouSpan.GetValue(19), 44.23M);
        }
    }
}