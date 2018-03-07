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
namespace TA4Net.Test
{
    using TA4Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Trading.Rules.Types;

    [TestClass] public class OrderTest
    {

        private Order opEquals1, opEquals2, opNotEquals1, opNotEquals2;

        [TestInitialize]
        public void setUp()
        {
            opEquals1 = Order.buyAt(1, Decimals.NaN, Decimals.NaN);
            opEquals2 = Order.buyAt(1, Decimals.NaN, Decimals.NaN);

            opNotEquals1 = Order.sellAt(1, Decimals.NaN, Decimals.NaN);
            opNotEquals2 = Order.buyAt(2, Decimals.NaN, Decimals.NaN);
        }

        [TestMethod]
        public void type()
        {
            Assert.AreEqual(OrderType.SELL, opNotEquals1.GetOrderType());
            Assert.IsFalse(opNotEquals1.isBuy());
            Assert.IsTrue(opNotEquals1.isSell());
            Assert.AreEqual(OrderType.BUY, opNotEquals2.GetOrderType());
            Assert.IsTrue(opNotEquals2.isBuy());
            Assert.IsFalse(opNotEquals2.isSell());
        }

        [TestMethod]
        public void overrideToString()
        {
            Assert.AreEqual(opEquals1.ToString(), opEquals2.ToString());

            Assert.AreNotEqual(opEquals1.ToString(), opNotEquals1.ToString());
            Assert.AreNotEqual(opEquals1.ToString(), opNotEquals2.ToString());
        }
    }
}
