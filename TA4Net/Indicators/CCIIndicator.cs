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
using TA4Net.Indicators.Statistics;
using TA4Net.Interfaces;

namespace TA4Net.Indicators
{
    /**
     * Commodity Channel Index (CCI) indicator.
     * <p/>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:commodity_channel_in">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:commodity_channel_in</a>
     */
    public class CCIIndicator : CachedIndicator<decimal>
    {

        public static readonly decimal FACTOR = 0.015M;

        private readonly TypicalPriceIndicator _typicalPriceInd;
        private readonly SMAIndicator _smaInd;
        private readonly MeanDeviationIndicator _meanDeviationInd;
        private readonly int _timeFrame;

        /**
         * Constructor.
         * @param series the time series
         * @param timeFrame the time frame (normally 20)
         */
        public CCIIndicator(ITimeSeries series, int timeFrame)
            : base(series)
        {
            _typicalPriceInd = new TypicalPriceIndicator(series);
            _smaInd = new SMAIndicator(_typicalPriceInd, timeFrame);
            _meanDeviationInd = new MeanDeviationIndicator(_typicalPriceInd, timeFrame);
            _timeFrame = timeFrame;
        }


        protected override decimal Calculate(int index)
        {
            decimal typicalPrice = _typicalPriceInd.GetValue(index);
            decimal typicalPriceAvg = _smaInd.GetValue(index);
            decimal meanDeviation = _meanDeviationInd.GetValue(index);
            if (meanDeviation.IsZero())
            {
                return Decimals.Zero;
            }
            return (typicalPrice.Minus(typicalPriceAvg)).DividedBy(meanDeviation.MultipliedBy(FACTOR));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, MeanDeviationIndicator: {_meanDeviationInd.GetConfiguration()}, SMAIndicator: {_smaInd.GetConfiguration()}, TypicalPriceIndicator: {_typicalPriceInd.GetConfiguration()}";
        }
    }
}
