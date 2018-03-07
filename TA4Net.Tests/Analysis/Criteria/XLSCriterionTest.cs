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
//    public class XLSCriterionTest : ExternalCriterionTest
//    {

//        private Class<?> clazz;
//        private string fileName;
//        private int criterionColumn;
//        private int statesColumn;
//        private TimeSeries cachedSeries = null;

//        /**
//         * Constructor.
//         * 
//         * @param clazz class containing the file resources
//         * @param fileName file name of the file containing the workbook
//         * @param criterionColumn column number containing the Calculated criterion
//         *            values
//         * @param statesColumn column number containing the trading record states
//         */
//        public XLSCriterionTest(Class<?> clazz, string fileName, int criterionColumn, int statesColumn)
//        {
//            this.clazz = clazz;
//            this.fileName = fileName;
//            this.criterionColumn = criterionColumn;
//            this.statesColumn = statesColumn;
//        }

//        /**
//         * Gets the TimeSeries from the XLS file. The TimeSeries is cached so that
//         * subsequent calls do not execute getSeries.
//         * 
//         * @return TimeSeries from the file
//         * @throws Exception if getSeries throws IOException or DataFormatException
//         */
//        public TimeSeries getSeries() { // throws exception
//        if (cachedSeries == null) {
//            cachedSeries = XlsTestsUtils.getSeries(clazz, fileName);
//        }
//        return cachedSeries;
//    }

///**
// * Gets the readonly criterion value from the XLS file given the parameters.
// * 
// * @param params criterion parameters
// * @return decimal readonly criterion value
// * @throws Exception if getreadonlyCriterionValue throws IOException or
// *             DataFormatException
// */
//public decimal getreadonlyCriterionValue(Object... params) { // throws exception
//        return XlsTestsUtils.getreadonlyCriterionValue(clazz, fileName, criterionColumn, params);
//}

///**
// * Gets the trading record from the XLS file.
// * 
// * @return TradingRecord from the file
// */
//public TradingRecord getTradingRecord() { // throws exception
//        return XlsTestsUtils.getTradingRecord(clazz, fileName, statesColumn);
//}

//}
