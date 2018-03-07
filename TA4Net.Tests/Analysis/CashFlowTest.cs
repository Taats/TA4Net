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
namespace TA4Net.Test.Analysis
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System.Linq;
    using TA4Net;
    using TA4Net.Analysis;
    using TA4Net.Interfaces;

    [TestClass]
    public class CashFlowTest
    {

        [TestMethod]
        public void CashFlowSize()
        {
            ITimeSeries sampleTimeSeries = new MockTimeSeries(1, 2, 3, 4, 5);
            CashFlow cashFlow = new CashFlow(sampleTimeSeries, new BaseTradingRecord());
            Assert.AreEqual(5, cashFlow.GetSize());
        }

        [TestMethod]
        public void cashFlowBuyWithOnlyOneTrade()
        {
            ITimeSeries sampleTimeSeries = new MockTimeSeries(1, 2);
            ITradingRecord tradingRecord = new BaseTradingRecord(Order.buyAt(0, sampleTimeSeries), Order.sellAt(1, sampleTimeSeries));

            CashFlow cashFlow = new CashFlow(sampleTimeSeries, tradingRecord);

            Assert.AreEqual(cashFlow.GetValue(0), 1);
            Assert.AreEqual(cashFlow.GetValue(1), 2);
        }

        [TestMethod]
        public void cashFlowWithSellAndBuyOrders()
        {
            ITimeSeries sampleTimeSeries = new MockTimeSeries(2, 1, 3, 5, 6, 3, 20);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, sampleTimeSeries), Order.sellAt(1, sampleTimeSeries),
                    Order.buyAt(3, sampleTimeSeries), Order.sellAt(4, sampleTimeSeries),
                    Order.sellAt(5, sampleTimeSeries), Order.buyAt(6, sampleTimeSeries));

            CashFlow cashFlow = new CashFlow(sampleTimeSeries, tradingRecord);

            Assert.AreEqual(cashFlow.GetValue(0), 1);
            Assert.AreEqual(cashFlow.GetValue(1), 0.5M);
            Assert.AreEqual(cashFlow.GetValue(2), 0.5M);
            Assert.AreEqual(cashFlow.GetValue(3), 0.5M);
            Assert.AreEqual(cashFlow.GetValue(4), 0.6M);
            Assert.AreEqual(cashFlow.GetValue(5), 0.6M);
            Assert.AreEqual(cashFlow.GetValue(6), 0.09M);
        }


        [TestMethod]
        public void cashFlowSell()
        {
            ITimeSeries sampleTimeSeries = new MockTimeSeries(1, 2, 4, 8, 16, 32);
            ITradingRecord tradingRecord = new BaseTradingRecord(Order.sellAt(2, sampleTimeSeries), Order.buyAt(3, sampleTimeSeries));

            CashFlow cashFlow = new CashFlow(sampleTimeSeries, tradingRecord);

            Assert.AreEqual(cashFlow.GetValue(0), 1);
            Assert.AreEqual(cashFlow.GetValue(1), 1);
            Assert.AreEqual(cashFlow.GetValue(2), 1);
            Assert.AreEqual(cashFlow.GetValue(3), 0.5M);
            Assert.AreEqual(cashFlow.GetValue(4), 0.5M);
            Assert.AreEqual(cashFlow.GetValue(5), 0.5M);
        }

        [TestMethod]
        public void cashFlowShortSell()
        {
            ITimeSeries sampleTimeSeries = new MockTimeSeries(1, 2, 4, 8, 16, 32);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, sampleTimeSeries), Order.sellAt(2, sampleTimeSeries),
                    Order.sellAt(2, sampleTimeSeries), Order.buyAt(4, sampleTimeSeries),
                    Order.buyAt(4, sampleTimeSeries), Order.sellAt(5, sampleTimeSeries));

            CashFlow cashFlow = new CashFlow(sampleTimeSeries, tradingRecord);

            Assert.AreEqual(cashFlow.GetValue(0), 1);
            Assert.AreEqual(cashFlow.GetValue(1), 2);
            Assert.AreEqual(cashFlow.GetValue(2), 4);
            Assert.AreEqual(cashFlow.GetValue(3), 2);
            Assert.AreEqual(cashFlow.GetValue(4), 1);
            Assert.AreEqual(cashFlow.GetValue(5), 2);
        }

        [TestMethod]
        public void cashFlowValueWithOnlyOneTradeAndAGapBefore()
        {
            ITimeSeries sampleTimeSeries = new MockTimeSeries(1, 1, 2);
            ITradingRecord tradingRecord = new BaseTradingRecord(Order.buyAt(1, sampleTimeSeries), Order.sellAt(2, sampleTimeSeries));

            CashFlow cashFlow = new CashFlow(sampleTimeSeries, tradingRecord);

            Assert.AreEqual(cashFlow.GetValue(0), 1);
            Assert.AreEqual(cashFlow.GetValue(1), 1);
            Assert.AreEqual(cashFlow.GetValue(2), 2);
        }

        [TestMethod]
        public void cashFlowValueWithOnlyOneTradeAndAGapAfter()
        {
            ITimeSeries sampleTimeSeries = new MockTimeSeries(1, 2, 2);
            ITradingRecord tradingRecord = new BaseTradingRecord(Order.buyAt(0, sampleTimeSeries), Order.sellAt(1, sampleTimeSeries));

            CashFlow cashFlow = new CashFlow(sampleTimeSeries, tradingRecord);

            Assert.AreEqual(3, cashFlow.GetSize());
            Assert.AreEqual(cashFlow.GetValue(0), 1);
            Assert.AreEqual(cashFlow.GetValue(1), 2);
            Assert.AreEqual(cashFlow.GetValue(2), 2);
        }

        [TestMethod]
        public void cashFlowValueWithTwoTradesAndLongTimeWithoutOrders()
        {
            ITimeSeries sampleTimeSeries = new MockTimeSeries(1, 2, 4, 8, 16, 32);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(1, sampleTimeSeries), Order.sellAt(2, sampleTimeSeries),
                    Order.buyAt(4, sampleTimeSeries), Order.sellAt(5, sampleTimeSeries));

            CashFlow cashFlow = new CashFlow(sampleTimeSeries, tradingRecord);

            Assert.AreEqual(cashFlow.GetValue(0), 1);
            Assert.AreEqual(cashFlow.GetValue(1), 1);
            Assert.AreEqual(cashFlow.GetValue(2), 2);
            Assert.AreEqual(cashFlow.GetValue(3), 2);
            Assert.AreEqual(cashFlow.GetValue(4), 2);
            Assert.AreEqual(cashFlow.GetValue(5), 4);
        }

        [TestMethod]
        public void cashFlowValue()
        {
            // First sample series
            ITimeSeries sampleTimeSeries = new MockTimeSeries(3, 2, 5, 1000, 5000, 0.0001M, 4M, 7M, 6M, 7, 8, 5, 6);
            ITradingRecord tradingRecord = new BaseTradingRecord(
                    Order.buyAt(0, sampleTimeSeries), Order.sellAt(2, sampleTimeSeries),
                    Order.buyAt(6, sampleTimeSeries), Order.sellAt(8, sampleTimeSeries),
                    Order.buyAt(9, sampleTimeSeries), Order.sellAt(11, sampleTimeSeries));

            CashFlow cashFlow = new CashFlow(sampleTimeSeries, tradingRecord);

            Assert.AreEqual(1, cashFlow.GetValue(0));
            Assert.AreEqual(2M / 3, cashFlow.GetValue(1));
            Assert.AreEqual(5M / 3, cashFlow.GetValue(2));
            Assert.AreEqual(5M / 3, cashFlow.GetValue(3));
            Assert.AreEqual(5M / 3, cashFlow.GetValue(4));
            Assert.AreEqual(5M / 3, cashFlow.GetValue(5));
            Assert.AreEqual(5M / 3, cashFlow.GetValue(6));
            Assert.AreEqual(2.9166666666666666666666666667M, cashFlow.GetValue(7)); // 5M / 3M * 7M / 4M
            Assert.AreEqual(5M / 3 * 6M / 4, cashFlow.GetValue(8));
            Assert.AreEqual(5M / 3 * 6M / 4, cashFlow.GetValue(9));
            Assert.AreEqual(2.8571428571428571428571428572M, cashFlow.GetValue(10)); // 5M / 3 * 6M / 4 * 8M / 7
            Assert.AreEqual(1.7857142857142857142857142858M, cashFlow.GetValue(11)); // 5M / 3 * 6M / 4 * 5M / 7
            Assert.AreEqual(1.7857142857142857142857142858M, cashFlow.GetValue(12)); // 5M / 3 * 6M / 4 * 5M / 7

            // Second sample series
            sampleTimeSeries = new MockTimeSeries(5, 6, 3, 7, 8, 6, 10, 15, 6);
            tradingRecord = new BaseTradingRecord(
                    Order.buyAt(4, sampleTimeSeries), Order.sellAt(5, sampleTimeSeries),
                    Order.buyAt(6, sampleTimeSeries), Order.sellAt(8, sampleTimeSeries));

            CashFlow flow = new CashFlow(sampleTimeSeries, tradingRecord);
            Assert.AreEqual(flow.GetValue(0), 1);
            Assert.AreEqual(flow.GetValue(1), 1);
            Assert.AreEqual(flow.GetValue(2), 1);
            Assert.AreEqual(flow.GetValue(3), 1);
            Assert.AreEqual(flow.GetValue(4), 1);
            Assert.AreEqual(flow.GetValue(5), 0.75M);
            Assert.AreEqual(flow.GetValue(6), 0.75M);
            Assert.AreEqual(flow.GetValue(7), 1.125M);
            Assert.AreEqual(flow.GetValue(8), 0.45M);
        }

        [TestMethod]
        public void cashFlowValueWithNoTrades()
        {
            ITimeSeries sampleTimeSeries = new MockTimeSeries(3, 2, 5, 4, 7, 6, 7, 8, 5, 6);
            CashFlow cashFlow = new CashFlow(sampleTimeSeries, new BaseTradingRecord());
            Assert.AreEqual(cashFlow.GetValue(4), 1);
            Assert.AreEqual(cashFlow.GetValue(7), 1);
            Assert.AreEqual(cashFlow.GetValue(9), 1);
        }

        [TestMethod]
        public void reallyLongCashFlow()
        {
            int size = 1000000;
            ITimeSeries sampleTimeSeries = new MockTimeSeries(Enumerable.Range(0, size).Select(_ => new MockBar(10)).Cast<IBar>().ToList());
            ITradingRecord tradingRecord = new BaseTradingRecord(Order.buyAt(0, sampleTimeSeries), Order.sellAt(size - 1, sampleTimeSeries));
            CashFlow cashFlow = new CashFlow(sampleTimeSeries, tradingRecord);
            Assert.AreEqual(cashFlow.GetValue(size - 1), 1);
        }

    }
}
