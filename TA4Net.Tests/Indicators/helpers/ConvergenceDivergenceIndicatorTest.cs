/**
 * The MIT License (MIT)
 *
 * Copyright (c) 2014-2017 Marc de Verdelhan & respective authors (see AUTHORS)
 *
 * Permission is hereby granteM, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
 * the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KINM, EXPRESS OR
 * IMPLIEM, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
namespace TA4Net.Test.Indicators.helpers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Indicators.Helpers.Types;
    using TA4Net.Interfaces;

    [TestClass]
    public class ConvergenceDivergenceIndicatorTest
    {

        private IIndicator<decimal> refPosCon;
        private IIndicator<decimal> otherPosCon;

        private IIndicator<decimal> refNegCon;
        private IIndicator<decimal> otherNegCon;

        private IIndicator<decimal> refPosDiv;
        private IIndicator<decimal> otherNegDiv;

        private IIndicator<decimal> refNegDig;
        private IIndicator<decimal> otherPosDiv;

        private ConvergenceDivergenceIndicator isPosCon;
        private ConvergenceDivergenceIndicator isNegCon;

        private ConvergenceDivergenceIndicator isPosDiv;
        private ConvergenceDivergenceIndicator isNegDiv;

        private ConvergenceDivergenceIndicator isPosConStrict;
        private ConvergenceDivergenceIndicator isNegConStrict;

        private ConvergenceDivergenceIndicator isPosDivStrict;
        private ConvergenceDivergenceIndicator isNegDivStrict;


        [TestInitialize]
        public void setUp()
        {
            refPosCon = new FixeddecimalIndicator(1, 2, 3, 4, 5, 8, 3, 2, -2, 1);
            otherPosCon = new FixeddecimalIndicator(10, 20, 30, 40, 50, 60, 7, 5, 3, 2);

            refNegCon = new FixeddecimalIndicator(150, 60, 20, 10, -20, -60, -200, -1, -200, 100);
            otherNegCon = new FixeddecimalIndicator(80, 50, 40, 20, 10, 0, -30, -50, -150, 7);

            refPosDiv = new FixeddecimalIndicator(1, 4, 8, 12, 15, 20, 3, 2, -2, 1);
            otherNegDiv = new FixeddecimalIndicator(80, 50, 20, -10, 0, -100, -200, -2, 5, 7);

            refNegDig = new FixeddecimalIndicator(100, 30, 15, 4, 2, -10, -3, -100, -2, 100);
            otherPosDiv = new FixeddecimalIndicator(20, 40, 70, 80, 90, 100, 200, 220, -50, 7);

            // for convergence and divergence
            isPosCon = new ConvergenceDivergenceIndicator(refPosCon, otherPosCon, 3, ConvergenceDivergenceType.positiveConvergent);
            isNegCon = new ConvergenceDivergenceIndicator(refNegCon, otherNegCon, 3, ConvergenceDivergenceType.negativeConvergent);
            isPosDiv = new ConvergenceDivergenceIndicator(refPosDiv, otherNegDiv, 3, ConvergenceDivergenceType.positiveDivergent);
            isNegDiv = new ConvergenceDivergenceIndicator(refNegDig, otherPosDiv, 3, ConvergenceDivergenceType.negativeDivergent);

            // for strict convergence and divergence 
            isPosConStrict = new ConvergenceDivergenceIndicator(refPosCon, otherPosDiv, 3, ConvergenceDivergenceStrictType.positiveConvergentStrict);
            isNegConStrict = new ConvergenceDivergenceIndicator(refNegDig, otherNegCon, 3, ConvergenceDivergenceStrictType.negativeConvergentStrict);
            isPosDivStrict = new ConvergenceDivergenceIndicator(otherPosDiv, refNegDig, 3, ConvergenceDivergenceStrictType.positiveDivergentStrict);
            isNegDivStrict = new ConvergenceDivergenceIndicator(refNegDig, otherPosDiv, 3, ConvergenceDivergenceStrictType.negativeDivergentStrict);
        }

        [TestMethod]
        public void isSatisfied()
        {

          //  testPositiveConvergent();
            testNegativeConvergent();
            testPositiveDivergent();
            testNegativeDivergent();

            //		testPositiveConvergentStrict();
            //		testNegativeConvergentStrict();
            //		testPositiveDivergentStrict();
            //		testNegativeDivergentStrict();

        }

        public void testPositiveConvergent()
        {
            Assert.IsFalse(isPosCon.GetValue(0));
            Assert.IsFalse(isPosCon.GetValue(1));
            Assert.IsFalse(isPosCon.GetValue(2));
            Assert.IsTrue(isPosCon.GetValue(3));
            Assert.IsTrue(isPosCon.GetValue(4));
            Assert.IsTrue(isPosCon.GetValue(5));
            Assert.IsFalse(isPosCon.GetValue(6));
            Assert.IsFalse(isPosCon.GetValue(7));
        //    Assert.IsTrue(isPosCon.getValue(8));
            Assert.IsFalse(isPosCon.GetValue(9));
        }

        public void testNegativeConvergent()
        {
            Assert.IsFalse(isNegCon.GetValue(0));
            Assert.IsFalse(isNegCon.GetValue(1));
            Assert.IsFalse(isNegCon.GetValue(2));
            Assert.IsTrue(isNegCon.GetValue(3));
        //    Assert.IsFalse(isNegCon.getValue(4));
            Assert.IsFalse(isNegCon.GetValue(5));
            Assert.IsFalse(isNegCon.GetValue(6));
            Assert.IsFalse(isNegCon.GetValue(7));
            Assert.IsFalse(isNegCon.GetValue(8));
        //    Assert.IsFalse(isNegCon.getValue(9));
        }

        public void testPositiveDivergent()
        {
            Assert.IsFalse(isPosDiv.GetValue(0));
            Assert.IsFalse(isPosDiv.GetValue(1));
            Assert.IsFalse(isPosDiv.GetValue(2));
            Assert.IsTrue(isPosDiv.GetValue(3));
            Assert.IsFalse(isPosDiv.GetValue(4));
            Assert.IsTrue(isPosDiv.GetValue(5));
            Assert.IsFalse(isPosDiv.GetValue(6));
            Assert.IsFalse(isPosDiv.GetValue(7));
            Assert.IsFalse(isPosDiv.GetValue(8));
            Assert.IsFalse(isPosDiv.GetValue(9));
        }

        public void testNegativeDivergent()
        {
            Assert.IsFalse(isNegDiv.GetValue(0));
            Assert.IsFalse(isNegDiv.GetValue(1));
            Assert.IsFalse(isNegDiv.GetValue(2));
            Assert.IsTrue(isNegDiv.GetValue(3));
            Assert.IsTrue(isNegDiv.GetValue(4));
            Assert.IsFalse(isNegDiv.GetValue(5));
            Assert.IsFalse(isNegDiv.GetValue(6));
            Assert.IsFalse(isNegDiv.GetValue(7));
            Assert.IsFalse(isNegDiv.GetValue(8));
            Assert.IsFalse(isNegDiv.GetValue(9));
        }

        public void testPositiveConvergentStrict()
        {
            Assert.IsFalse(isPosConStrict.GetValue(0));
            Assert.IsFalse(isPosConStrict.GetValue(1));
            Assert.IsFalse(isPosConStrict.GetValue(2));
            Assert.IsTrue(isPosConStrict.GetValue(3));
            Assert.IsTrue(isPosConStrict.GetValue(4));
            Assert.IsTrue(isPosConStrict.GetValue(5));
            Assert.IsFalse(isPosConStrict.GetValue(6));
            Assert.IsFalse(isPosConStrict.GetValue(7));
            Assert.IsTrue(isPosConStrict.GetValue(8));
            Assert.IsFalse(isPosConStrict.GetValue(9));
        }

        public void testNegativeConvergentStrict()
        {
            Assert.IsFalse(isNegConStrict.GetValue(0));
            Assert.IsFalse(isNegConStrict.GetValue(1));
            Assert.IsFalse(isNegConStrict.GetValue(2));
            Assert.IsTrue(isNegConStrict.GetValue(3));
            Assert.IsFalse(isNegConStrict.GetValue(4));
            Assert.IsFalse(isNegConStrict.GetValue(5));
            Assert.IsFalse(isNegConStrict.GetValue(6));
            Assert.IsFalse(isNegConStrict.GetValue(7));
            Assert.IsFalse(isNegConStrict.GetValue(8));
            Assert.IsFalse(isNegConStrict.GetValue(9));
        }

        public void testPositiveDivergentStrict()
        {
            Assert.IsFalse(isPosDivStrict.GetValue(0));
            Assert.IsFalse(isPosDivStrict.GetValue(1));
            Assert.IsFalse(isPosDivStrict.GetValue(2));
            Assert.IsTrue(isPosDivStrict.GetValue(3));
            Assert.IsFalse(isPosDivStrict.GetValue(4));
            Assert.IsTrue(isPosDivStrict.GetValue(5));
            Assert.IsFalse(isPosDivStrict.GetValue(6));
            Assert.IsFalse(isPosDivStrict.GetValue(7));
            Assert.IsFalse(isPosDivStrict.GetValue(8));
            Assert.IsFalse(isPosDivStrict.GetValue(9));
        }

        public void testNegativeDivergentStrict()
        {
            Assert.IsFalse(isNegDivStrict.GetValue(0));
            Assert.IsFalse(isNegDivStrict.GetValue(1));
            Assert.IsFalse(isNegDivStrict.GetValue(2));
            Assert.IsTrue(isNegDivStrict.GetValue(3));
            Assert.IsTrue(isNegDivStrict.GetValue(4));
            Assert.IsFalse(isNegDivStrict.GetValue(5));
            Assert.IsFalse(isNegDivStrict.GetValue(6));
            Assert.IsFalse(isNegDivStrict.GetValue(7));
            Assert.IsFalse(isNegDivStrict.GetValue(8));
            Assert.IsFalse(isNegDivStrict.GetValue(9));
        }

    }
}