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
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers
{
    /**
     * Returns the previous (n-th) value of an indicator
     * <p></p>
     */
    public class PreviousValueIndicator : CachedIndicator<decimal>
    {

        private int _n;
        private IIndicator<decimal> _indicator;

        /**
         * Constructor.
         * @param indicator the indicator of which the previous value should be Calculated
         */
        public PreviousValueIndicator(IIndicator<decimal> indicator)
            : this(indicator, 1)
        {
        }

        /**
         * Constructor.
         * @param indicator the indicator of which the previous value should be Calculated
         * @param n parameter defines the previous n-th value
         */
        public PreviousValueIndicator(IIndicator<decimal> indicator, int n)
            : base(indicator)
        {
            _n = n;
            _indicator = indicator;
        }

        protected override decimal Calculate(int index)
        {
            int previousValue = Math.Max(0, (index - _n));
            return _indicator.GetValue(previousValue);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, N: {_n}, Indicator: {_indicator.GetConfiguration()}";
        }
    }
}