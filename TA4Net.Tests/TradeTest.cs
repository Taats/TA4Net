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
    using System;
    using TA4Net.Trading.Rules.Types;

    [TestClass]
    public class TradeTest
    {

        private Trade newTrade, uncoveredTrade, trEquals1, trEquals2, trNotEquals1, trNotEquals2;

        [TestInitialize]
        public void setUp()
        {
            this.newTrade = new Trade();
            this.uncoveredTrade = new Trade(OrderType.SELL);

            trEquals1 = new Trade();
            trEquals1.Operate(1);
            trEquals1.Operate(2);

            trEquals2 = new Trade();
            trEquals2.Operate(1);
            trEquals2.Operate(2);

            trNotEquals1 = new Trade(OrderType.SELL);
            trNotEquals1.Operate(1);
            trNotEquals1.Operate(2);

            trNotEquals2 = new Trade(OrderType.SELL);
            trNotEquals2.Operate(1);
            trNotEquals2.Operate(2);
        }

        [TestMethod]
        public void whenNewShouldCreateBuyOrderWhenEntering()
        {
            newTrade.Operate(0);
            Assert.AreEqual(Order.buyAt(0, Decimals.NaN, Decimals.NaN), newTrade.GetEntry());
        }

        [TestMethod]
        public void whenNewShouldNotExit()
        {
            Assert.IsFalse(newTrade.IsOpened());
        }

        [TestMethod]
        public void whenOpenedShouldCreateSellOrderWhenExiting()
        {
            newTrade.Operate(0);
            newTrade.Operate(1);
            Assert.AreEqual(Order.sellAt(1, Decimals.NaN, Decimals.NaN), newTrade.GetExit());
        }

        [TestMethod]
        public void whenClosedShouldNotEnter()
        {
            newTrade.Operate(0);
            newTrade.Operate(1);
            Assert.IsTrue(newTrade.IsClosed());
            newTrade.Operate(2);
            Assert.IsTrue(newTrade.IsClosed());
        }


        public void whenExitIndexIsLessThanEntryIndexShouldThrowException()
        {
            newTrade.Operate(3);
            Assert.ThrowsException<NotSupportedException>(() => newTrade.Operate(1));
        }

        [TestMethod]
        public void shouldCloseTradeOnSameIndex()
        {
            newTrade.Operate(3);
            newTrade.Operate(3);
            Assert.IsTrue(newTrade.IsClosed());
        }


        public void shouldThrowArgumentExceptionWhenOrdersHaveSameType()
        {
            Assert.ThrowsException<NotSupportedException>(() =>
                new Trade(Order.buyAt(0, Decimals.NaN, Decimals.NaN), Order.buyAt(1, Decimals.NaN, Decimals.NaN))
            );
        }

        [TestMethod]
        public void whenNewShouldCreateSellOrderWhenEnteringUncovered()
        {
            uncoveredTrade.Operate(0);
            Assert.AreEqual(Order.sellAt(0, Decimals.NaN, Decimals.NaN), uncoveredTrade.GetEntry());
        }

        [TestMethod]
        public void whenOpenedShouldCreateBuyOrderWhenExitingUncovered()
        {
            uncoveredTrade.Operate(0);
            uncoveredTrade.Operate(1);
            Assert.AreEqual(Order.buyAt(1, Decimals.NaN, Decimals.NaN), uncoveredTrade.GetExit());
        }

        [TestMethod]
        public void overrideToString()
        {
            Assert.AreEqual(trEquals1.ToString(), trEquals2.ToString());
            Assert.AreNotEqual(trEquals1.ToString(), trNotEquals1.ToString());
            Assert.AreNotEqual(trEquals1.ToString(), trNotEquals2.ToString());
        }
    }
}
