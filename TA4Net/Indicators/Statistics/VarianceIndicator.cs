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

namespace TA4Net.Indicators.Statistics
{
    /**
     * Variance indicator.
     * <p></p>
     */
    public class VarianceIndicator : CachedIndicator<decimal>
    {
        private IIndicator<decimal> _indicator;
        private int _timeFrame;
        private SMAIndicator _sma;

        /**
         * Constructor.
         * @param indicator the indicator
         * @param timeFrame the time frame
         */
        public VarianceIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator)
        {
            _indicator = indicator;
            _timeFrame = timeFrame;
            _sma = new SMAIndicator(indicator, timeFrame);
        }


        protected override decimal Calculate(int index)
        {
            int startIndex = Math.Max(0, index - _timeFrame + 1);
            int numberOfObservations = index - startIndex + 1;
            decimal variance = Decimals.Zero;
            decimal average = _sma.GetValue(index);
            for (int i = startIndex; i <= index; i++)
            {
                decimal Pow = _indicator.GetValue(i).Minus(average).Pow(2);
                variance = variance.Plus(Pow);
            }
            variance = variance.DividedBy(numberOfObservations);
            return variance;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, Indicator: {_indicator.GetConfiguration()}, SmaIndicator: {_sma.GetConfiguration()}";
        }
    }
}
