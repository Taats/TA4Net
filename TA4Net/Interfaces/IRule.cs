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
namespace TA4Net.Interfaces
{

    /**
     * A rule for strategy building.
     * <p></p>
     * A trading rule may be composed of a combination of other rules.
     *
     * A {@link Strategy trading strategy} is a pair of complementary (entry and exit) rules.
     */
    public interface IRule
    {

        /**
         * @param rule another trading rule
         * @return a rule which is the AND combination of this rule with the provided one
         */
        IRule And(IRule rule);

        /**
         * @param rule another trading rule
         * @return a rule which is the OR combination of this rule with the provided one
         */
        IRule Or(IRule rule);

        /**
         * @param rule another trading rule
         * @return a rule which is the XOR combination of this rule with the provided one
         */
        IRule Xor(IRule rule);

        /**
         * @return a rule which is the logical negation of this rule
         */
        IRule Negation();

        /**
         * @param index the bar index
         * @return true if this rule is satisfied for the provided index, false otherwise
         */
        bool IsSatisfied(int index);

        /**
         * @param index the bar index
         * @param tradingRecord the potentially needed trading history
         * @return true if this rule is satisfied for the provided index, false otherwise
         */
        bool IsSatisfied(int index, ITradingRecord tradingRecord);

        /// <summary>
        /// Return the configuration of the rule.
        /// </summary>
        /// <returns></returns>
        string GetConfiguration();
    }
}
