/*
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TA4Net.Interfaces;
using TA4Net.Mocks;
using TA4Net.Tests.Extensions;

namespace TA4Net.Test
{
    [TestClass]
    public class XlsTestsUtils
    {

        /**
         * Returns the first Sheet (mutable) from a workbook with the file name in
         * the test class's resources.
         * 
         * @param clazz class containing the file resources
         * @param fileName file name of the file containing the workbook
         * @return Sheet number zero from the workbook (mutable)
         * @throws IOException if the workbook constructor or close throws
         *             IOException
         */
        private static ISheet GetSheet(Type clazz, string fileName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(File.OpenRead(fileName));
            ISheet sheet = workbook[0];
            workbook.Close();
            return sheet;
        }

        /**
         * Writes the parameters into the second column of the parameters section of
         * a mutable sheet. The parameters section starts after the parameters
         * section header. There must be at least params.Count rows between the
         * parameters section header and the data section header or part of the data
         * section will be overwritten.
         * 
         * @param sheet mutable Sheet
         * @param params parameters to write
         * @throws DataFormatException if the parameters section header is not found
         */
        private static void SetParams(ISheet sheet, params decimal[] values)
        {
            IFormulaEvaluator evaluator = sheet.Workbook.GetCreationHelper().CreateFormulaEvaluator();
            var iterator = sheet.GetRowEnumerator();
            while (iterator.MoveNext())
            {
                IRow row = iterator.Current as IRow;
                // skip rows with an empty first cell
                if (row.GetCell(0) == null)
                {
                    continue;
                }
                // parameters section header is first row with "Param" in first cell
                if (evaluator.Evaluate(row.GetCell(0)).FormatAsString().Contains("Param"))
                {
                    // stream parameters into the second column of subsequent rows
                    // overwrites data section if there is not a large enough gap
                    foreach (var val in values)
                    {
                        iterator.MoveNext();
                        (iterator.Current as IRow).GetCell(1).SetCellValue((double)val);
                    }

                    return;
                }
            }
            // the parameters section header was not found
            throw new FormatException("\"Param\" header row not found");
        }

        /**
         * Gets the TimeSeries from a file.
         * 
         * @param clazz class containing the file resources
         * @param fileName file name of the file resource
         * @return TimeSeries of the data
         * @throws IOException if getSheet throws IOException
         * @throws DataFormatException if getSeries throws DataFormatException
         */
        public static ITimeSeries GetSeries(Type clazz, string fileName)
        {
            ISheet sheet = GetSheet(clazz, fileName);
            return GetSeries(sheet);
        }

        /**
         * Gets a TimeSeries from the data section of a mutable Sheet. Data follows
         * a data section header and appears in the first six columns to the end of
         * the file. Empty cells in the data are forbidden.
         * 
         * @param sheet mutable Sheet
         * @return TimeSeries of the data
         * @throws DataFormatException if getData throws DataFormatException or if
         *             the data contains empty cells
         */
        private static ITimeSeries GetSeries(ISheet sheet)
        {
            ITimeSeries series = new BaseTimeSeries();
            IFormulaEvaluator evaluator = sheet.Workbook.GetCreationHelper().CreateFormulaEvaluator();
            TimeSpan weekDuration = new TimeSpan(7, 0, 0, 0);
            List<IRow> rows = GetData(sheet);
            // parse the rows from the data section
            foreach (IRow row in rows)
            {
                CellValue[] cellValues = new CellValue[6];
                for (int i = 0; i < 6; i++)
                {
                    // empty cells in the data section are forbidden
                    if (row.GetCell(i) == null)
                    {
                        throw new FormatException("empty cell in xls time series data");
                    }
                    cellValues[i] = evaluator.Evaluate(row.GetCell(i));
                }
                // build a bar from the row and add it to the series
                DateTime weekEndDate = DateUtil.GetJavaDate(cellValues[0].NumberValue);
                IBar bar = new BaseBar(weekDuration, weekEndDate,
                        // open, high, low, close, volume
                       cellValues[1].FormatAsString().ToDecimal(),
                       cellValues[2].FormatAsString().ToDecimal(),
                       cellValues[3].FormatAsString().ToDecimal(),
                       cellValues[4].FormatAsString().ToDecimal(),
                       cellValues[5].FormatAsString().ToDecimal());
                series.AddBar(bar);
            }
            return series;
        }

        /**
         * Converts Object parameters into decimal parameters and calls getValues on
         * a column of a mutable sheet.
         * 
         * @param sheet mutable Sheet
         * @param column column number of the values to get
         * @param params Object parameters to convert to decimal
         * @return List<decimal> of values from the column
         * @throws DataFormatException if getValues returns DataFormatException
         */
        private static List<decimal> GetValues(ISheet sheet, int column, params object[] values)
        {
            var decimalParams = values.Select(_ => _.ToString().ToDecimal()).ToArray();
            return GetValues(sheet, column, decimalParams);
        }

        /**
         * Writes the parameters to a mutable Sheet then gets the values from the
         * column.
         * 
         * @param sheet mutable Sheet
         * @param column column number of the values to get
         * @param params decimal parameters to write to the Sheet
         * @return List<decimal> of values from the column after the parameters have
         *         been written
         * @throws DataFormatException if setParams or getValues throws
         *             DataFormatException
         */
        private static List<decimal> GetValues(ISheet sheet, int column, params decimal[] values)
        {
            SetParams(sheet, values);
            return GetValues(sheet, column);
        }

        /**
         * Gets the values in a column of the data section of a sheet. Rows with an
         * empty first cell are ignored.
         * 
         * @param sheet mutable Sheet
         * @param column column number of the values to get
         * @return List<decimal> of values from the column
         * @throws DataFormatException if getData throws DataFormatException
         */
        private static List<decimal> GetValues(ISheet sheet, int column)
        {
            List<decimal> values = new List<decimal>();
            IFormulaEvaluator evaluator = sheet.Workbook.GetCreationHelper().CreateFormulaEvaluator();
            // get all of the data from the data section of the sheet
            List<IRow> rows = GetData(sheet);
            foreach (IRow row in rows)
            {
                // skip rows where the first cell is empty
                if (row.GetCell(column) == null)
                {
                    continue;
                }
                string s = evaluator.Evaluate(row.GetCell(column)).FormatAsString();
                values.Add(decimal.Parse(s));
            }
            return values;
        }

        /**
         * Gets all data rows in the data section, following the data section header
         * to the end of the sheet. Skips rows that start with "//" as data
         * comments.
         * 
         * @param sheet mutable Sheet
         * @return List<Row> of the data rows
         * @throws DataFormatException if the data section header is not found.
         */
        private static List<IRow> GetData(ISheet sheet)
        {
            IFormulaEvaluator evaluator = sheet.Workbook.GetCreationHelper().CreateFormulaEvaluator();
            var iterator = sheet.GetRowEnumerator();
            bool noHeader = true;
            List<IRow> rows = new List<IRow>();
            // iterate through all rows of the sheet
            while (iterator.MoveNext())
            {
                IRow row = iterator.Current as IRow;
                // skip rows with an empty first cell
                if (row.GetCell(0) == null)
                {
                    continue;
                }
                // after the data section header is founM, add all rows that don't
                // have "//" in the first cell
                if (!noHeader)
                {
                    if (evaluator.Evaluate(row.GetCell(0)).FormatAsString().CompareTo("\"//\"") != 0)
                    {
                        rows.Add(row);
                    }
                }
                // if the data section header is not found and this row has "Date"
                // in its first cell, then mark the header as found
                if (noHeader && evaluator.Evaluate(row.GetCell(0)).FormatAsString().Contains("Date"))
                {
                    noHeader = false;
                }
            }
            // if the header was not found throw an exception
            if (noHeader)
            {
                throw new FormatException("\"Date\" header row not found");
            }
            return rows;
        }

        /**
         * Gets an Indicator from a column of an XLS file parameters.
         * 
         * @param clazz class containing the file resource
         * @param fileName file name of the file resource
         * @param column column number of the indicator values
         * @param params indicator parameters
         * @return Indicator<decimal> as Calculated by the XLS file given the
         *         parameters
         * @throws IOException if getSheet throws IOException
         * @throws DataFormatException if getSeries or getValues throws
         *             DataFormatException
         */
        public static IIndicator<decimal> GetIndicator(Type clazz, string fileName, int column, params object[] values)
        {
            ISheet sheet = GetSheet(clazz, fileName);
            return new MockIndicator(GetSeries(sheet), GetValues(sheet, column, values));
        }

        /**
         * Gets the readonly criterion value from a column of an XLS file given
         * parameters.
         * 
         * @param clazz test class containing the file resources
         * @param fileName file name of the file resource
         * @param column column number of the Calculated criterion values
         * @param params criterion parameters
         * @return decimal readonly criterion value as Calculated by the XLS file given
         *         the parameters
         * @throws IOException if getSheet throws IOException
         * @throws DataFormatException if getValues throws DataFormatException
         */
        public static decimal GetreadonlyCriterionValue(Type clazz, string fileName, int column, params object[] objects)
        {
            ISheet sheet = GetSheet(clazz, fileName);
            List<decimal> values = GetValues(sheet, column, objects);
            return values[values.Count - 1];
        }

        /**
         * Gets the trading record from an XLS file.
         * 
         * @param clazz the test class containing the file resources
         * @param fileName file name of the file resource
         * @param column column number of the trading record
         * @return TradingRecord from the file
         * @throws IOException if getSheet throws IOException
         * @throws DataFormatException if getValues throws DataFormatException
         */
        public static ITradingRecord GetTradingRecord(Type clazz, string fileName, int column)
        {
            ISheet sheet = GetSheet(clazz, fileName);
            return new MockTradingRecord(GetValues(sheet, column));
        }

    }
}