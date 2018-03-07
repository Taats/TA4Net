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

namespace TA4Net.Indicators.Candles
{
    /**
     * Doji indicator.
     * <p></p>
     * A candle/bar is considered Doji if its body height is lower than the average multiplied by a factor.
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:chart_analysis:introduction_to_candlesbars#doji">
     *     http://stockcharts.com/school/doku.php?id=chart_school:chart_analysis:introduction_to_candlesbars#doji</a>
     */
    public class DojiIndicator : CachedIndicator<bool>
    {

        /** Body height */
        private readonly IIndicator<decimal> _bodyHeightInd;
        /** Average body height */
        private readonly SMAIndicator _averageBodyHeightInd;

        private readonly decimal _factor;

        /**
         * Constructor.
         * @param series a time series
         * @param timeFrame the number of bars used to Calculate the average body height
         * @param bodyFactor the factor used when checking if a candle is Doji
         */
        public DojiIndicator(ITimeSeries series, int timeFrame, decimal bodyFactor)
            : base(series)
        {
            _bodyHeightInd = new AbsoluteIndicator(new RealBodyIndicator(series));
            _averageBodyHeightInd = new SMAIndicator(_bodyHeightInd, timeFrame);
            _factor = bodyFactor;
        }

        protected override bool Calculate(int index)
        {
            if (index < 1)
            {
                return _bodyHeightInd.GetValue(index).IsZero();
            }

            decimal averageBodyHeight = _averageBodyHeightInd.GetValue(index - 1);
            decimal currentBodyHeight = _bodyHeightInd.GetValue(index);

            return currentBodyHeight.IsLessThan(averageBodyHeight.MultipliedBy(_factor));
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, Factor: {_factor}, BodyHeightIndicator: {_bodyHeightInd.GetConfiguration()}, AverageBodyHeigtIndicator: {_averageBodyHeightInd.GetConfiguration()}";
        }
    }
}
