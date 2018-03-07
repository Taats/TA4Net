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
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Volume
{
    /**
     * The volume-weighted average price (VWAP) Indicator.
     * @see <a href="http://www.investopedia.com/articles/trading/11/trading-with-vwap-mvwap.asp">
     *     http://www.investopedia.com/articles/trading/11/trading-with-vwap-mvwap.asp</a>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:vwap_intraday">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:vwap_intraday</a>
     * @see <a href="https://en.wikipedia.org/wiki/Volume-weighted_average_price">
     *     https://en.wikipedia.org/wiki/Volume-weighted_average_price</a>
     */
    public class VWAPIndicator : CachedIndicator<decimal>
    {
        private readonly int _timeFrame;
        private readonly IIndicator<decimal> _typicalPrice;
        private readonly IIndicator<decimal> _volume;

        /**
         * Constructor.
         * @param series the series
         * @param timeFrame the time frame
         */
        public VWAPIndicator(ITimeSeries series, int timeFrame)
            : base(series)
        {
            _timeFrame = timeFrame;
            _typicalPrice = new TypicalPriceIndicator(series);
            _volume = new VolumeIndicator(series);
        }


        protected override decimal Calculate(int index)
        {
            if (index <= 0)
            {
                return _typicalPrice.GetValue(index);
            }
            int startIndex = Math.Max(0, index - _timeFrame + 1);
            decimal cumulativeTPV = Decimals.Zero;
            decimal cumulativeVolume = Decimals.Zero;
            for (int i = startIndex; i <= index; i++)
            {
                decimal currentVolume = _volume.GetValue(i);
                cumulativeTPV = cumulativeTPV.Plus(_typicalPrice.GetValue(i).MultipliedBy(currentVolume));
                cumulativeVolume = cumulativeVolume.Plus(currentVolume);
            }
            return cumulativeTPV.DividedBy(cumulativeVolume);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, TypicalPriceIndicator: {_typicalPrice.GetConfiguration()}, VolumeIndicator: {_volume.GetConfiguration()}";
        }
    }
}
