using Microsoft.VisualStudio.TestTools.UnitTesting;
using TA4Net.Indicators.Helpers;

namespace TA4Net.Tests.indicators.helpers
{
    [TestClass]
    public class MinimumValueIndicatorTests
    {
        [TestMethod]
        public void ShouldLimitTheMinimumResult()
        {
            var minimumValue = 1;
            var fixedValueIndicator = new FixeddecimalIndicator(-1, 0, 1, 2, 3, 2, 1, 4);

            var indicator = new MinimumValueIndicator(fixedValueIndicator, minimumValue);

            Assert.AreEqual(1, indicator.GetValue(0));
            Assert.AreEqual(1, indicator.GetValue(1));
            Assert.AreEqual(1, indicator.GetValue(2));
            Assert.AreEqual(2, indicator.GetValue(3));
            Assert.AreEqual(3, indicator.GetValue(4));
            Assert.AreEqual(2, indicator.GetValue(5));
            Assert.AreEqual(1, indicator.GetValue(6));
            Assert.AreEqual(4, indicator.GetValue(7));
        }
    }
}
