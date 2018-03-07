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
     * WMA indicator.
     * <p></p>
     */
    public class WMAIndicator : CachedIndicator<decimal>
    {
        private int _timeFrame;
        private IIndicator<decimal> _indicator;

        public WMAIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator)
        {
            _indicator = indicator;
            _timeFrame = timeFrame;
        }


        protected override decimal Calculate(int index)
        {
            if (index == 0)
            {
                return _indicator.GetValue(0);
            }
            decimal value = Decimals.ZERO;
            if (index - _timeFrame < 0)
            {

                for (int i = index + 1; i > 0; i--)
                {
                    value = value.Plus(i.MultipliedBy(_indicator.GetValue(i - 1)));
                }
                return value.DividedBy(((index + 1) * (index + 2)) / 2);
            }

            int actualIndex = index;
            for (int i = _timeFrame; i > 0; i--)
            {
                value = value.Plus(i.MultipliedBy(_indicator.GetValue(actualIndex)));
                actualIndex--;
            }
            return value.DividedBy((_timeFrame * (_timeFrame + 1)) / 2);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, indicator: {_indicator.GetConfiguration()}";
        }
    }
}