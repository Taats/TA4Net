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
     * Aroon up indicator.
     * <p></p>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:aroon">chart_school:technical_indicators:aroon</a>
     */
    public class AroonUpIndicator : CachedIndicator<decimal>
    {

        private readonly int _timeFrame;

        private readonly HighestValueIndicator _highestMaxPriceIndicator;
        private readonly IIndicator<decimal> _maxValueIndicator;

        /**
         * Constructor.
         * <p>
         * @param series the time series
         * @param MaxValueIndicator the indicator for the Maximum price ( {@link MaxPriceIndicator})
         * @param timeFrame the time frame
         */
        public AroonUpIndicator(ITimeSeries series, IIndicator<decimal> maxValueIndicator, int timeFrame)
            : base(series)
        {
            _timeFrame = timeFrame;
            _maxValueIndicator = maxValueIndicator;

            // + 1 needed for last possible iteration in loop
            _highestMaxPriceIndicator = new HighestValueIndicator(maxValueIndicator, timeFrame + 1);
        }

        /**
         *  Constructor that is using the Maximum price
         * <p>
         * @param series the time series
         * @param timeFrame the time frame
         */
        public AroonUpIndicator(ITimeSeries series, int timeFrame)
            : this(series, new MaxPriceIndicator(series), timeFrame)
        {
        }


        protected override decimal Calculate(int index)
        {
            if (TimeSeries.GetBar(index).MaxPrice.IsNaN())
                return Decimals.NaN;

            // Getting the number of bars since the highest close price
            int endIndex = Math.Max(0, index - _timeFrame);
            int nbBars = 0;
            for (int i = index; i > endIndex; i--)
            {
                if (_maxValueIndicator.GetValue(i).Equals(_highestMaxPriceIndicator.GetValue(index)))
                {
                    break;
                }
                nbBars++;
            }

            return ((decimal)(_timeFrame - nbBars)).DividedBy(_timeFrame).MultipliedBy(Decimals.HUNDRED);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, HighestMaxValueIndicator: {_highestMaxPriceIndicator.GetConfiguration()}, MaxValueIndicator: {_maxValueIndicator.GetConfiguration()}";
        }
    }
}
