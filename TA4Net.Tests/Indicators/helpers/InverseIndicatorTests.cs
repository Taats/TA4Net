using Microsoft.VisualStudio.TestTools.UnitTesting;
using TA4Net.Indicators.Helpers;

namespace TA4Net.Tests.indicators.helpers
{
    [TestClass]
    public class InverseIndicatorTests
    {
        [TestMethod]
        public void CalculateInverseValues()
        {
            var fixedValueIndicator = new FixeddecimalIndicator(100, 200, 300, -400, -500, 600);

            var indicator = new InverseIndicator(fixedValueIndicator);

            Assert.AreEqual(-100, indicator.GetValue(0));
            Assert.AreEqual(-200, indicator.GetValue(1));
            Assert.AreEqual(-300, indicator.GetValue(2));
            Assert.AreEqual(400, indicator.GetValue(3));
            Assert.AreEqual(500, indicator.GetValue(4));
            Assert.AreEqual(-600, indicator.GetValue(5));
        }
    }
}
