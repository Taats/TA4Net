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
     * Total profit criterion.
     * <p></p>
     * The total profit of the provided {@link Trade trade(s)} over the provided {@link TimeSeries series}.
     */
    public class TotalProfitCriterion : AbstractAnalysisCriterion
    {


        public override decimal Calculate(ITimeSeries series, ITradingRecord tradingRecord)
        {
            decimal value = 1M;
            foreach (Trade trade in tradingRecord.Trades)
            {
                value *= CalculateProfit(series, trade);
            }
            return value;
        }


        public override decimal Calculate(ITimeSeries series, Trade trade)
        {
            return CalculateProfit(series, trade);
        }


        public override bool BetterThan(decimal criterionValue1, decimal criterionValue2)
        {
            return criterionValue1 > criterionValue2;
        }

        /**
         * Calculates the profit of a trade (Buy and sell).
         * @param series a time series
         * @param trade a trade
         * @return the profit of the trade
         */
        private decimal CalculateProfit(ITimeSeries series, Trade trade)
        {
            decimal profit = Decimals.ONE;
            if (trade.IsClosed())
            {
                // use price of entry/exit order, if NaN use close price of underlying time series
                decimal exitClosePrice = trade.GetExit().getPrice().IsNaN() ?
                        series.GetBar(trade.GetExit().getIndex()).ClosePrice : trade.GetExit().getPrice();
                decimal entryClosePrice = trade.GetEntry().getPrice().IsNaN() ?
                        series.GetBar(trade.GetEntry().getIndex()).ClosePrice : trade.GetEntry().getPrice();

                if (trade.GetEntry().isBuy())
                {
                    profit = exitClosePrice.DividedBy(entryClosePrice);
                }
                else
                {
                    profit = entryClosePrice.DividedBy(exitClosePrice);
                }
            }
            return profit;
        }
    }
}
