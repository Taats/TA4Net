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
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Statistics
{
    /**
     * Periodical Growth Rate indicator.
     *
     * In general the 'Growth Rate' is useful for comparing the average returns of
     * investments in stocks or funds and can be used to compare the performance
     * e.g. comparing the historical returns of stocks with bonds.
     *
     * this indicator has the following characteristics:
     *  - the calculation is timeframe dependendant. The timeframe corresponds to the
     *    number of trading events in a period, e. g. the timeframe for a US trading
     *    year for end of day bars would be '251' trading days
     *  - the result is a step function with a constant value within a timeframe
     *  - NaN values while index is smaller than timeframe, e.g. timeframe is year,
     *    than no values are Calculated before a full year is reached
     *  - NaN values for incomplete timeframes, e.g. timeframe is a year and your
     *    timeseries contains data for 11,3 years, than no values are Calculated for
     *    the remaining 0,3 years
     *  - the method 'getTotalReturn' Calculates the total return over all returns
     *    of the coresponding timeframes
     *
     *
     * Further readings:
     * Good sumary on 'Rate of Return': https://en.wikipedia.org/wiki/Rate_of_return
     * Annual return / CAGR: http://www.investopedia.com/terms/a/annual-return.asp
     * Annualized Total Return: http://www.investopedia.com/terms/a/annualized-total-return.asp
     * Annualized Return vs. Cumulative Return:
     * http://www.fool.com/knowledge-center/2015/11/03/annualized-return-vs-cumulative-return.aspx
     *
     */
    public class PeriodicalGrowthRateIndicator : CachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _indicator;
        private readonly int _timeFrame;

        /**
         * Constructor.
         * Example: use timeFrame = 251 and "end of day"-bars for annual behaviour
         * in the US (http://tradingsim.com/blog/trading-days-in-a-year/).
         * @param indicator the indicator
         * @param timeFrame the time frame
         */
        public PeriodicalGrowthRateIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator)
        {
            _indicator = indicator;
            _timeFrame = timeFrame;
        }

        /**
         * Gets the TotalReturn from the Calculated results of the method 'Calculate'.
         * For a timeFrame = number of trading days within a year (e. g. 251 days in the US)
         * and "end of day"-bars you will get the 'Annualized Total Return'.
         * Only complete timeFrames are taken into the calculation.
         * @return the total return from the Calculated results of the method 'Calculate'
         */
        public decimal getTotalReturn() // was double
        {

            decimal totalProduct = Decimals.ONE;
            int completeTimeframes = (TimeSeries.GetBarCount() / _timeFrame);

            for (int i = 1; i <= completeTimeframes; i++) {
                int index = i * _timeFrame;
                decimal currentReturn = GetValue(index);

                // Skip NaN at the end of a series
                if (currentReturn != Decimals.NaN)
                {
                    currentReturn = currentReturn.Plus(Decimals.ONE);
                    totalProduct = totalProduct.MultipliedBy(currentReturn);
                }
            }

            return totalProduct.Pow(1.0M / completeTimeframes);
        }


        protected override decimal Calculate(int index)
        {

            decimal currentValue = _indicator.GetValue(index);

            int helpPartialTimeframe = index % _timeFrame;
            decimal helpFullTimeframes =  ((decimal)_indicator.TimeSeries.GetBarCount() / _timeFrame).Floor();
            decimal helpIndexTimeframes = (decimal)index / _timeFrame;

            decimal helpPartialTimeframeHeld = (decimal)helpPartialTimeframe / _timeFrame;
            decimal partialTimeframeHeld = (helpPartialTimeframeHeld == 0) ? 1.0M : helpPartialTimeframeHeld;

            // Avoid calculations of returns:
            // a.) if index number is below timeframe
            // e.g. timeframe = 365, index = 5 => no calculation
            // b.) if at the end of a series incomplete timeframes would remain
            decimal timeframedReturn = Decimals.NaN;
            if ((index >= _timeFrame) /*(a)*/ && (helpIndexTimeframes < helpFullTimeframes) /*(b)*/)
            {
                decimal movingValue = _indicator.GetValue(index - _timeFrame);
                decimal movingSimpleReturn = (currentValue.Minus(movingValue)).DividedBy(movingValue);

                decimal timeframedReturn_double = (1 + movingSimpleReturn).Pow(1M / partialTimeframeHeld) - 1;
                timeframedReturn = timeframedReturn_double;
            }

            return timeframedReturn;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, Indicator: {_indicator.GetConfiguration()}";
        }
    }
}

