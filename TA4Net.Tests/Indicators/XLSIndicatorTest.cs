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
using TA4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TA4Net.Interfaces;

namespace TA4Net.Test.Indicators
{
    [TestClass]
    public class XLSIndicatorTest : ExternalIndicatorTest
    {

        private Type type;
        private string fileName;
        private int column;
        private ITimeSeries cachedSeries = null;

        /**
         * Constructor.
         * 
         * @param clazz class containing the file resources
         * @param fileName file name of the file containing the workbook
         * @param column column number containing the Calculated indicator values
         */
        public XLSIndicatorTest(Type type, string fileName, int column)
        {
            this.type = type;
            this.fileName = fileName;
            this.column = column;
        }

        /**
         * Gets the TimeSeries from the XLS file.
         * 
         * @return TimeSeries from the file
         * @throws Exception if getSeries throws IOException or DataFormatException
         */
        public ITimeSeries getSeries()
        {
            if (cachedSeries == null)
            {
                cachedSeries = XlsTestsUtils.GetSeries(type, fileName);
            }
            return cachedSeries;
        }

        /**
         * Gets the Indicator from the XLS file given the parameters.
         * 
         * @param params indicator parameters
         * @return Indicator from the file given the parameters
         * @throws Exception if getIndicator throws IOException or
         *             DataFormatException
         */
        public IIndicator<decimal> getIndicator(params object[] values)
        {
            return XlsTestsUtils.GetIndicator(type, fileName, column, values);
        }
    }
}
