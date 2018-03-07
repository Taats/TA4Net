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

namespace TA4Net.Indicators
{
    /**
     * The Stochastic RSI Indicator.
     * 
     * Stoch RSI = (RSI - MinimumRSIn) / (MaximumRSIn - MinimumRSIn)
     */
    public class StochasticRSIIndicator : CachedIndicator<decimal>
    {

        private readonly RSIIndicator _rsi;
        private readonly LowestValueIndicator _minRsi;
        private readonly HighestValueIndicator _maxRsi;

        /**
         * Constructor.
         * @param series the series
         * @param timeFrame the time frame
         */
        public StochasticRSIIndicator(ITimeSeries series, int timeFrame)
            : this(new ClosePriceIndicator(series), timeFrame)
        {
        }

        /**
         * Constructor.
         * @param indicator the indicator
         * @param timeFrame the time frame
         */
        public StochasticRSIIndicator(IIndicator<decimal> indicator, int timeFrame)
            : this(new RSIIndicator(indicator, timeFrame), timeFrame)
        {
        }

        /**
         * Constructor.
         * @param rsi the rsi indicator
         * @param timeFrame the time frame
         */
        public StochasticRSIIndicator(RSIIndicator rsi, int timeFrame)
            : base(rsi)
        {
            _rsi = rsi;
            _minRsi = new LowestValueIndicator(rsi, timeFrame);
            _maxRsi = new HighestValueIndicator(rsi, timeFrame);
        }


        protected override decimal Calculate(int index)
        {
            decimal MinRsiValue = _minRsi.GetValue(index);
            return _rsi.GetValue(index).Minus(MinRsiValue)
                    .DividedBy(_maxRsi.GetValue(index).Minus(MinRsiValue));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, RSIIndicator: {_rsi.GetConfiguration()}, MinRSIIndicator: {_minRsi.GetConfiguration()}, MaxRSIIndicator: {_maxRsi.GetConfiguration()}";
        }
    }
}
