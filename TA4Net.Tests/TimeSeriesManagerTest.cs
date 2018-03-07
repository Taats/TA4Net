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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TA4Net.Mocks;
using System;
using System.Linq;
using TA4Net;
using TA4Net.Interfaces;
using TA4Net.Trading.Rules;
using TA4Net.Trading.Rules.Types;

namespace TA4Net.Test
{
    [TestClass]
    public class TimeSeriesManagerTest
    {

        private ITimeSeries seriesForRun;

        private TimeSeriesManager manager;

        private IStrategy strategy;

        [TestInitialize]
        public void setUp()
        {

            seriesForRun = new MockTimeSeries(
                    new decimal[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                    new DateTime[] {
                    DateTime.Parse("2013-01-01T00:00:00-05:00"),
                    DateTime.Parse("2013-08-01T00:00:00-05:00"),
                    DateTime.Parse("2013-10-01T00:00:00-05:00"),
                    DateTime.Parse("2013-12-01T00:00:00-05:00"),
                    DateTime.Parse("2014-02-01T00:00:00-05:00"),
                    DateTime.Parse("2015-01-01T00:00:00-05:00"),
                    DateTime.Parse("2015-08-01T00:00:00-05:00"),
                    DateTime.Parse("2015-10-01T00:00:00-05:00"),
                    DateTime.Parse("2015-12-01T00:00:00-05:00")
                    });
            manager = new TimeSeriesManager(seriesForRun);

            strategy = new BaseStrategy(new FixedRule(0, 2, 3, 6), new FixedRule(1, 4, 7, 8));
            strategy.SetUnstablePeriod(2); // Strategy would need a real test class
        }

        [TestMethod]
        public void runOnWholeSeries()
        {
            ITimeSeries series = new MockTimeSeries(20, 40, 60, 10, 30, 50, 0, 20, 40);
            manager.SetTimeSeries(series);
            var allTrades = manager.Run(strategy).Trades;
            Assert.AreEqual(2, allTrades.Count());
        }

        [TestMethod]
        public void runOnWholeSeriesWithAmount()
        {
            ITimeSeries series = new MockTimeSeries(20, 40, 60, 10, 30, 50, 0, 20, 40);
            manager.SetTimeSeries(series);
            var allTrades = manager.Run(strategy, OrderType.BUY, Decimals.HUNDRED).Trades.ToArray();

            Assert.AreEqual(2, allTrades.Count());
            Assert.AreEqual(Decimals.HUNDRED, allTrades[0].GetEntry().Amount);
            Assert.AreEqual(Decimals.HUNDRED, allTrades[1].GetEntry().Amount);

        }

        [TestMethod]
        public void runOnSeries()
        {
            var trades = manager.Run(strategy).Trades;
            Assert.AreEqual(2, trades.Count);

            Assert.AreEqual(Order.buyAt(2, seriesForRun.GetBar(2).ClosePrice, Decimals.NaN), trades[0].GetEntry());
            Assert.AreEqual(Order.sellAt(4, seriesForRun.GetBar(4).ClosePrice, Decimals.NaN), trades[0].GetExit());

            Assert.AreEqual(Order.buyAt(6, seriesForRun.GetBar(6).ClosePrice, Decimals.NaN), trades[1].GetEntry());
            Assert.AreEqual(Order.sellAt(7, seriesForRun.GetBar(7).ClosePrice, Decimals.NaN), trades[1].GetExit());
        }

        [TestMethod]
        public void runWithOpenEntryBuyLeft()
        {
            IStrategy aStrategy = new BaseStrategy(new FixedRule(1), new FixedRule(3));
            var trades = manager.Run(aStrategy, 0, 3).Trades;
            Assert.AreEqual(1, trades.Count);

            Assert.AreEqual(Order.buyAt(1, seriesForRun.GetBar(1).ClosePrice, Decimals.NaN), trades[0].GetEntry());
            Assert.AreEqual(Order.sellAt(3, seriesForRun.GetBar(3).ClosePrice, Decimals.NaN), trades[0].GetExit());
        }

        [TestMethod]
        public void runWithOpenEntrySellLeft()
        {
            IStrategy aStrategy = new BaseStrategy(new FixedRule(1), new FixedRule(3));
            var trades = manager.Run(aStrategy, OrderType.SELL, 0, 3).Trades;
            Assert.AreEqual(1, trades.Count);

            Assert.AreEqual(Order.sellAt(1, seriesForRun.GetBar(1).ClosePrice, Decimals.NaN), trades[0].GetEntry());
            Assert.AreEqual(Order.buyAt(3, seriesForRun.GetBar(3).ClosePrice, Decimals.NaN), trades[0].GetExit());
        }

        [TestMethod]
        public void runBetweenIndexes()
        {

            var trades = manager.Run(strategy, 0, 3).Trades;
            Assert.AreEqual(1, trades.Count);
            Assert.AreEqual(Order.buyAt(2, seriesForRun.GetBar(2).ClosePrice, Decimals.NaN), trades[0].GetEntry());
            Assert.AreEqual(Order.sellAt(4, seriesForRun.GetBar(4).ClosePrice, Decimals.NaN), trades[0].GetExit());

            trades = manager.Run(strategy, 4, 4).Trades;
            Assert.IsTrue(trades.isEmpty());

            trades = manager.Run(strategy, 5, 8).Trades;
            Assert.AreEqual(1, trades.Count);
            Assert.AreEqual(Order.buyAt(6, seriesForRun.GetBar(6).ClosePrice, Decimals.NaN), trades[0].GetEntry());
            Assert.AreEqual(Order.sellAt(7, seriesForRun.GetBar(7).ClosePrice, Decimals.NaN), trades[0].GetExit());
        }

        [TestMethod]
        public void runOnSeriesSlices()
        {
            ITimeSeries series = new MockTimeSeries(new decimal[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                        new DateTime[]{
                            new DateTime(2000, 1, 1, 0, 0, 0, 0),
                            new DateTime(2000, 1, 1, 0, 0, 0, 0),
                            new DateTime(2001, 1, 1, 0, 0, 0, 0),
                            new DateTime(2001, 1, 1, 0, 0, 0, 0),
                            new DateTime(2002, 1, 1, 0, 0, 0, 0),
                            new DateTime(2002, 1, 1, 0, 0, 0, 0),
                            new DateTime(2002, 1, 1, 0, 0, 0, 0),
                            new DateTime(2003, 1, 1, 0, 0, 0, 0),
                            new DateTime(2004, 1, 1, 0, 0, 0, 0),
                            new DateTime(2005, 1, 1, 0, 0, 0, 0)
                        });
            manager.SetTimeSeries(series);

            IStrategy aStrategy = new BaseStrategy(new FixedRule(0, 3, 5, 7), new FixedRule(2, 4, 6, 9));

            var trades = manager.Run(aStrategy, 0, 1).Trades;
            Assert.AreEqual(1, trades.Count);
            Assert.AreEqual(Order.buyAt(0, series.GetBar(0).ClosePrice, Decimals.NaN), trades[0].GetEntry());
            Assert.AreEqual(Order.sellAt(2, series.GetBar(2).ClosePrice, Decimals.NaN), trades[0].GetExit());

            trades = manager.Run(aStrategy, 2, 3).Trades;
            Assert.AreEqual(1, trades.Count);
            Assert.AreEqual(Order.buyAt(3, series.GetBar(3).ClosePrice, Decimals.NaN), trades[0].GetEntry());
            Assert.AreEqual(Order.sellAt(4, series.GetBar(4).ClosePrice, Decimals.NaN), trades[0].GetExit());

            trades = manager.Run(aStrategy, 4, 6).Trades;
            Assert.AreEqual(1, trades.Count);
            Assert.AreEqual(Order.buyAt(5, series.GetBar(5).ClosePrice, Decimals.NaN), trades[0].GetEntry());
            Assert.AreEqual(Order.sellAt(6, series.GetBar(6).ClosePrice, Decimals.NaN), trades[0].GetExit());

            trades = manager.Run(aStrategy, 7, 7).Trades;
            Assert.AreEqual(1, trades.Count);
            Assert.AreEqual(Order.buyAt(7, series.GetBar(7).ClosePrice, Decimals.NaN), trades[0].GetEntry());
            Assert.AreEqual(Order.sellAt(9, series.GetBar(9).ClosePrice, Decimals.NaN), trades[0].GetExit());

            trades = manager.Run(aStrategy, 8, 8).Trades;
            Assert.IsTrue(trades.isEmpty());

            trades = manager.Run(aStrategy, 9, 9).Trades;
            Assert.IsTrue(trades.isEmpty());
        }
    }
}
