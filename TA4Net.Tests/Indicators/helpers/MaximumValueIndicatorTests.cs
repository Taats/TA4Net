using Microsoft.VisualStudio.TestTools.UnitTesting;
using TA4Net.Indicators.Helpers;

namespace TA4Net.Tests.indicators.helpers
{
    [TestClass]
    public class MaximumValueIndicatorTests
    {
        [TestMethod]
        public void ShouldLimitTheMaximumResult()
        {
            var maximumValue = 2;
            var fixedValueIndicator = new FixeddecimalIndicator(-1, 0, 1, 2, 3, 2, 1);

            var indicator = new MaximumValueIndicator(fixedValueIndicator, maximumValue);

            Assert.AreEqual(-1, indicator.GetValue(0));
            Assert.AreEqual(0, indicator.GetValue(1));
            Assert.AreEqual(1, indicator.GetValue(2));
            Assert.AreEqual(2, indicator.GetValue(3));
            Assert.AreEqual(2, indicator.GetValue(4));
            Assert.AreEqual(2, indicator.GetValue(5));
            Assert.AreEqual(1, indicator.GetValue(6));
        }
    }
}
