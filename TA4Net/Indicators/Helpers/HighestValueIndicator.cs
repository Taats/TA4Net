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
     * Highest value indicator.
     * <p/>
     */
    public class HighestValueIndicator : CachedIndicator<decimal>
    {

        private readonly IIndicator<decimal> _indicator;

        private readonly int _timeFrame;

        public HighestValueIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator)
        {
            _indicator = indicator;
            _timeFrame = timeFrame;
        }


        protected override decimal Calculate(int index)
        {
            if (_indicator.GetValue(index).IsNaN() && _timeFrame != 1)
                return new HighestValueIndicator(_indicator, _timeFrame - 1).GetValue(index - 1);
            int end = Math.Max(0, index - _timeFrame + 1);
            decimal highest = _indicator.GetValue(index);
            for (int i = index - 1; i >= end; i--)
            {
                if (highest.IsLessThan(_indicator.GetValue(i)))
                {
                    highest = _indicator.GetValue(i);
                }
            }
            return highest;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, Indicator: {_indicator.GetConfiguration()}";
        }
    }
}
