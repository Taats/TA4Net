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
using TA4Net.Interfaces;

namespace TA4Net.Trading.Rules
{
    /**
     * An OR combination of two {@link Rule rules}.
     * <p></p>
     * Satisfied when one of the two provided rules is satisfied.<br>
     * Warning: the second rule is not tested if the first rule is satisfied.
     */
    public class OrRule : AbstractRule
    {
        private readonly IRule _rule1;
        private readonly IRule _rule2;

        /**
         * Constructor.
         * @param rule1 a trading rule
         * @param rule2 another trading rule
         */
        public OrRule(IRule rule1, IRule rule2)
        {
            _rule1 = rule1;
            _rule2 = rule2;
        }
        
        public override bool IsSatisfied(int index, ITradingRecord tradingRecord)
        {
            bool satisfied = _rule1.IsSatisfied(index, tradingRecord) || _rule2.IsSatisfied(index, tradingRecord);
            traceIsSatisfied(index, satisfied);
            return satisfied;
        }

        public override string GetConfiguration()
        {
            return $"{GetType()} - Rule1: {_rule1.GetConfiguration()}, Rule2: {_rule2.GetConfiguration()}";
        }
    }
}
