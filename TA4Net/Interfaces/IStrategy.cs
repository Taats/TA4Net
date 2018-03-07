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
     * A trading strategy.
     * <p></p>
     * A strategy is a pair of complementary {@link Rule rules}. It may recommend to enter or to exit.
     * Recommendations are based respectively on the entry rule or on the exit rule.
     */
    public interface IStrategy
    {

        /**
         * @return the name of the strategy
         */
        string Name { get; }

        /**
         * @return the entry rule
         */
        IRule EntryRule { get; }

        /**
         * @return the exit rule
         */
        IRule ExitRule { get; }

        /**
         * @param strategy the other strategy
         * @return the AND combination of two {@link Strategy strategies}
         */
        IStrategy And(IStrategy strategy);

        /**
         * @param strategy the other strategy
         * @return the OR combination of two {@link Strategy strategies}
         */
        IStrategy Or(IStrategy strategy);

        /**
         * @param name the name of the strategy
         * @param strategy the other strategy
         * @param unstablePeriod number of bars that will be strip off for this strategy
         * @return the AND combination of two {@link Strategy strategies}
         */
        IStrategy And(string name, IStrategy strategy, int unstablePeriod);

        /**
         * @param name the name of the strategy
         * @param strategy the other strategy
         * @param unstablePeriod number of bars that will be strip off for this strategy
         * @return the OR combination of two {@link Strategy strategies}
         */
        IStrategy Or(string name, IStrategy strategy, int unstablePeriod);

        /**
         * @return the opposite of the {@link Strategy strategy}
         */
        IStrategy Opposite();

        /**
         * @param unstablePeriod number of bars that will be strip off for this strategy
         */
        void SetUnstablePeriod(int unstablePeriod);

        /**
         * @return unstablePeriod number of bars that will be strip off for this strategy
         */
        int GetUnstablePeriod();

        /**
         * @param index a bar index
         * @return true if this strategy is unstable at the provided index, false otherwise (stable)
         */
        bool IsUnstableAt(int index);

        /**
         * @param index the bar index
         * @param tradingRecord the potentially needed trading history
         * @return true to recommend an order, false otherwise (no recommendation)
         */
        bool ShouldOperate(int index, ITradingRecord tradingRecord);

        /**
         * @param index the bar index
         * @return true to recommend to enter, false otherwise
         */
        bool ShouldEnter(int index);

        /**
         * @param index the bar index
         * @param tradingRecord the potentially needed trading history
         * @return true to recommend to enter, false otherwise
         */
        bool ShouldEnter(int index, ITradingRecord tradingRecord);

        /**
         * @param index the bar index
         * @return true to recommend to exit, false otherwise
         */
        bool ShouldExit(int index);

        /**
         * @param index the bar index
         * @param tradingRecord the potentially needed trading history
         * @return true to recommend to exit, false otherwise
         */
        bool ShouldExit(int index, ITradingRecord tradingRecord);
    }
}
