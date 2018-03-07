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
     * A one-shot rule.<p />
     * Satisfied the first time it's checked then never again.
     */
    public class JustOnceRule : AbstractRule
    {
        private readonly IRule _rule;
        private bool satisfied = false;

        /**
         * Constructor.<p />
         * Satisfied the first time the inner rule is satisfied then never again.
         * @param rule the rule that should be satisfied only the first time
         */
        public JustOnceRule(IRule rule)
        {
            _rule = rule;
        }

        /**
         * Constructor.<p />
         * Satisfied the first time it's checked then never again.
         */
        public JustOnceRule()
        {
            _rule = null;
        }

        public override bool IsSatisfied(int index, ITradingRecord tradingRecord)
        {
            if (satisfied)
            {
                return false;
            }
            else if (_rule == null)
            {
                satisfied = true;
                traceIsSatisfied(index, true);
                return true;
            }
            satisfied = _rule.IsSatisfied(index, tradingRecord);
            return satisfied;
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, Rule: {_rule.GetConfiguration()}";
        }
    }
}
