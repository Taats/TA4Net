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
//namespace TA4Net.Test.Indicators.statistics
//{
//    using TA4Net;
//    using TA4Net.Indicators.Helpers;
//    using TA4Net.Indicators.Statistics;
//    using Microsoft.VisualStudio.TestTools.UnitTesting;
//    using TA4Net.Mocks;
//    using TA4Net.Extensions;

//    [TestClass] public class SimpleLinearRegressionIndicatorTest {

//    private Indicator<decimal> closePrice;
    
//    [TestInitialize]
//    public void setUp() {
//        decimal[] data = new decimal[]{10, 20, 30, 40, 30, 40, 30, 20, 30, 50, 60, 70, 80};
//        closePrice = new ClosePriceIndicator(new MockTimeSeries(data));
//    }

//    [TestMethod]
//    public void notComputedLinearRegression() {

//        SimpleLinearRegressionIndicator linearReg = new SimpleLinearRegressionIndicator(closePrice, 0);
//        Assert.IsTrue(linearReg.getValue(0).IsNaN());
//        Assert.IsTrue(linearReg.getValue(1).IsNaN());
//        Assert.IsTrue(linearReg.getValue(2).IsNaN());

//        linearReg = new SimpleLinearRegressionIndicator(closePrice, 1);
//        Assert.IsTrue(linearReg.getValue(0).IsNaN());
//        Assert.IsTrue(linearReg.getValue(1).IsNaN());
//        Assert.IsTrue(linearReg.getValue(2).IsNaN());
//    }

//    [TestMethod]
//    public void CalculateLinearRegressionWithLessThan2ObservationsReturnsNaN() {
//        SimpleLinearRegressionIndicator reg = new SimpleLinearRegressionIndicator(closePrice, 0);
//        Assert.IsTrue(reg.getValue(0).IsNaN());
//        Assert.IsTrue(reg.getValue(3).IsNaN());
//        Assert.IsTrue(reg.getValue(6).IsNaN());
//        Assert.IsTrue(reg.getValue(9).IsNaN());
//        reg = new SimpleLinearRegressionIndicator(closePrice, 1);
//        Assert.IsTrue(reg.getValue(0).IsNaN());
//        Assert.IsTrue(reg.getValue(3).IsNaN());
//        Assert.IsTrue(reg.getValue(6).IsNaN());
//        Assert.IsTrue(reg.getValue(9).IsNaN());
//    }

//    [TestMethod]
//    public void CalculateLinearRegressionOn4Observations() {

//        SimpleLinearRegressionIndicator reg = new SimpleLinearRegressionIndicator(closePrice, 4);
//        Assert.AreEqual(reg.getValue(1), 20);
//        Assert.AreEqual(reg.getValue(2), 30);
        
//        SimpleRegression origReg = buildSimpleRegression(10, 20, 30, 40);
//        Assert.AreEqual(reg.getValue(3), 40);
//        Assert.AreEqual(reg.getValue(3), origReg.predict(3));
        
//        origReg = buildSimpleRegression(30, 40, 30, 40);
//        Assert.AreEqual(reg.getValue(5), origReg.predict(3));
        
//        origReg = buildSimpleRegression(30, 20, 30, 50);
//        Assert.AreEqual(reg.getValue(9), origReg.predict(3));
//    }
    
//    [TestMethod]
//    public void CalculateLinearRegression() {
//        double[] values = new double[] { 1, 2, 1.3, 3.75, 2.25 };
//        ClosePriceIndicator indicator = new ClosePriceIndicator(new MockTimeSeries(values));
//        SimpleLinearRegressionIndicator reg = new SimpleLinearRegressionIndicator(indicator, 5);
        
//        SimpleRegression origReg = buildSimpleRegression(values);
//        Assert.AreEqual(reg.getValue(4), origReg.predict(4));
//    }
    
//    /**
//     * @param values values
//     * @return a simple linear regression based on provided values
//     */
//    private static SimpleRegression buildSimpleRegression(params double[] values) {
//        SimpleRegression simpleReg = new SimpleRegression();
//        for (int i = 0; i < values.Length; i++) {
//            simpleReg.addData(i, values[i]);
//        }
//        return simpleReg;
//    }
//}
