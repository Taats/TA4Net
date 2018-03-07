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

namespace TA4Net.Indicators.Helpers
{
    /**
     * Difference indicator.
     * <p></p>
     * I.e.: first - second
     */
    public class DifferenceIndicator : CachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _first;
        private readonly IIndicator<decimal> _second;

        /**
         * Constructor.
         * (first Minus second)
         * @param first the first indicator
         * @param second the second indicator
         */
        public DifferenceIndicator(IIndicator<decimal> first, IIndicator<decimal> second)
            : base(first)
        {
            // TODO: check if first series is equal to second one
            _first = first;
            _second = second;
        }


        protected override decimal Calculate(int index)
        {
            return _first.GetValue(index).Minus(_second.GetValue(index));
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, Indicator1: {_first.GetConfiguration()}, Indicator2: {_second.GetConfiguration()}";
        }
    }
}
