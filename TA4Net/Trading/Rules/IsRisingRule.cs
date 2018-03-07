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

namespace TA4Net.Trading.Rules
{
    /**
     * Indicator-rising-indicator rule.
     * <p></p>
     * Satisfied when the values of the {@link Indicator indicator} increase
     * within the timeFrame.
     */
    public class IsRisingRule : AbstractRule
    {

        /** The actual indicator */
        private readonly IIndicator<decimal> _indicator;
        /** The timeFrame */
        private readonly int _timeFrame;
        /** The Minimum required strenght of the rising */
        private double _minStrenght;

        /**
         * Constructor for strict rising.
         * 
         * @param ref the indicator
         * @param timeFrame the time frame
         */
        public IsRisingRule(IIndicator<decimal> indicator, int timeFrame)
            : this(indicator, timeFrame, 1)
        {
        }

        /**
         * Constructor.
         * 
         * @param ref the indicator
         * @param timeFrame the time frame
         * @param MinStrenght the Minimum required rising strenght (between '0' and '1', e.g. '1' for strict rising)
         */
        public IsRisingRule(IIndicator<decimal> indicator, int timeFrame, double MinStrenght)
        {
            _indicator = indicator;
            _timeFrame = timeFrame;
            _minStrenght = MinStrenght;
        }


        public override bool IsSatisfied(int index, ITradingRecord tradingRecord)
        {

            if (_minStrenght >= 1)
            {
                _minStrenght = 0.99;
            }

            int count = 0;
            for (int i = Math.Max(0, index - _timeFrame + 1); i <= index; i++)
            {
                if (_indicator.GetValue(i).IsGreaterThan(_indicator.GetValue(Math.Max(0, i - 1))))
                {
                    count += 1;
                }
            }

            double ratio = count / (double)_timeFrame;

            bool satisfied = ratio >= _minStrenght;
            traceIsSatisfied(index, satisfied);
            return satisfied;
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, Indicator: {_indicator.GetConfiguration()}, TimeFrame: {_timeFrame}, MinStrength: {_minStrenght}";
        }

    }
}
