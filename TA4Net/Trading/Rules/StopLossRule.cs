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
using TA4Net.Indicators.Helpers;
using TA4Net.Interfaces;

namespace TA4Net.Trading.Rules
{
    /**
     * A stop-loss rule.
     * <p></p>
     * Satisfied when the close price reaches the loss threshold.
     */
    public class StopLossRule : AbstractRule
    {

        /** The close price indicator */
        private ClosePriceIndicator _closePrice;

        /** The loss ratio threshold (e.g. 0.97 for 3%) */
        private decimal _lossRatioThreshold;

        /**
         * Constructor.
         * @param closePrice the close price indicator
         * @param lossPercentage the loss percentage
         */
        public StopLossRule(ClosePriceIndicator closePrice, decimal lossPercentage)
        {
            _closePrice = closePrice;
            _lossRatioThreshold = Decimals.HUNDRED.Minus(lossPercentage).DividedBy(Decimals.HUNDRED);
        }
        
        public override bool IsSatisfied(int index, ITradingRecord tradingRecord)
        {
            bool satisfied = false;
            // No trading history or no trade opened, no loss
            if (tradingRecord != null)
            {
                Trade currentTrade = tradingRecord.GetCurrentTrade();
                if (currentTrade.IsOpened())
                {
                    decimal entryPrice = currentTrade.GetEntry().getPrice();
                    decimal currentPrice = _closePrice.GetValue(index);
                    decimal threshold = entryPrice.MultipliedBy(_lossRatioThreshold);
                    if (currentTrade.GetEntry().isBuy())
                    {
                        satisfied = currentPrice.IsLessThanOrEqual(threshold);
                    }
                    else
                    {
                        satisfied = currentPrice.IsGreaterThanOrEqual(threshold);
                    }
                }
            }

            traceIsSatisfied(index, satisfied);
            return satisfied;
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, Indicator: {_closePrice.GetConfiguration()}, Treshold: {_lossRatioThreshold}";
        }
    }
}
