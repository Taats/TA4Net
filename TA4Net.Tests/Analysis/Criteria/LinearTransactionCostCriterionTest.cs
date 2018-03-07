///*
//  The MIT License (MIT)

//  Copyright (c) 2014-2017 Marc de Verdelhan & respective authors (see AUTHORS)

//  Permission is hereby granteM, free of charge, to any person obtaining a copy of
//  this software and associated documentation files (the "Software"), to deal in
//  the Software without restriction, including without limitation the rights to
//  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//  the Software, and to permit persons to whom the Software is furnished to do so,
//  subject to the following conditions:

//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.

//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KINM, EXPRESS OR
//  IMPLIEM, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
//  FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
//  COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//  IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//  CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// */
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace TA4Net.Test.Analysis.Criteria
//{
//    [TestClass]
//    public class LinearTransactionCostCriterionTest : CriterionTest
//    {

//        private ExternalCriterionTest xls;

//        public LinearTransactionCostCriterionTest()
//            : base(params[0], (double) params[1], (double) params[2]));
//        {
//        xls = new XLSCriterionTest(GetType(), "LTC.xls", 16, 6);
//    }

//    [TestMethod]
//    public void externalData() {
//        TimeSeries xlsSeries = xls.getSeries();
//        TradingRecord xlsTradingRecord = xls.getTradingRecord();
//        double value;

//        value = getCriterion(1000M, 0.005, 0.2).Calculate(xlsSeries, xlsTradingRecord);
//        Assert.AreEqual(xls.getreadonlyCriterionValue(1000M, 0.005, 0.2), value);
//        Assert.AreEqual(843.5492, value);

//        value = getCriterion(1000M, 0.1, 1.0).Calculate(xlsSeries, xlsTradingRecord);
//        Assert.AreEqual(xls.getreadonlyCriterionValue(1000M, 0.1, 1.0), value);
//        Assert.AreEqual(1122.4410, value);
//    }

//    [TestMethod]
//    public void dummyData()
//    {
//        MockTimeSeries series = new MockTimeSeries(100, 150, 200, 100, 50, 100);
//        TradingRecord tradingRecord = new BaseTradingRecord();
//        double criterion;

//        tradingRecord.operate(0); tradingRecord.operate(1);
//        criterion = getCriterion(1000M, 0.005, 0.2).Calculate(series, tradingRecord);
//        Assert.AreEqual(12.861, criterion);

//        tradingRecord.operate(2); tradingRecord.operate(3);
//        criterion = getCriterion(1000M, 0.005, 0.2).Calculate(series, tradingRecord);
//        Assert.AreEqual(24.3759, criterion);

//        tradingRecord.operate(5);
//        criterion = getCriterion(1000M, 0.005, 0.2).Calculate(series, tradingRecord);
//        Assert.AreEqual(28.2488, criterion);
//    }

//    [TestMethod]
//    public void fixedCost()
//    {
//        MockTimeSeries series = new MockTimeSeries(100, 105, 110, 100, 95, 105);
//        TradingRecord tradingRecord = new BaseTradingRecord();
//        double criterion;

//        tradingRecord.operate(0); tradingRecord.operate(1);
//        criterion = getCriterion(1000M, 0M, 1.3d).Calculate(series, tradingRecord);
//        Assert.AreEqual(2.6M, criterion);

//        tradingRecord.operate(2); tradingRecord.operate(3);
//        criterion = getCriterion(1000M, 0M, 1.3d).Calculate(series, tradingRecord);
//        Assert.AreEqual(5.2M, criterion);

//        tradingRecord.operate(0);
//        criterion = getCriterion(1000M, 0M, 1.3d).Calculate(series, tradingRecord);
//        Assert.AreEqual(6.5M, criterion);
//    }

//    [TestMethod]
//    public void fixedCostWithOneTrade()
//    {
//        MockTimeSeries series = new MockTimeSeries(100, 95, 100, 80, 85, 70);
//        Trade trade = new Trade();
//        double criterion;

//        criterion = getCriterion(1000M, 0M, 0.75d).Calculate(series, trade);
//        Assert.AreEqual(0M, criterion);

//        trade.operate(1);
//        criterion = getCriterion(1000M, 0M, 0.75d).Calculate(series, trade);
//        Assert.AreEqual(0.75M, criterion);

//        trade.operate(3);
//        criterion = getCriterion(1000M, 0M, 0.75d).Calculate(series, trade);
//        Assert.AreEqual(1.5M, criterion);

//        trade.operate(4);
//        criterion = getCriterion(1000M, 0M, 0.75d).Calculate(series, trade);
//        Assert.AreEqual(1.5M, criterion);
//    }

//    [TestMethod]
//    public void betterThan()
//    {
//        AbstractAnalysisCriterion criterion = new LinearTransactionCostCriterion(1000, 0.5);
//        Assert.IsTrue(criterion.betterThan(3.1, 4.2));
//        Assert.IsFalse(criterion.betterThan(2.1, 1.9));
//    }
//}
