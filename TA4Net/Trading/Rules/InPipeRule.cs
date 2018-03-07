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
     * Indicator-between-indicators rule.
     * <p></p>
     * Satisfied when the value of the {@link Indicator indicator} is between the values of the boundary (up/down) indicators.
     */
    public class InPipeRule : AbstractRule
    {
        /** The upper indicator */
        private IIndicator<decimal> _upper;
        /** The lower indicator */
        private IIndicator<decimal> _lower;
        /** The evaluated indicator */
        private IIndicator<decimal> _indicator;

        /**
         * Constructor.
         * @param ref the reference indicator
         * @param upper the upper threshold
         * @param lower the lower threshold
         */
        public InPipeRule(IIndicator<decimal> indicator, decimal upper, decimal lower)
                : this(indicator, new ConstantIndicator<decimal>(upper), new ConstantIndicator<decimal>(lower))
        {
        }

        /**
         * Constructor.
         * @param ref the reference indicator
         * @param upper the upper indicator
         * @param lower the lower indicator
         */
        public InPipeRule(IIndicator<decimal> indicator, IIndicator<decimal> upper, IIndicator<decimal> lower)
        {
            _upper = upper;
            _lower = lower;
            _indicator = indicator;
        }
        
        public override bool IsSatisfied(int index, ITradingRecord tradingRecord)
        {
            bool satisfied = _indicator.GetValue(index).IsLessThanOrEqual(_upper.GetValue(index))
                    && _indicator.GetValue(index).IsGreaterThanOrEqual(_lower.GetValue(index));
            traceIsSatisfied(index, satisfied);
            return satisfied;
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, Indicator: {_indicator.GetConfiguration()}, Upper: {_upper.GetConfiguration()}, Lower: {_lower.GetConfiguration()}";
        }
    }
}
