using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TA4Net.Indicators.Helpers;

namespace TA4Net.Tests.indicators.helpers
{
    [TestClass]
    public class MinusIndicatorTests
    {
        [TestMethod]
        public void TestWith2Indicators()
        {
            var fixedValueIndicator1 = new FixeddecimalIndicator(100, 200, 300, 400, 500);
            var fixedValueIndicator2 = new FixeddecimalIndicator(10, 20, 30, 40, 50);

            var indicator = new MinusIndicator(fixedValueIndicator1, fixedValueIndicator2);

            Assert.AreEqual(90, indicator.GetValue(0));
            Assert.AreEqual(180, indicator.GetValue(1));
            Assert.AreEqual(270, indicator.GetValue(2));
            Assert.AreEqual(360, indicator.GetValue(3));
            Assert.AreEqual(450, indicator.GetValue(4));
        }

        [TestMethod]
        public void TestWith3Indicators()
        {
            var fixedValueIndicator1 = new FixeddecimalIndicator(100, 200, 300, 400, 500);
            var fixedValueIndicator2 = new FixeddecimalIndicator(10, 20, 30, 40, 50);
            var fixedValueIndicator3 = new FixeddecimalIndicator(1000, 120, 530, 140, -50);

            var indicator = new MinusIndicator(fixedValueIndicator1, fixedValueIndicator2, fixedValueIndicator3);

            Assert.AreEqual(-910, indicator.GetValue(0));
            Assert.AreEqual(60, indicator.GetValue(1));
            Assert.AreEqual(-260, indicator.GetValue(2));
            Assert.AreEqual(220, indicator.GetValue(3));
            Assert.AreEqual(500, indicator.GetValue(4));
        }
    }
}
