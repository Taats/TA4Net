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
using System.Collections.Generic;
using TA4Net.Trading.Rules.Types;

namespace TA4Net.Interfaces {
    /**
     * A history/record of a trading session.
     * <p></p>
     * Holds the full trading record when running a {@link Strategy strategy}.
     * It is used to:
     * <ul>
     *     <li>check to satisfaction of some trading rules (when running a strategy)
     *     <li>analyze the performance of a trading strategy
     * </ul>
     */
    public interface ITradingRecord {

        /**
         * @return the current trade
         */
        Trade GetCurrentTrade();

        /**
         * Operates an order in the trading record.
         * @param index the index to operate the order
         */
        void Operate(int index);

        /**
         * Operates an order in the trading record.
         * @param index the index to operate the order
         * @param price the price of the order
         * @param amount the amount to be ordered
         */
        void Operate(int index, decimal price, decimal amount);

        /**
         * Operates an entry order in the trading record.
         * @param index the index to operate the entry
         * @return true if the entry has been operated, false otherwise
         */
        bool Enter(int index);

        /**
         * Operates an entry order in the trading record.
         * @param index the index to operate the entry
         * @param price the price of the order
         * @param amount the amount to be ordered
         * @return true if the entry has been operated, false otherwise
         */
        bool Enter(int index, decimal price, decimal amount);

        /**
         * Operates an exit order in the trading record.
         * @param index the index to operate the exit
         * @return true if the exit has been operated, false otherwise
         */
        bool Exit(int index);

        /**
         * Operates an exit order in the trading record.
         * @param index the index to operate the exit
         * @param price the price of the order
         * @param amount the amount to be ordered
         * @return true if the exit has been operated, false otherwise
         */
        bool Exit(int index, decimal price, decimal amount);

        /**
         * @return true if no trade is open, false otherwise
         */
        bool IsClosed();

        /**
         * @return the recorded trades
         */
        IReadOnlyList<Trade> Trades { get; }

        /**
         * @return the number of recorded trades
         */
        int GetTradeCount();

        /**
         * @return the last trade recorded
         */
        Trade GetLastTrade();

        /**
         * @return the last order recorded
         */
        Order GetLastOrder();

        /**
         * @param orderType the type of the order to get the last of
         * @return the last order (of the provided type) recorded
         */
        Order GetLastOrder(OrderType orderType);

        /**
         * @return the last entry order recorded
         */
        Order GetLastEntry();

        /**
         * @return the last exit order recorded
         */
        Order GetLastExit();
    }
}
