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

namespace TA4Net.Indicators
{
    /**
     * Aroon Oscillator.
     * <p></p>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:aroon_oscillator">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:aroon_oscillator</a>
     */
    public class AroonOscillatorIndicator : CachedIndicator<decimal>
    {

        private readonly AroonDownIndicator _aroonDownIndicator;
        private readonly AroonUpIndicator _aroonUpIndicator;
        private readonly int _timeFrame;

        public AroonOscillatorIndicator(ITimeSeries series, int timeFrame)
            : base(series)
        {
            _timeFrame = timeFrame;
            _aroonDownIndicator = new AroonDownIndicator(series, timeFrame);
            _aroonUpIndicator = new AroonUpIndicator(series, timeFrame);
        }


        protected override decimal Calculate(int index)
        {
            return _aroonUpIndicator.GetValue(index).Minus(_aroonDownIndicator.GetValue(index));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, AroonDownIndicator: {_aroonDownIndicator.GetConfiguration()}, AroonUpIndicator: {_aroonUpIndicator.GetConfiguration()}";
        }
    }
}
