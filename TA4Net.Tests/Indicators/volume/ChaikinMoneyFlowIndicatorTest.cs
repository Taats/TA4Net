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
namespace TA4Net.Test.Indicators.volume
{
    using TA4Net;
    using TA4Net.Indicators.Volume;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class ChaikinMoneyFlowIndicatorTest
    {

        [TestMethod]
        public void getValue()
        {

            DateTime now = DateTime.Now;
            List<IBar> bars = new List<IBar>();
            bars.Add(new BaseBar(now, 0M, 62.34M, 61.37M, 62.15M, 7849.025M));
            bars.Add(new BaseBar(now, 0M, 62.05M, 60.69M, 60.81M, 11692.075M));
            bars.Add(new BaseBar(now, 0M, 62.27M, 60.10M, 60.45M, 10575.307M));
            bars.Add(new BaseBar(now, 0M, 60.79M, 58.61M, 59.18M, 13059.128M));
            bars.Add(new BaseBar(now, 0M, 59.93M, 58.71M, 59.24M, 20733.508M));
            bars.Add(new BaseBar(now, 0M, 61.75M, 59.86M, 60.20M, 29630.096M));
            bars.Add(new BaseBar(now, 0M, 60.00M, 57.97M, 58.48M, 17705.294M));
            bars.Add(new BaseBar(now, 0M, 59.00M, 58.02M, 58.24M, 7259.203M));
            bars.Add(new BaseBar(now, 0M, 59.07M, 57.48M, 58.69M, 10474.629M));
            bars.Add(new BaseBar(now, 0M, 59.22M, 58.30M, 58.65M, 5203.714M));
            bars.Add(new BaseBar(now, 0M, 58.75M, 57.83M, 58.47M, 3422.865M));
            bars.Add(new BaseBar(now, 0M, 58.65M, 57.86M, 58.02M, 3962.150M));
            bars.Add(new BaseBar(now, 0M, 58.47M, 57.91M, 58.17M, 4095.905M));
            bars.Add(new BaseBar(now, 0M, 58.25M, 57.83M, 58.07M, 3766.006M));
            bars.Add(new BaseBar(now, 0M, 58.35M, 57.53M, 58.13M, 4239.335M));
            bars.Add(new BaseBar(now, 0M, 59.86M, 58.58M, 58.94M, 8039.979M));
            bars.Add(new BaseBar(now, 0M, 59.53M, 58.30M, 59.10M, 6956.717M));
            bars.Add(new BaseBar(now, 0M, 62.10M, 58.53M, 61.92M, 18171.552M));
            bars.Add(new BaseBar(now, 0M, 62.16M, 59.80M, 61.37M, 22225.894M));

            bars.Add(new BaseBar(now, 0M, 62.67M, 60.93M, 61.68M, 14613.509M));
            bars.Add(new BaseBar(now, 0M, 62.38M, 60.15M, 62.09M, 12319.763M));
            bars.Add(new BaseBar(now, 0M, 63.73M, 62.26M, 62.89M, 15007.690M));
            bars.Add(new BaseBar(now, 0M, 63.85M, 63.00M, 63.53M, 8879.667M));
            bars.Add(new BaseBar(now, 0M, 66.15M, 63.58M, 64.01M, 22693.812M));
            bars.Add(new BaseBar(now, 0M, 65.34M, 64.07M, 64.77M, 10191.814M));
            bars.Add(new BaseBar(now, 0M, 66.48M, 65.20M, 65.22M, 10074.152M));
            bars.Add(new BaseBar(now, 0M, 65.23M, 63.21M, 63.28M, 9411.620M));
            bars.Add(new BaseBar(now, 0M, 63.40M, 61.88M, 62.40M, 10391.690M));
            bars.Add(new BaseBar(now, 0M, 63.18M, 61.11M, 61.55M, 8926.512M));
            bars.Add(new BaseBar(now, 0M, 62.70M, 61.25M, 62.69M, 7459.575M));
            ITimeSeries series = new BaseTimeSeries(bars);

            ChaikinMoneyFlowIndicator cmf = new ChaikinMoneyFlowIndicator(series, 20);

            Assert.AreEqual(cmf.GetValue(0), 0.6082474226804123711340206186M);
            Assert.AreEqual(cmf.GetValue(1), -0.2484311743072140165124562343M);
            Assert.AreEqual(cmf.GetValue(19), -0.12109525344467201822861587M);
            Assert.AreEqual(cmf.GetValue(20), -0.0996934222876103824404821392M);
            Assert.AreEqual(cmf.GetValue(21), -0.065928274226956289145366249M);
            Assert.AreEqual(cmf.GetValue(22), -0.0256877564683178956522684246M);
            Assert.AreEqual(cmf.GetValue(23), -0.0617035928215171828140621566M);
            Assert.AreEqual(cmf.GetValue(24), -0.048105977389585559132748056M);
            Assert.AreEqual(cmf.GetValue(25), -0.0085966816372546350865067276M);
            Assert.AreEqual(cmf.GetValue(26), -0.0087040876420881401439180481M);
            Assert.AreEqual(cmf.GetValue(27), -0.005051500548553229341727545M);
            Assert.AreEqual(cmf.GetValue(28), -0.057409204188786737790197221M);
            Assert.AreEqual(cmf.GetValue(29), -0.0147899751712651882445236589M);
        }
    }
}