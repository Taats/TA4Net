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
namespace TA4Net.Test.Indicators.helpers
{
    using TA4Net;
    using TA4Net.Indicators.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Indicators.Helpers.Types;

    [TestClass]
    public class decimalTransformIndicatorTest
    {

        private DecimalTransformIndicator transPlus;
        private DecimalTransformIndicator transMinus;
        private DecimalTransformIndicator transMultiply;
        private DecimalTransformIndicator transDivide;
        private DecimalTransformIndicator transMax;
        private DecimalTransformIndicator transMin;

        private DecimalTransformIndicator transAbs;
        private DecimalTransformIndicator transSqrt;
        private DecimalTransformIndicator transLog;

        [TestInitialize]
        public void setUp()
        {
            ConstantIndicator<decimal> constantIndicator = new ConstantIndicator<decimal>(4);

            transPlus = new DecimalTransformIndicator(constantIndicator, Decimals.TEN, decimalTransformType.Plus);
            transMinus = new DecimalTransformIndicator(constantIndicator, Decimals.TEN, decimalTransformType.Minus);
            transMultiply = new DecimalTransformIndicator(constantIndicator, Decimals.TEN, decimalTransformType.multiply);
            transDivide = new DecimalTransformIndicator(constantIndicator, Decimals.TEN, decimalTransformType.divide);
            transMax = new DecimalTransformIndicator(constantIndicator, Decimals.TEN, decimalTransformType.Max);
            transMin = new DecimalTransformIndicator(constantIndicator, Decimals.TEN, decimalTransformType.Min);

            transAbs = new DecimalTransformIndicator(new ConstantIndicator<decimal>(-4), decimalTransformSimpleType.abs);
            transSqrt = new DecimalTransformIndicator(constantIndicator, decimalTransformSimpleType.sqrt);
            transLog = new DecimalTransformIndicator(constantIndicator, decimalTransformSimpleType.log);
        }

        [TestMethod]
        public void getValue()
        {
            Assert.AreEqual(transPlus.GetValue(0), 14M);
            Assert.AreEqual(transMinus.GetValue(0), -6M);
            Assert.AreEqual(transMultiply.GetValue(0), 40M);
            Assert.AreEqual(transDivide.GetValue(0), 0.4M);
            Assert.AreEqual(transMax.GetValue(0), 10M);
            Assert.AreEqual(transMin.GetValue(0), 4M);

            Assert.AreEqual(transAbs.GetValue(0), 4M);
            Assert.AreEqual(transSqrt.GetValue(0), 2M);
            Assert.AreEqual(transLog.GetValue(0), 1.3862943611198906188344642429M);
        }
    }
}
