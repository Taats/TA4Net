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
using System;
using TA4Net.Interfaces;
using TA4Net.Trading.Rules.Types;

namespace TA4Net
{
    /**
     * A manager for {@link TimeSeries} objects.
     * <p></p>
     * Used for backtesting.
     * Allows to run a {@link Strategy trading strategy} over the managed time series.
     */
    public class TimeSeriesManager
    {

        /** The logger */
        // private readonly Logger log = LoggerFactory.getLogger(getClass());

        /** The managed time series */
        private ITimeSeries _timeSeries;

        /**
         * Constructor.
         */
        public TimeSeriesManager()
        {
        }

        /**
         * Constructor.
         * @param timeSeries the time series to be managed
         */
        public TimeSeriesManager(ITimeSeries timeSeries)
        {
            _timeSeries = timeSeries;
        }

        /**
         * @param timeSeries the time series to be managed
         */
        public void SetTimeSeries(ITimeSeries timeSeries)
        {
            _timeSeries = timeSeries;
        }

        /**
         * @return the managed time series
         */
        public ITimeSeries TimeSeries
        {
            get { return _timeSeries; }
        }

        /**
         * Runs the provided strategy over the managed series.
         * <p>
         * Opens the trades with {@link OrderType} BUY @return the trading record coMing from the run
         */
        public ITradingRecord Run(IStrategy strategy)
        {
            return Run(strategy, OrderType.BUY);
        }

        /**
         * Runs the provided strategy over the managed series (from startIndex to finishIndex).
         * <p>
         * Opens the trades with {@link OrderType} BUY orders.
         * @param strategy the trading strategy
         * @param startIndex the start index for the run (included)
         * @param finishIndex the finish index for the run (included)
         * @return the trading record coMing from the run
         */
        public ITradingRecord Run(IStrategy strategy, int startIndex, int finishIndex)
        {
            return Run(strategy, OrderType.BUY, Decimals.NaN, startIndex, finishIndex);
        }

        /**
         * Runs the provided strategy over the managed series.
         * <p>
         * Opens the trades with {@link OrderType} BUY orders.
         * @param strategy the trading strategy
         * @param orderType the {@link OrderType} used to open the trades
         * @return the trading record coMing from the run
         */
        public ITradingRecord Run(IStrategy strategy, OrderType orderType)
        {
            return Run(strategy, orderType, Decimals.NaN);
        }

        /**
         * Runs the provided strategy over the managed series (from startIndex to finishIndex).
         * <p>
         * Opens the trades with {@link OrderType} BUYorders.
         * @param strategy the trading strategy
         * @param orderType the {@link OrderType} used to open the trades
         * @param startIndex the start index for the run (included)
         * @param finishIndex the finish index for the run (included)
         * @return the trading record coMing from the run
         */
        public ITradingRecord Run(IStrategy strategy, OrderType orderType, int startIndex, int finishIndex)
        {
            return Run(strategy, orderType, Decimals.NaN, startIndex, finishIndex);
        }

        /**
         * Runs the provided strategy over the managed series.
         * <p>
         * @param strategy the trading strategy
         * @param orderType the {@link OrderType} used to open the trades
         * @param amount the amount used to open/close the trades
         * @return the trading record coMing from the run
         */
        public ITradingRecord Run(IStrategy strategy, OrderType orderType, decimal amount)
        {
            return Run(strategy, orderType, amount, _timeSeries.GetBeginIndex(), _timeSeries.GetEndIndex());
        }

        /**
         * Runs the provided strategy over the managed series (from startIndex to finishIndex).
         * <p>
         * @param strategy the trading strategy
         * @param orderType the {@link OrderType} used to open the trades
         * @param amount the amount used to open/close the trades
         * @param startIndex the start index for the run (included)
         * @param finishIndex the finish index for the run (included)
         * @return the trading record coMing from the run
         */
        public ITradingRecord Run(IStrategy strategy, OrderType orderType, decimal amount, int startIndex, int finishIndex)
        {

            int runBeginIndex = Math.Max(startIndex, _timeSeries.GetBeginIndex());
            int runEndIndex = Math.Min(finishIndex, _timeSeries.GetEndIndex());

            // log.trace("Running strategy (indexes: {} -> {}): {} (starting with {})", runBeginIndex, runEndIndex, strategy, orderType);
            ITradingRecord tradingRecord = new BaseTradingRecord(orderType);
            for(int i = runBeginIndex; i <= runEndIndex; i++)
            {
                // For each bar between both indexes[] 
                if (strategy.ShouldOperate(i, tradingRecord))
                {
                    tradingRecord.Operate(i, _timeSeries.GetBar(i).ClosePrice, amount);
                }
            }

            if (!tradingRecord.IsClosed())
            {
                // If the last trade is still opened, we search out of the run end index.
                // May works if the end index for this run was inferior to the actual number of bars
                int seriesMaxSize = Math.Max(_timeSeries.GetEndIndex() + 1, _timeSeries.GetBarData().Count);
                for(int i = runEndIndex + 1; i < seriesMaxSize; i++)
                {
                    // For each bar after the end index of this run[] 
                    // --> Trying to close the last trade
                    if (strategy.ShouldOperate(i, tradingRecord))
                    {
                        tradingRecord.Operate(i, _timeSeries.GetBar(i).ClosePrice, amount);
                        break;
                    }
                }
            }
            return tradingRecord;
        }

    }
}
