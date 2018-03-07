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

namespace TA4Net.Indicators.Helpers
{
    /**
     * Volume indicator.
     * <p></p>
     */
    public class VolumeIndicator : CachedIndicator<decimal>
    {

        private ITimeSeries _series;

        private int _timeFrame;

        public VolumeIndicator(ITimeSeries series)
            : this(series, 1)
        {
        }

        public VolumeIndicator(ITimeSeries series, int timeFrame)
            : base(series)
        {
            _series = series;
            _timeFrame = timeFrame;
        }


        protected override decimal Calculate(int index)
        {
            int startIndex = Math.Max(0, index - _timeFrame + 1);
            decimal sumOfVolume = Decimals.Zero;
            for (int i = startIndex; i <= index; i++)
            {
                sumOfVolume = sumOfVolume.Plus(_series.GetBar(i).Volume);
            }
            return sumOfVolume;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}";
        }
    }
}