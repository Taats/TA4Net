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
     * Chande Momentum Oscillator indicator.
     * <p></p>
     * @see <a href="http://tradingsim.com/blog/chande-momentum-oscillator-cmo-technical-indicator/">
     *     http://tradingsim.com/blog/chande-momentum-oscillator-cmo-technical-indicator/</a>
     * @see <a href="http://www.investopedia.com/terms/c/chandemomentumoscillator.asp">
     *     href="http://www.investopedia.com/terms/c/chandemomentumoscillator.asp"</a>
     */
    public class CMOIndicator : CachedIndicator<decimal>
    {
        private readonly GainIndicator _gainIndicator;
        private readonly LossIndicator _lossIndicator;
        private readonly int _timeFrame;

        /**
         * Constructor.
         *
         * @param indicator a price indicator
         * @param timeFrame the time frame
         */
        public CMOIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator)
        {
            _gainIndicator = new GainIndicator(indicator);
            _lossIndicator = new LossIndicator(indicator);
            _timeFrame = timeFrame;
        }


        protected override decimal Calculate(int index)
        {
            decimal sumOfGains = Decimals.Zero;
            for (int i = Math.Max(1, index - _timeFrame + 1); i <= index; i++)
            {
                sumOfGains = sumOfGains.Plus(_gainIndicator.GetValue(i));
            }
            decimal sumOfLosses = Decimals.Zero;
            for (int i = Math.Max(1, index - _timeFrame + 1); i <= index; i++)
            {
                sumOfLosses = sumOfLosses.Plus(_lossIndicator.GetValue(i));
            }
            return sumOfGains.Minus(sumOfLosses)
                    .DividedBy(sumOfGains.Plus(sumOfLosses))
                    .MultipliedBy(Decimals.HUNDRED);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, GainIndicator: {_gainIndicator.GetConfiguration()}, LossIndicator: {_lossIndicator.GetConfiguration()}";
        }
    }
}
