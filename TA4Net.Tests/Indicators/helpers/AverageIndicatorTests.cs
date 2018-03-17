using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TA4Net.Indicators.Helpers;
using TA4Net.Interfaces;
using TA4Net.Mocks;

namespace TA4Net.Tests.indicators.helpers
{
    [TestClass]
    public class AverageIndicatorTests
    {
        [TestMethod]
        public void CalculateOHCLAverage()
        {
            var series = new MockTimeSeries(new List<IBar>() {
                new MockBar(10, 20, 30, 40),
                new MockBar(20, 20, 30, 30),
                new MockBar(0, 10, 20, 30),
                new MockBar(1, 2, 3, 4)
            });

            var indicator = new AverageIndicator(series);

            Assert.AreEqual(25, indicator.GetValue(0));
            Assert.AreEqual(25, indicator.GetValue(1));
            Assert.AreEqual(15, indicator.GetValue(2));
            Assert.AreEqual(2.5m, indicator.GetValue(3));
        }

        [TestMethod]
        public void TestWithIndicatorsInput()
        {
            var fixedValueIndicator1 = new FixeddecimalIndicator(10, 20, 30, 40, 50);
            var fixedValueIndicator2 = new FixeddecimalIndicator(100, 200, 300, 400, 500);

            var indicator = new AverageIndicator(fixedValueIndicator1, fixedValueIndicator2);

            Assert.AreEqual(55, indicator.GetValue(0));
            Assert.AreEqual(110, indicator.GetValue(1));
            Assert.AreEqual(165, indicator.GetValue(2));
            Assert.AreEqual(220, indicator.GetValue(3));
            Assert.AreEqual(275, indicator.GetValue(4));
        }
    }
}
