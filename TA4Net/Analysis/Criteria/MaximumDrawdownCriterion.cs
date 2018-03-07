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
using TA4Net.Interfaces;

namespace TA4Net.Analysis.Criteria
{
    /**
     * Maximum drawdown criterion.
     * <p></p>
     * @see <a href="http://en.wikipedia.org/wiki/Drawdown_%28economics%29">http://en.wikipedia.org/wiki/Drawdown_%28economics%29</a>
     */
    public class MaximumDrawdownCriterion : AbstractAnalysisCriterion
    {


        public override decimal Calculate(ITimeSeries series, ITradingRecord tradingRecord)
        {
            CashFlow cashFlow = new CashFlow(series, tradingRecord);
            decimal MaximumDrawdown = CalculateMaximumDrawdown(series, cashFlow);
            return MaximumDrawdown;
        }


        public override decimal Calculate(ITimeSeries series, Trade trade)
        {
            if (trade != null && trade.GetEntry() != null && trade.GetExit() != null)
            {
                CashFlow cashFlow = new CashFlow(series, trade);
                decimal MaximumDrawdown = CalculateMaximumDrawdown(series, cashFlow);
                return MaximumDrawdown;
            }
            return 0;
        }


        public override bool BetterThan(decimal criterionValue1, decimal criterionValue2)
        {
            return criterionValue1 < criterionValue2;
        }

        /**
         * Calculates the Maximum drawdown from a cash flow over a series.
         * @param series the time series
         * @param cashFlow the cash flow
         * @return the Maximum drawdown from a cash flow over a series
         */
        private decimal CalculateMaximumDrawdown(ITimeSeries series, CashFlow cashFlow)
        {
            decimal MaximumDrawdown = Decimals.Zero;
            decimal MaxPeak = Decimals.Zero;
            if (!series.IsEmpty())
            {
                // The series is not empty
                for (int i = series.GetBeginIndex(); i <= series.GetEndIndex(); i++)
                {
                    decimal value = cashFlow.GetValue(i);
                    if (value.IsGreaterThan(MaxPeak))
                    {
                        MaxPeak = value;
                    }

                    decimal drawdown = MaxPeak.Minus(value).DividedBy(MaxPeak);
                    if (drawdown.IsGreaterThan(MaximumDrawdown))
                    {
                        MaximumDrawdown = drawdown;
                    }
                }
            }
            return MaximumDrawdown;
        }
    }
}