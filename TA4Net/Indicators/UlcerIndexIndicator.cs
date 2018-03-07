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

namespace TA4Net.Indicators
{
    /**
     * Ulcer index indicator.
     * <p/>
     * @apiNote Minimal deviations in last decimal places possible. During the calculations this indicator converts {@link decimal decimal/Bigdecimal} to to {@link double double}
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:ulcer_index">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:ulcer_index</a>
     * @see <a href="https://en.wikipedia.org/wiki/Ulcer_index">https://en.wikipedia.org/wiki/Ulcer_index</a>
     */
    public class UlcerIndexIndicator : CachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _indicator;
        private readonly HighestValueIndicator _highestValueInd;
        private readonly int _timeFrame;

        /**
         * Constructor.
         * @param indicator the indicator
         * @param timeFrame the time frame
         */
        public UlcerIndexIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator)
        {
            _indicator = indicator;
            _timeFrame = timeFrame;
            _highestValueInd = new HighestValueIndicator(indicator, timeFrame);
        }


        protected override decimal Calculate(int index)
        {
            int startIndex = Math.Max(0, index - _timeFrame + 1);
            int numberOfObservations = index - startIndex + 1;
            decimal squaredAverage = Decimals.Zero;
            for (int i = startIndex; i <= index; i++) {
                decimal currentValue = _indicator.GetValue(i);
                decimal highestValue = _highestValueInd.GetValue(i);
                decimal percentageDrawdown = currentValue.Minus(highestValue).DividedBy(highestValue).MultipliedBy(Decimals.HUNDRED);
                squaredAverage = squaredAverage.Plus(percentageDrawdown.Pow(2));
            }
            squaredAverage = squaredAverage.DividedBy(numberOfObservations);
            return squaredAverage.Sqrt();
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, HighestValueIndicator: {_highestValueInd.GetConfiguration()}, Indicator: {_indicator.GetConfiguration()}";
        }
    }
}
