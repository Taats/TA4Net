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
    using System;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class AroonUpIndicatorTest
    {

        private ITimeSeries data;

        [TestInitialize]
        public void init()
        {
            data = new BaseTimeSeries();
            data.AddBar(new BaseBar(DateTime.Now.AddDays(1), 168.28M, 169.87M, 167.15M, 169.64M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(2), 168.84M, 169.36M, 168.2M, 168.71M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(3), 168.88M, 169.29M, 166.41M, 167.74M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(4), 168M, 168.38M, 166.18M, 166.32M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(5), 166.89M, 167.7M, 166.33M, 167.24M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(6), 165.25M, 168.43M, 165M, 168.05M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(7), 168.17M, 170.18M, 167.63M, 169.92M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(8), 170.42M, 172.15M, 170.06M, 171.97M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(9), 172.41M, 172.92M, 171.31M, 172.02M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(10), 171.2M, 172.39M, 169.55M, 170.72M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(11), 170.91M, 172.48M, 169.57M, 172.09M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(12), 171.8M, 173.31M, 170.27M, 173.21M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(13), 173.09M, 173.49M, 170.8M, 170.95M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(14), 172.41M, 173.89M, 172.2M, 173.51M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(15), 173.87M, 174.17M, 175M, 172.96M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(16), 173M, 173.17M, 172.06M, 173.05M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(17), 172.26M, 172.28M, 170.5M, 170.96M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(18), 170.88M, 172.34M, 170.26M, 171.64M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(19), 171.85M, 172.07M, 169.34M, 170.01M, 0));
            data.AddBar(new BaseBar(DateTime.Now.AddDays(20), 170.75M, 172.56M, 170.36M, 172.52M, 0)); // FB, daily, 9.19.'17

        }

        [TestMethod]
        public void upAndSlowDown()
        {
            AroonUpIndicator arronUp = new AroonUpIndicator(data, 5);
            Assert.AreEqual(arronUp.GetValue(19), 0);
            Assert.AreEqual(arronUp.GetValue(18), 20);
            Assert.AreEqual(arronUp.GetValue(17), 40);
            Assert.AreEqual(arronUp.GetValue(16), 60);
            Assert.AreEqual(arronUp.GetValue(15), 80);
            Assert.AreEqual(arronUp.GetValue(14), 100);
            Assert.AreEqual(arronUp.GetValue(13), 100);
            Assert.AreEqual(arronUp.GetValue(12), 100);
            Assert.AreEqual(arronUp.GetValue(11), 100);
            Assert.AreEqual(arronUp.GetValue(10), 60);
            Assert.AreEqual(arronUp.GetValue(9), 80);
            Assert.AreEqual(arronUp.GetValue(8), 100);
            Assert.AreEqual(arronUp.GetValue(7), 100);
            Assert.AreEqual(arronUp.GetValue(6), 100);
            Assert.AreEqual(arronUp.GetValue(5), 0);

        }


        [TestMethod]
        public void onlyNaNValues()
        {
            List<IBar> bars = new List<IBar>();
            for (long i = 0; i <= 1000; i++)
            {
                IBar bar = new BaseBar(DateTime.Now.AddDays(i), Decimals.NaN, Decimals.NaN, Decimals.NaN, Decimals.NaN, Decimals.NaN);
                bars.Add(bar);
            }

            BaseTimeSeries series = new BaseTimeSeries("NaN test", bars);
            AroonUpIndicator aroonUpIndicator = new AroonUpIndicator(series, 5);
            for (int i = series.GetBeginIndex(); i <= series.GetEndIndex(); i++)
            {
                Assert.AreEqual(Decimals.NaN.ToString(), aroonUpIndicator.GetValue(i).ToString());
            }
        }

        [TestMethod]
        public void naNValuesInIntervall()
        {
            List<IBar> bars = new List<IBar>();
            for (long i = 0; i <= 10; i++)
            { // (0, NaN, 2, NaN, 4, NaN, 6, NaN, 8, ...)
                decimal maxPrice = i % 2 == 0 ? i : Decimals.NaN;
                IBar bar = new BaseBar(DateTime.Now.AddDays(i), Decimals.NaN, maxPrice, Decimals.NaN, Decimals.NaN, Decimals.NaN);
                bars.Add(bar);
            }
            BaseTimeSeries series = new BaseTimeSeries("NaN test", bars);
            AroonUpIndicator aroonUpIndicator = new AroonUpIndicator(series, 5);
            for (int i = series.GetBeginIndex(); i <= series.GetEndIndex(); i++)
            {
                if (i % 2 != 0)
                {
                    Assert.AreEqual(Decimals.NaN, aroonUpIndicator.GetValue(i));
                }
                else
                {
                    Assert.AreEqual(Decimals.HUNDRED, aroonUpIndicator.GetValue(i));
                }
            }
        }
    }
}
