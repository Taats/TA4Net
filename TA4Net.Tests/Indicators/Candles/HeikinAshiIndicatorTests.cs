using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TA4Net.Indicators.Candles;
using TA4Net.Interfaces;
using TA4Net.Mocks;

namespace TA4Net.Tests.indicators.Candles
{
    [TestClass]
    public class HeikinAshiIndicatorTests
    {
        [TestMethod]
        public void HeikinAshi()
        {
            var timeSeries = new MockTimeSeries(new List<IBar>
            {
                //          open,   close,  max,    min
                new MockBar(10M,    15M,    20M,    12M),
                new MockBar(16M,    20M,    30M,    5M),
                new MockBar(19M,    17M,    20M,    18M),
                new MockBar(17M,    21M,    22M,    16M)
            });

            var haIndicator = new HeikinAshiIndicator(timeSeries);

            var haBar = haIndicator.GetValue(0);
            Assert.AreEqual(10M, haBar.OpenPrice);
            Assert.AreEqual(15M, haBar.ClosePrice);
            Assert.AreEqual(20M, haBar.MaxPrice);
            Assert.AreEqual(12M, haBar.MinPrice);

            haBar = haIndicator.GetValue(1);
            Assert.AreEqual(12.50M, haBar.OpenPrice);
            Assert.AreEqual(17.75M, haBar.ClosePrice);
            Assert.AreEqual(30M, haBar.MaxPrice);
            Assert.AreEqual(5M, haBar.MinPrice);

            haBar = haIndicator.GetValue(2);
            Assert.AreEqual(15.125M, haBar.OpenPrice);
            Assert.AreEqual(18.50M, haBar.ClosePrice);
            Assert.AreEqual(20M, haBar.MaxPrice);
            Assert.AreEqual(15.125M, haBar.MinPrice);

            haBar = haIndicator.GetValue(3);
            Assert.AreEqual(16.8125M, haBar.OpenPrice);
            Assert.AreEqual(19M, haBar.ClosePrice);
            Assert.AreEqual(22M, haBar.MaxPrice);
            Assert.AreEqual(16M, haBar.MinPrice);
        }
    }
}