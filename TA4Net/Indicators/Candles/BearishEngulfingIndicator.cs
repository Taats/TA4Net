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

namespace TA4Net.Indicators.Candles
{
    /**
     * Bearish engulfing pattern indicator.
     * <p></p>
     * @see <a href="http://www.investopedia.com/terms/b/bearishengulfingp.asp">
     *     http://www.investopedia.com/terms/b/bearishengulfingp.asp</a>
     */
    public class BearishEngulfingIndicator : CachedIndicator<bool>
    {

        private readonly ITimeSeries _series;

        /**
         * Constructor.
         * @param series a time series
         */
        public BearishEngulfingIndicator(ITimeSeries series)
            : base(series)
        {
            _series = series;
        }
        
        protected override bool Calculate(int index)
        {
            if (index < 1)
            {
                // Engulfing is a 2-candle pattern
                return false;
            }
            IBar prevBar = _series.GetBar(index - 1);
            IBar currBar = _series.GetBar(index);
            if (prevBar.IsBullish() && currBar.IsBearish())
            {
                decimal prevOpenPrice = prevBar.OpenPrice;
                decimal prevClosePrice = prevBar.ClosePrice;
                decimal currOpenPrice = currBar.OpenPrice;
                decimal currClosePrice = currBar.ClosePrice;
                return currOpenPrice.IsGreaterThan(prevOpenPrice) && currOpenPrice.IsGreaterThan(prevClosePrice)
                        && currClosePrice.IsLessThan(prevOpenPrice) && currClosePrice.IsLessThan(prevClosePrice);
            }
            return false;
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}";
        }
    }
}
