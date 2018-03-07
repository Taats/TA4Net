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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Indicators.Helpers.Types;

    [TestClass]
    public class BooleanTransformIndicatorTest
    {

        private BooleanTransformIndicator transEquals;
        private BooleanTransformIndicator transIsGreaterThan;
        private BooleanTransformIndicator transIsGreaterThanOrEqual;
        private BooleanTransformIndicator transIsLessThan;
        private BooleanTransformIndicator transIsLessThanOrEqual;

        private BooleanTransformIndicator transIsNaN;
        private BooleanTransformIndicator transIsNegative;
        private BooleanTransformIndicator transIsNegativeOrZero;
        private BooleanTransformIndicator transIsPositive;
        private BooleanTransformIndicator transIsPositiveOrZero;
        private BooleanTransformIndicator transIsZero;


        [TestInitialize]
        public void setUp()
        {
            ConstantIndicator<decimal> constantIndicator = new ConstantIndicator<decimal>(4);

            transEquals = new BooleanTransformIndicator(constantIndicator, 4, BooleanTransformType.equals);
            transIsGreaterThan = new BooleanTransformIndicator(constantIndicator, 3, BooleanTransformType.IsGreaterThan);
            transIsGreaterThanOrEqual = new BooleanTransformIndicator(constantIndicator, 4, BooleanTransformType.IsGreaterThanOrEqual);
            transIsLessThan = new BooleanTransformIndicator(constantIndicator, Decimals.TEN, BooleanTransformType.IsLessThan);
            transIsLessThanOrEqual = new BooleanTransformIndicator(constantIndicator, 4, BooleanTransformType.IsLessThanOrEqual);

            transIsNaN = new BooleanTransformIndicator(constantIndicator, BooleanTransformSimpleType.isNaN);
            transIsNegative = new BooleanTransformIndicator(new ConstantIndicator<decimal>(-4), BooleanTransformSimpleType.isNegative);
            transIsNegativeOrZero = new BooleanTransformIndicator(constantIndicator, BooleanTransformSimpleType.isNegativeOrZero);
            transIsPositive = new BooleanTransformIndicator(constantIndicator, BooleanTransformSimpleType.isPositive);
            transIsPositiveOrZero = new BooleanTransformIndicator(constantIndicator, BooleanTransformSimpleType.isPositiveOrZero);
            transIsZero = new BooleanTransformIndicator(new ConstantIndicator<decimal>(Decimals.ZERO), BooleanTransformSimpleType.isZero);
        }

        [TestMethod]
        public void getValue()
        {
            Assert.IsTrue(transEquals.GetValue(0));
            Assert.IsTrue(transIsGreaterThan.GetValue(0));
            Assert.IsTrue(transIsGreaterThanOrEqual.GetValue(0));
            Assert.IsTrue(transIsLessThan.GetValue(0));
            Assert.IsTrue(transIsLessThanOrEqual.GetValue(0));

            Assert.IsFalse(transIsNaN.GetValue(0));
            Assert.IsTrue(transIsNegative.GetValue(0));
            Assert.IsFalse(transIsNegativeOrZero.GetValue(0));
            Assert.IsTrue(transIsPositive.GetValue(0));
            Assert.IsTrue(transIsPositiveOrZero.GetValue(0));
            Assert.IsTrue(transIsZero.GetValue(0));
        }
    }
}
