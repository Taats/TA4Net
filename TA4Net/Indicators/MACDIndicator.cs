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
     * Moving average convergence divergence (MACDIndicator) indicator. <br/>
     * Aka. MACD Absolute Price Oscillator (APO).
     * </p>
     * see
     * http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:moving_average_convergence_divergence_macd
     */
    public class MACDIndicator : CachedIndicator<decimal>
    {
        private readonly EMAIndicator _shortTermEma;
        private readonly EMAIndicator _longTermEma;

        /**
         * Constructor with shortTimeFrame "12" and longTimeFrame "26".
         *
         * @param indicator the indicator
         */
        public MACDIndicator(IIndicator<decimal> indicator)
           : this(indicator, 12, 26)
        {
        }

        /**
         * Constructor.
         *
         * @param indicator the indicator
         * @param shortTimeFrame the short time frame (normally 12)
         * @param longTimeFrame the long time frame (normally 26)
         */
        public MACDIndicator(IIndicator<decimal> indicator, int shortTimeFrame, int longTimeFrame)
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
            return _shortTermEma.GetValue(index).Minus(_longTermEma.GetValue(index));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, LlongTermEmaIndicator: {_longTermEma.GetConfiguration()}, ShortTermEmaIndicator: {_shortTermEma.GetConfiguration()}";
        }
    }
}
