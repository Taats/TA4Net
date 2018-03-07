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
using TA4Net.Indicators.Statistics;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Bollinger
{
    /**
     * %B indicator.
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:bollinger_band_perce>
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:bollinger_band_perce</a>
     */
    public class PercentBIndicator : CachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _indicator;
        private readonly BollingerBandsUpperIndicator _bbu;
        private readonly BollingerBandsLowerIndicator _bbl;

        /**
         * Constructor.
         * @param indicator an indicator (usually close price)
         * @param timeFrame the time frame
         * @param k the K multiplier (usually 2.0)
         */
        public PercentBIndicator(IIndicator<decimal> indicator, int timeFrame, decimal k)
            : base(indicator)
        {
            _indicator = indicator;
            BollingerBandsMiddleIndicator bbm = new BollingerBandsMiddleIndicator(new SMAIndicator(indicator, timeFrame));
            StandardDeviationIndicator sd = new StandardDeviationIndicator(indicator, timeFrame);
            _bbu = new BollingerBandsUpperIndicator(bbm, sd, k);
            _bbl = new BollingerBandsLowerIndicator(bbm, sd, k);
        }


        protected override decimal Calculate(int index)
        {
            decimal value = _indicator.GetValue(index);
            decimal upValue = _bbu.GetValue(index);
            decimal lowValue = _bbl.GetValue(index);
            return value.Minus(lowValue).DividedBy(upValue.Minus(lowValue));
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, Indicator: {_indicator.GetConfiguration()}, BollingerBandsUpperIndicator: {_bbu.GetConfiguration()}, BollingBandsLowerIndicator: {_bbl.GetConfiguration()}";
        }
    }
}
