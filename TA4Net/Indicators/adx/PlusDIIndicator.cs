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

namespace TA4Net.Indicators.Adx
{
    /**
     * +DI indicator.
     * Part of the Directional Movement System
     * <p>
     * </p>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:average_directional_index_adx">
     * http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:average_directional_index_adx</a>
     */
    public class PlusDIIndicator : CachedIndicator<decimal>
    {

        private readonly MMAIndicator _avgPlusDMindicator;
        private readonly ATRIndicator _atrIndicator;
        private readonly int _timeFrame;

        public PlusDIIndicator(ITimeSeries series, int timeFrame)
            : base(series)
        {
            _avgPlusDMindicator = new MMAIndicator(new PlusDMIndicator(series), timeFrame);
            _atrIndicator = new ATRIndicator(series, timeFrame);
            _timeFrame = timeFrame;
        }


        protected override decimal Calculate(int index)
        {
            return _avgPlusDMindicator.GetValue(index).DividedBy(_atrIndicator.GetValue(index)).MultipliedBy(Decimals.HUNDRED);
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, TimeFrame: {_timeFrame}, ATRIndicator: {_atrIndicator.GetConfiguration()}, MMAIndicator: {_avgPlusDMindicator.GetConfiguration()}";
        }
    }
}
