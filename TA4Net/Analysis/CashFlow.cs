/*
  The MIT License (MIT)

  Copyright (c) 2014-2017 Marc de Verdelhan & respective authors (see AUTHORS)

  Permission is hereby granted, free of charge, to any person obtaining a copy of
  this software and associated documentation files (the "Software"), to deal in
  the Software without restriction, including without limitation the rights to
  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
  the Software, and to permit persons to whom the Software is furnished to do so,
  subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
  FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
  COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
  IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
  CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using TA4Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TA4Net.Interfaces;

namespace TA4Net.Analysis
{

    /**
     * The cash flow.
     * <p></p>
     * this class allows to follow the money cash flow involved by a list of trades over a time series.
     */
    public class CashFlow : IIndicator<decimal>
    {

        /** The time series */
        private readonly ITimeSeries _timeSeries;

        /** The cash flow values */
        private List<decimal> _values = new List<decimal> { Decimals.ONE };

        /**
         * Constructor.
         * @param timeSeries the time series
         * @param trade a single trade
         */
        public CashFlow(ITimeSeries timeSeries, Trade trade)
        {
            _timeSeries = timeSeries;
            Calculate(trade);
            FillToTheEnd();
        }

        /**
         * Constructor.
         * @param timeSeries the time series
         * @param tradingRecord the trading record
         */
        public CashFlow(ITimeSeries timeSeries, ITradingRecord tradingRecord)
        {
            _timeSeries = timeSeries;
            Calculate(tradingRecord);
            FillToTheEnd();
        }

        /**
         * @param index the bar index
         * @return the cash flow value at the index-th position
         */

        public decimal GetValue(int index)
        {
            return _values[index];
        }


        public ITimeSeries TimeSeries
        {
            get { return _timeSeries; }
        }

        /**
         * @return the size of the time series
         */
        public int GetSize()
        {
            return _timeSeries.GetBarCount();
        }

        /**
         * Calculates the cash flow for a single trade.
         * @param trade a single trade
         */
        private void Calculate(Trade trade)
        {
            int entryIndex = trade.GetEntry().getIndex();
            int begin = entryIndex + 1;
            if (begin > _values.Count)
            {
                decimal lastValue = _values[_values.Count - 1];
                //  _values.AddRange(Collections.nCopies(begin - _values.Count, lastValue));
                _values.AddRange(Enumerable.Range(0, begin - _values.Count).Select(_ => lastValue));
            }
            int end = trade.GetExit().getIndex();
            for (int i = Math.Max(begin, 1); i <= end; i++)
            {
                decimal ratio;
                if (trade.GetEntry().isBuy())
                {
                    ratio = _timeSeries.GetBar(i).ClosePrice.DividedBy(_timeSeries.GetBar(entryIndex).ClosePrice);
                }
                else
                {
                    ratio = _timeSeries.GetBar(entryIndex).ClosePrice.DividedBy(_timeSeries.GetBar(i).ClosePrice);
                }
                _values.Add(_values[entryIndex].MultipliedBy(ratio));
            }
        }

        /**
         * Calculates the cash flow for a trading record.
         * @param tradingRecord the trading record
         */
        private void Calculate(ITradingRecord tradingRecord)
        {
            foreach (Trade trade in tradingRecord.Trades)
            {
                // For each trade[] 
                Calculate(trade);
            }
        }

        /**
         * Fills with last value till the end of the series.
         */
        private void FillToTheEnd()
        {
            if (_timeSeries.GetEndIndex() >= _values.Count)
            {
                decimal lastValue = _values[_values.Count - 1];
                _values.AddRange(Enumerable.Range(0, _timeSeries.GetEndIndex() - _values.Count + 1).Select(_ => lastValue));
            }
        }

        public string GetConfiguration()
        {
            return $"{GetType()}";
        }
    }
}