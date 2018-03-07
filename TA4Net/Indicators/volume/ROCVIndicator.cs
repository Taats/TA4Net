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
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Volume
{
    /**
     * Rate of change of volume (ROCVIndicator) indicator.
     * Aka. Momentum of Volume
     * <p></p>
     * The ROCVIndicator calculation compares the current volume with the volume "n" periods ago.
     */
    public class ROCVIndicator : CachedIndicator<decimal>
    {
        private readonly ITimeSeries _series;
        private readonly int _timeFrame;

        /**
         * Constructor.
         *
         * @param series the time series
         * @param timeFrame the time frame
         */
        public ROCVIndicator(ITimeSeries series, int timeFrame)
            : base(series)
        {
            _series = series;
            _timeFrame = timeFrame;
        }


        protected override decimal Calculate(int index)
        {
            int nIndex = Math.Max(index - _timeFrame, 0);
            decimal nPeriodsAgoValue = _series.GetBar(nIndex).Volume;
            decimal currentValue = _series.GetBar(index).Volume;
            return currentValue.Minus(nPeriodsAgoValue)
                    .DividedBy(nPeriodsAgoValue)
                    .MultipliedBy(Decimals.HUNDRED);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}";
        }
    }
}
