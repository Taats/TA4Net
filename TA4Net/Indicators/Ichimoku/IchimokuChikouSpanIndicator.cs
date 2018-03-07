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
using TA4Net.Indicators.Helpers;
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Ichimoku
{
    /**
     * Ichimoku clouds: Chikou Span indicator
     * <p></p>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:ichimoku_cloud">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:ichimoku_cloud</a>
     */
    public class IchimokuChikouSpanIndicator : CachedIndicator<decimal>
    {

        /** The close price */
        private readonly ClosePriceIndicator _closePriceIndicator;

        /** The time delay */
        private readonly int _timeDelay;

        /**
         * Constructor.
         * @param series the series
         */
        public IchimokuChikouSpanIndicator(ITimeSeries series)
            : this(series, 26)
        {
        }

        /**
         * Constructor.
         * @param series the series
         * @param timeDelay the time delay (usually 26)
         */
        public IchimokuChikouSpanIndicator(ITimeSeries series, int timeDelay)
            : base(series)
        {
            _closePriceIndicator = new ClosePriceIndicator(series);
            _timeDelay = timeDelay;
        }

        protected override decimal Calculate(int index)
        {
            return _closePriceIndicator.GetValue(Math.Max(0, index - _timeDelay));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeDelay: {_timeDelay}, ClosePriceIndicator: {_closePriceIndicator.GetConfiguration()}";
        }
    }
}
