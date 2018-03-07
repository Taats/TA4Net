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
using TA4Net.Interfaces;

namespace TA4Net.Indicators
{
    /**
     * Chande's Range Action Verification Index (RAVI) indicator.
     * 
     * To preserve trend direction,  calculation does not use absolute value.
     */
    public class RAVIIndicator : CachedIndicator<decimal>
    {

        private readonly SMAIndicator _shortSma;
        private readonly SMAIndicator _longSma;

        /**
         * Constructor.
         * @param price the price
         * @param shortSmaTimeFrame the time frame for the short SMA (usually 7)
         * @param longSmaTimeFrame the time frame for the long SMA (usually 65)
         */
        public RAVIIndicator(IIndicator<decimal> price, int shortSmaTimeFrame, int longSmaTimeFrame)
            : base(price)
        {
            _shortSma = new SMAIndicator(price, shortSmaTimeFrame);
            _longSma = new SMAIndicator(price, longSmaTimeFrame);
        }


        protected override decimal Calculate(int index)
        {
            decimal shortMA = _shortSma.GetValue(index);
            decimal longMA = _longSma.GetValue(index);
            return shortMA.Minus(longMA)
                    .DividedBy(longMA)
                    .MultipliedBy(Decimals.HUNDRED);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, ShortSmaIndicator: {_shortSma.GetConfiguration()}, LongSmaIndicator: {_longSma.GetConfiguration()}";
        }
    }
}
