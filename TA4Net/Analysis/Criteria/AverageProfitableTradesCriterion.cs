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
     * Average profitable trades criterion.
     * <p></p>
     * The number of profitable trades.
     */
    public class AverageProfitableTradesCriterion : AbstractAnalysisCriterion
    {
        public override decimal Calculate(ITimeSeries series, Trade trade)
        {
            int entryIndex = trade.GetEntry().getIndex();
            int exitIndex = trade.GetExit().getIndex();

            decimal result;
            if (trade.GetEntry().isBuy())
            {
                // buy-then-sell trade
                result = series.GetBar(exitIndex).ClosePrice.DividedBy(series.GetBar(entryIndex).ClosePrice);
            }
            else
            {
                // sell-then-buy trade
                result = series.GetBar(entryIndex).ClosePrice.DividedBy(series.GetBar(exitIndex).ClosePrice);
            }

            return (result.IsGreaterThan(Decimals.ONE)) ? 1M : 0M;
        }


        public override decimal Calculate(ITimeSeries series, ITradingRecord tradingRecord)
        {
            int numberOfProfitable = 0;
            foreach (Trade trade in tradingRecord.Trades)
            {
                int entryIndex = trade.GetEntry().getIndex();
                int exitIndex = trade.GetExit().getIndex();

                decimal result;
                if (trade.GetEntry().isBuy())
                {
                    // buy-then-sell trade
                    result = series.GetBar(exitIndex).ClosePrice.DividedBy(series.GetBar(entryIndex).ClosePrice);
                }
                else
                {
                    // sell-then-buy trade
                    result = series.GetBar(entryIndex).ClosePrice.DividedBy(series.GetBar(exitIndex).ClosePrice);
                }
                if (result.IsGreaterThan(Decimals.ONE))
                {
                    numberOfProfitable++;
                }
            }
            return ((decimal)numberOfProfitable).DividedBy(tradingRecord.GetTradeCount());
        }


        public override bool BetterThan(decimal criterionValue1, decimal criterionValue2)
        {
            return criterionValue1 > criterionValue2;
        }
    }
}
