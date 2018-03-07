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
using System;

namespace TA4Net.Interfaces {

    /**
     * End bar of a time period.
     * <p></p>
     * Bar object is aggregated open/high/low/close/volume/etc. data over a time period.
     */
    public interface IBar {

        /**
         * @return the open price of the period
         */
        decimal OpenPrice { get; }

        /**
         * @return the Min price of the period
         */
        decimal MinPrice { get; }

        /**
         * @return the Max price of the period
         */
        decimal MaxPrice { get; }

        /**
         * @return the close price of the period
         */
        decimal ClosePrice { get; }

        /**
         * @return the whole traded volume in the period
         */
        decimal Volume { get; }

        /**
         * @return the number of trades in the period
         */
        int NumberOfTrades { get; }

        /**
         * @return the whole traded amount of the period
         */
        decimal Amount { get; }

        /**
         * @return the time period of the bar
         */
        TimeSpan TimePeriod { get; }

        /**
         * @return the begin timestamp of the bar period
         */
        DateTime BeginTime { get; }

        /**
         * @return the end timestamp of the bar period
         */
        DateTime EndTime { get; }

        /**
         * @param timestamp a timestamp
         * @return true if the provided timestamp is between the begin time and the end time of the current period, false otherwise
         */
        bool InPeriod(DateTime timestamp);

        /**
         * @return true if this is a bearish bar, false otherwise
         */
        bool IsBearish();

        /**
         * @return true if this is a bullish bar, false otherwise
         */
        bool IsBullish();

        /**
         * Adds a trade at the end of bar period.
         * @param tradeVolume the traded volume
         * @param tradePrice the price
         */
        void AddTrade(decimal tradeVolume, decimal tradePrice);
    }
}
