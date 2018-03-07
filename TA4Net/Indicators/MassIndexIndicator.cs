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
     * Mass index indicator.
     * <p></p>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:mass_index">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:mass_index</a>
     */
    public class MassIndexIndicator : CachedIndicator<decimal>
    {
        private readonly EMAIndicator _singleEma;
        private readonly EMAIndicator _doubleEma;
        private readonly int _timeFrame;

        /**
         * Constructor.
         * @param series the time series
         * @param emaTimeFrame the time frame for EMAs (usually 9)
         * @param timeFrame the time frame
         */
        public MassIndexIndicator(ITimeSeries series, int emaTimeFrame, int timeFrame)
            : base(series)
        {
            IIndicator<decimal> highLowDifferential = new DifferenceIndicator(
                    new MaxPriceIndicator(series),
                    new MinPriceIndicator(series)
            );
            _singleEma = new EMAIndicator(highLowDifferential, emaTimeFrame);
            _doubleEma = new EMAIndicator(_singleEma, emaTimeFrame); // Not the same formula as doubleEMAIndicator
            _timeFrame = timeFrame;
        }


        protected override decimal Calculate(int index)
        {
            int startIndex = Math.Max(0, index - _timeFrame + 1);
            decimal massIndex = Decimals.Zero;
            for (int i = startIndex; i <= index; i++)
            {
                decimal emaRatio = _singleEma.GetValue(i).DividedBy(_doubleEma.GetValue(i));
                massIndex = massIndex.Plus(emaRatio);
            }
            return massIndex;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, SingleEmaIndicator: {_singleEma.GetConfiguration()}, DoubleEmaIndicator: {_doubleEma.GetConfiguration()}";
        }
    }
}
