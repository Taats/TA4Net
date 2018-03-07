/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2014-2017 Marc de Verdelhan & respective authors (see AUTHORS)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
 * the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using TA4Net.Extensions;
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators
{

    /**
     * Percentage price oscillator (PPO) indicator. <br/>
     * Aka. MACD Percentage Price Oscillator (MACD-PPO).
     * </p>
     */
    public class PPOIndicator : CachedIndicator<decimal>
    {
        private readonly EMAIndicator _shortTermEma;
        private readonly EMAIndicator _longTermEma;

        /**
         * Constructor with shortTimeFrame "12" and longTimeFrame "26".
         * 
         * @param indicator the indicator
         */
        public PPOIndicator(IIndicator<decimal> indicator)
            : this(indicator, 12, 26)
        {
        }

        /**
         * Constructor.
         * 
         * @param indicator the indicator
         * @param shortTimeFrame the short time frame
         * @param longTimeFrame the long time frame
         */
        public PPOIndicator(IIndicator<decimal> indicator, int shortTimeFrame, int longTimeFrame)
            : base(indicator)
        {
            if (shortTimeFrame > longTimeFrame)
            {
                throw new ArgumentException("Long term period count must be greater than short term period count");
            }
            _shortTermEma = new EMAIndicator(indicator, shortTimeFrame);
            _longTermEma = new EMAIndicator(indicator, longTimeFrame);
        }


        protected override decimal Calculate(int index)
        {
            decimal shortEmaValue = _shortTermEma.GetValue(index);
            decimal longEmaValue = _longTermEma.GetValue(index);
            return shortEmaValue.Minus(longEmaValue)
                    .DividedBy(longEmaValue)
                    .MultipliedBy(Decimals.HUNDRED);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, LongTermEmaIndicator: {_longTermEma.GetConfiguration()}, ShortTermEmaIndicator: {_shortTermEma.GetConfiguration()}";
        }
    }
}
