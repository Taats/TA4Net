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
    using TA4Net.Interfaces;

    [TestClass]
    public class TradingRecordTest
    {

        private ITradingRecord emptyRecord, openedRecord, closedRecord;

        [TestInitialize]
        public void SetUp()
        {
            emptyRecord = new BaseTradingRecord();
            openedRecord = new BaseTradingRecord(
               Order.buyAt(0, Decimals.NaN, Decimals.NaN), 
               Order.sellAt(3, Decimals.NaN, Decimals.NaN),
               Order.buyAt(7, Decimals.NaN, Decimals.NaN)
            );
            closedRecord = new BaseTradingRecord(
                Order.buyAt(0, Decimals.NaN, Decimals.NaN), 
                Order.sellAt(3, Decimals.NaN, Decimals.NaN),
                Order.buyAt(7, Decimals.NaN, Decimals.NaN), 
                Order.sellAt(8, Decimals.NaN, Decimals.NaN)
            );
        }

        [TestMethod]
        public void GetCurrentTrade()
        {
            Assert.IsTrue(emptyRecord.GetCurrentTrade().IsNew());
            Assert.IsTrue(openedRecord.GetCurrentTrade().IsOpened());
            Assert.IsTrue(closedRecord.GetCurrentTrade().IsNew());
        }

        [TestMethod]
        public void Operate()
        {
            ITradingRecord record = new BaseTradingRecord();

            record.Operate(1);
            Assert.IsTrue(record.GetCurrentTrade().IsOpened());
            Assert.AreEqual(0, record.GetTradeCount());
            Assert.IsNull(record.GetLastTrade());
            Assert.AreEqual(Order.buyAt(1, Decimals.NaN, Decimals.NaN), record.GetLastOrder());
            Assert.AreEqual(Order.buyAt(1, Decimals.NaN, Decimals.NaN), record.GetLastOrder(OrderType.BUY));
            Assert.IsNull(record.GetLastOrder(OrderType.SELL));
            Assert.AreEqual(Order.buyAt(1, Decimals.NaN, Decimals.NaN), record.GetLastEntry());
            Assert.IsNull(record.GetLastExit());

            record.Operate(3);
            Assert.IsTrue(record.GetCurrentTrade().IsNew());
            Assert.AreEqual(1, record.GetTradeCount());
            Assert.AreEqual(new Trade(Order.buyAt(1, Decimals.NaN, Decimals.NaN), Order.sellAt(3, Decimals.NaN, Decimals.NaN)), record.GetLastTrade());
            Assert.AreEqual(Order.sellAt(3, Decimals.NaN, Decimals.NaN), record.GetLastOrder());
            Assert.AreEqual(Order.buyAt(1, Decimals.NaN, Decimals.NaN), record.GetLastOrder(OrderType.BUY));
            Assert.AreEqual(Order.sellAt(3, Decimals.NaN, Decimals.NaN), record.GetLastOrder(OrderType.SELL));
            Assert.AreEqual(Order.buyAt(1, Decimals.NaN, Decimals.NaN), record.GetLastEntry());
            Assert.AreEqual(Order.sellAt(3, Decimals.NaN, Decimals.NaN), record.GetLastExit());

            record.Operate(5);
            Assert.IsTrue(record.GetCurrentTrade().IsOpened());
            Assert.AreEqual(1, record.GetTradeCount());
            Assert.AreEqual(new Trade(Order.buyAt(1, Decimals.NaN, Decimals.NaN), Order.sellAt(3, Decimals.NaN, Decimals.NaN)), record.GetLastTrade());
            Assert.AreEqual(Order.buyAt(5, Decimals.NaN, Decimals.NaN), record.GetLastOrder());
            Assert.AreEqual(Order.buyAt(5, Decimals.NaN, Decimals.NaN), record.GetLastOrder(OrderType.BUY));
            Assert.AreEqual(Order.sellAt(3, Decimals.NaN, Decimals.NaN), record.GetLastOrder(OrderType.SELL));
            Assert.AreEqual(Order.buyAt(5, Decimals.NaN, Decimals.NaN), record.GetLastEntry());
            Assert.AreEqual(Order.sellAt(3, Decimals.NaN, Decimals.NaN), record.GetLastExit());
        }

        [TestMethod]
        public void IsClosed()
        {
            Assert.IsTrue(emptyRecord.IsClosed());
            Assert.IsFalse(openedRecord.IsClosed());
            Assert.IsTrue(closedRecord.IsClosed());
        }

        [TestMethod]
        public void GetTradeCount()
        {
            Assert.AreEqual(0, emptyRecord.GetTradeCount());
            Assert.AreEqual(1, openedRecord.GetTradeCount());
            Assert.AreEqual(2, closedRecord.GetTradeCount());
        }

        [TestMethod]
        public void GetLastTrade()
        {
            Assert.IsNull(emptyRecord.GetLastTrade());
            Assert.AreEqual(new Trade(Order.buyAt(0, Decimals.NaN, Decimals.NaN), Order.sellAt(3, Decimals.NaN, Decimals.NaN)), openedRecord.GetLastTrade());
            Assert.AreEqual(new Trade(Order.buyAt(7, Decimals.NaN, Decimals.NaN), Order.sellAt(8, Decimals.NaN, Decimals.NaN)), closedRecord.GetLastTrade());
        }

        [TestMethod]
        public void GetLastOrder()
        {
            // Last order
            Assert.IsNull(emptyRecord.GetLastOrder());
            Assert.AreEqual(Order.buyAt(7, Decimals.NaN, Decimals.NaN), openedRecord.GetLastOrder());
            Assert.AreEqual(Order.sellAt(8, Decimals.NaN, Decimals.NaN), closedRecord.GetLastOrder());
            // Last BUY order
            Assert.IsNull(emptyRecord.GetLastOrder(OrderType.BUY));
            Assert.AreEqual(Order.buyAt(7, Decimals.NaN, Decimals.NaN), openedRecord.GetLastOrder(OrderType.BUY));
            Assert.AreEqual(Order.buyAt(7, Decimals.NaN, Decimals.NaN), closedRecord.GetLastOrder(OrderType.BUY));
            // Last SELL order
            Assert.IsNull(emptyRecord.GetLastOrder(OrderType.SELL));
            Assert.AreEqual(Order.sellAt(3, Decimals.NaN, Decimals.NaN), openedRecord.GetLastOrder(OrderType.SELL));
            Assert.AreEqual(Order.sellAt(8, Decimals.NaN, Decimals.NaN), closedRecord.GetLastOrder(OrderType.SELL));
        }

        [TestMethod]
        public void GetLastEntryExit()
        {
            // Last entry
            Assert.IsNull(emptyRecord.GetLastEntry());
            Assert.AreEqual(Order.buyAt(7, Decimals.NaN, Decimals.NaN), openedRecord.GetLastEntry());
            Assert.AreEqual(Order.buyAt(7, Decimals.NaN, Decimals.NaN), closedRecord.GetLastEntry());
            // Last exit
            Assert.IsNull(emptyRecord.GetLastExit());
            Assert.AreEqual(Order.sellAt(3, Decimals.NaN, Decimals.NaN), openedRecord.GetLastExit());
            Assert.AreEqual(Order.sellAt(8, Decimals.NaN, Decimals.NaN), closedRecord.GetLastExit());
        }
    }
}