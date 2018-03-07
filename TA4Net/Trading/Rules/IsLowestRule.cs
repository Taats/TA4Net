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
     * Indicator-lowest-indicator rule.
     * <p></p>
     * Satisfied when the value of the {@link Indicator indicator} is the lowest
     * within the timeFrame.
     */
    public class IsLowestRule : AbstractRule
    {

        /** The actual indicator */
        private readonly IIndicator<decimal> _indicator;
        /** The timeFrame */
        private readonly int _timeFrame;

        /**
         * Constructor.
         * 
         * @param ref the indicator
         * @param timeFrame the time frame
         */
        public IsLowestRule(IIndicator<decimal> indicator, int timeFrame)
        {
            _indicator = indicator;
            _timeFrame = timeFrame;
        }

        public override bool IsSatisfied(int index, ITradingRecord tradingRecord)
        {
            LowestValueIndicator lowest = new LowestValueIndicator(_indicator, _timeFrame);
            decimal lowestVal = lowest.GetValue(index);
            decimal refVal = _indicator.GetValue(index);

            bool satisfied = !refVal.IsNaN() && !lowestVal.IsNaN() && refVal.Equals(lowestVal);
            traceIsSatisfied(index, satisfied);
            return satisfied;
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, Indicator: {_indicator.GetConfiguration()}, TimeFrame: {_timeFrame}";
        }
    }
}
