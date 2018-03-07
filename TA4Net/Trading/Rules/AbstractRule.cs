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
     * An abstract trading {@link Rule rule}.
     */
    public abstract class AbstractRule : IRule
    {/**
         * @param rule another trading rule
         * @return a rule which is the AND combination of this rule with the provided one
         */
        public virtual IRule And(IRule rule)
        {
            return new AndRule(this, rule);
        }

        /**
         * @param rule another trading rule
         * @return a rule which is the OR combination of this rule with the provided one
         */
        public virtual IRule Or(IRule rule)
        {
            return new OrRule(this, rule);
        }

        /**
         * @param rule another trading rule
         * @return a rule which is the XOR combination of this rule with the provided one
         */
        public virtual IRule Xor(IRule rule)
        {
            return new XorRule(this, rule);
        }

        /**
         * @return a rule which is the logical negation of this rule
         */
        public virtual IRule Negation()
        {
            return new NotRule(this);
        }

        /**
         * @param index the bar index
         * @return true if this rule is satisfied for the provided index, false otherwise
         */
        public virtual bool IsSatisfied(int index)
        {
            return IsSatisfied(index, null);
        }

        public abstract bool IsSatisfied(int index, ITradingRecord tradingRecord);

        /// <summary>
        /// Returns the full configuration, including the referenced objects.
        /// </summary>
        /// <returns></returns>
        public abstract string GetConfiguration();

        /** The logger */
        // protected readonly Logger log = LoggerFactory.getLogger(getClass());

        /** The class name */
        //  protected readonly string className = getClass().getSimpleName();

        /**
         * Traces the isSatisfied() method calls.
         * @param index the bar index
         * @param isSatisfied true if the rule is satisfied, false otherwise
         */
        protected void traceIsSatisfied(int index, bool isSatisfied)
        {
            // log.trace("{}#isSatisfied({}): {}", className, index, isSatisfied);
        }

        
    }
}
