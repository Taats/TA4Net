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

namespace TA4Net.Trading.Rules
{
    /**
     * Indicator-in-slope rule.
     * <p></p>
     * Satisfied when the difference of the value of the {@link Indicator indicator}
     * and the previous (n-th) value of the {@link Indicator indicator} is between the values of
     * MaxSlope or/and MinSlope. It can test both, positive and negative slope.
     */
    public class InSlopeRule : AbstractRule
    {

        /** The actual indicator */
        private IIndicator<decimal> _indicator;
        /** The previous n-th value of ref */
        private PreviousValueIndicator _prev;
        /** The Minimum slope between ref and prev */
        private decimal _minSlope;
        /** The Maximum slope between ref and prev */
        private decimal _maxSlope;

        /**
         * Constructor.
         * @param ref the reference indicator
         * @param MinSlope Minumum slope between reference and previous indicator
         */
        public InSlopeRule(IIndicator<decimal> indicator, decimal MinSlope)
            : this(indicator, 1, MinSlope, Decimals.NaN)
        {
        }

        /**
         * Constructor.
         * @param ref the reference indicator
         * @param MinSlope Minumum slope between value of reference and previous indicator
         * @param MaxSlope Maximum slope between value of reference and previous indicator
         */
        public InSlopeRule(IIndicator<decimal> indicator, decimal MinSlope, decimal MaxSlope)
            : this(indicator, 1, MinSlope, MaxSlope)
        {
        }

        /**
        * Constructor.
        * @param ref the reference indicator
        * @param nthPrevious defines the previous n-th indicator
        * @param MaxSlope Maximum slope between value of reference and previous indicator
        */
        public InSlopeRule(IIndicator<decimal> indicator, int nthPrevious, decimal MaxSlope)
            : this(indicator, nthPrevious, Decimals.NaN, MaxSlope)
        {
        }

        /**
         * Constructor.
         * @param ref the reference indicator
         * @param nthPrevious defines the previous n-th indicator
         * @param MinSlope Minumum slope between value of reference and previous indicator
         * @param MaxSlope Maximum slope between value of reference and previous indicator
         */
        public InSlopeRule(IIndicator<decimal> indicator, int nthPrevious, decimal MinSlope, decimal MaxSlope)
        {
            _indicator = indicator;
            _prev = new PreviousValueIndicator(indicator, nthPrevious);
            _minSlope = MinSlope;
            _maxSlope = MaxSlope;
        }
        
        public override bool IsSatisfied(int index, ITradingRecord tradingRecord)
        {
            DifferenceIndicator diff = new DifferenceIndicator(_indicator, _prev);
            decimal val = diff.GetValue(index);
            bool MinSlopeSatisfied = _minSlope.IsNaN() || val.IsGreaterThanOrEqual(_minSlope);
            bool MaxSlopeSatisfied = _maxSlope.IsNaN() || val.IsLessThanOrEqual(_maxSlope);
            bool isNaN = _minSlope.IsNaN() && _maxSlope.IsNaN();

            bool satisfied = MinSlopeSatisfied && MaxSlopeSatisfied && !isNaN;
            traceIsSatisfied(index, satisfied);
            return satisfied;
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, Indicator: {_indicator.GetConfiguration()}, Previous: {_prev.GetConfiguration()}, MinSlope: {_minSlope}, MaxSlope: {_maxSlope}";
        }
    }
}
