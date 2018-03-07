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
using TA4Net.Indicators.Helpers;
using TA4Net.Interfaces;

namespace TA4Net.Trading.Rules
{
    /**
     * Crossed-down indicator rule.
     * <p></p>
     * Satisfied when the value of the first {@link Indicator indicator} crosses-down the value of the second one.
     */
    public class CrossedDownIndicatorRule : AbstractRule
    {

        /** The cross indicator */
        private CrossIndicator _cross;

        /**
         * Constructor.
         * @param indicator the indicator
         * @param threshold a threshold
         */
        public CrossedDownIndicatorRule(IIndicator<decimal> indicator, decimal threshold)
            : this(indicator, new ConstantIndicator<decimal>(threshold))
        {
        }

        /**
         * Constructor.
         * @param first the first indicator
         * @param second the second indicator
         */
        public CrossedDownIndicatorRule(IIndicator<decimal> first, IIndicator<decimal> second)
        {
            _cross = new CrossIndicator(first, second);
        }


        public override bool IsSatisfied(int index, ITradingRecord tradingRecord)
        {
            bool satisfied = _cross.GetValue(index);
            traceIsSatisfied(index, satisfied);
            return satisfied;
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, Cross: {_cross.GetConfiguration()}";
        }
    }
}
