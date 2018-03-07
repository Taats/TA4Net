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
namespace TA4Net
{
    using System;
    using TA4Net.Extensions;
    using TA4Net.Interfaces;

    /**
     * Base implementation of a {@link Bar}.
     * <p></p>
     */
    public class BaseBar : IBar
    {
        /** Time period (e.g. 1 day, 15 Min, etc.) of the bar */
        private TimeSpan _timePeriod;
        /** End time of the bar */
        private DateTime _endTime;
        /** Begin time of the bar */
        private DateTime _beginTime;
        /** Open price of the period */
        private decimal? _openPrice;
        /** Close price of the period */
        private decimal _closePrice;
        /** Max price of the period */
        private decimal? _maxPrice;
        /** Min price of the period */
        private decimal? _minPrice;
        /** Traded amount during the period */
        private decimal _amount = Decimals.Zero;
        /** Volume of the period */
        private decimal _volume = Decimals.Zero;
        /** Trade count */
        private int _trades = 0;

        /**
         * Constructor.
         * @param timePeriod the time period
         * @param endTime the end time of the bar period
         */
        public BaseBar(TimeSpan timePeriod, DateTime endTime)
        {
            checkTimeArguments(timePeriod, endTime);
            _timePeriod = timePeriod;
            _endTime = endTime;
            _beginTime = endTime.Subtract(timePeriod);
        }

        /**
         * Constructor.
         * @param endTime the end time of the bar period
         * @param openPrice the open price of the bar period
         * @param highPrice the highest price of the bar period
         * @param lowPrice the lowest price of the bar period
         * @param closePrice the close price of the bar period
         * @param volume the volume of the bar period
         */
        public BaseBar(DateTime endTime, decimal openPrice, decimal highPrice, decimal lowPrice, decimal closePrice, decimal volume)
            : this(new TimeSpan(1, 0, 0, 0, 0), endTime, openPrice, highPrice, lowPrice, closePrice, volume)
        {
        }

        /**
         * Constructor.
         * @param timePeriod the time period
         * @param endTime the end time of the bar period
         * @param openPrice the open price of the bar period
         * @param highPrice the highest price of the bar period
         * @param lowPrice the lowest price of the bar period
         * @param closePrice the close price of the bar period
         * @param volume the volume of the bar period
         */
        public BaseBar(TimeSpan timePeriod, DateTime endTime, decimal openPrice, decimal highPrice, decimal lowPrice, decimal closePrice, decimal volume)
            : this(timePeriod, endTime, openPrice, highPrice, lowPrice, closePrice, volume, Decimals.Zero)
        {
        }

        /**
         * Constructor.
         * @param timePeriod the time period
         * @param endTime the end time of the bar period
         * @param openPrice the open price of the bar period
         * @param highPrice the highest price of the bar period
         * @param lowPrice the lowest price of the bar period
         * @param closePrice the close price of the bar period
         * @param volume the volume of the bar period
         * @param amount the amount of the bar period
         */
        public BaseBar(TimeSpan timePeriod, DateTime endTime, decimal openPrice, decimal highPrice, decimal lowPrice, decimal closePrice, decimal volume, decimal amount)
        {
            checkTimeArguments(timePeriod, endTime);
            _timePeriod = timePeriod;
            _endTime = endTime;
            _beginTime = endTime.Subtract(timePeriod);
            _openPrice = openPrice;
            _maxPrice = highPrice;
            _minPrice = lowPrice;
            _closePrice = closePrice;
            _volume = volume;
            _amount = amount;
        }

        /**
         * @return the open price of the period
         */
        public decimal OpenPrice
        {
           get { return _openPrice ?? 0; }
        }

        /**
         * @return the Min price of the period
         */
        public decimal MinPrice
        {
            get { return _minPrice ?? 0; }
        }

        /**
         * @return the Max price of the period
         */
        public decimal MaxPrice
        {
            get { return _maxPrice ?? 0; }
        }

        /**
         * @return the close price of the period
         */
        public decimal ClosePrice
        {
            get { return _closePrice; }
        }

        /**
         * @return the whole traded volume in the period
         */
        public decimal Volume
        {
            get { return _volume; }
        }

        /**
         * @return the number of trades in the period
         */
        public int NumberOfTrades
        {
            get { return _trades; }
        }

        /**
         * @return the whole traded amount of the period
         */
        public decimal Amount
        {
            get { return _amount; }
        }

        /**
         * @return the time period of the bar
         */
        public TimeSpan TimePeriod
        {
            get { return _timePeriod; }
        }

        /**
         * @return the begin timestamp of the bar period
         */
        public DateTime BeginTime
        {
            get { return _beginTime; }
        }

        /**
         * @return the end timestamp of the bar period
         */
        public DateTime EndTime
        {
            get { return _endTime; }
        }

        /**
         * Adds a trade at the end of bar period.
         * @param tradeVolume the traded volume
         * @param tradePrice the price
         */
        public void AddTrade(decimal tradeVolume, decimal tradePrice)
        {
            if (_openPrice == null)
            {
                _openPrice = tradePrice;
            }
            _closePrice = tradePrice;

            if (_maxPrice == null)
            {
                _maxPrice = tradePrice;
            }
            else
            {
                _maxPrice = _maxPrice.Value.IsLessThan(tradePrice) ? tradePrice : _maxPrice;
            }
            if (_minPrice == null)
            {
                _minPrice = tradePrice;
            }
            else
            {
                _minPrice = _minPrice.Value.IsGreaterThan(tradePrice) ? tradePrice : _minPrice;
            }
            _volume = _volume.Plus(tradeVolume);
            _amount = _amount.Plus(tradeVolume.MultipliedBy(tradePrice));
            _trades++;
        }

        public bool InPeriod(DateTime timestamp)
        {
            return timestamp != null
                    && !timestamp.IsBefore(BeginTime)
                    && timestamp.IsBefore(EndTime);
        }

        /**
         * @return true if this is a bearish bar, false otherwise
         */
        public bool IsBearish()
        {
            decimal openPrice = OpenPrice;
            decimal closePrice = ClosePrice;
            return closePrice.IsLessThan(openPrice);
        }

        /**
         * @return true if this is a bullish bar, false otherwise
         */
        public bool IsBullish()
        {
            decimal openPrice = OpenPrice;
            decimal closePrice = ClosePrice;
            return openPrice.IsLessThan(closePrice);
        }

        /**
         * @param timePeriod the time period
         * @param endTime the end time of the bar
         * @throws ArgumentException if one of the arguments is null
         */
        private void checkTimeArguments(TimeSpan timePeriod, DateTime endTime)
        {
            if (timePeriod == null)
            {
                throw new ArgumentException("Time period cannot be null");
            }
            if (endTime == null)
            {
                throw new ArgumentException("End time cannot be null");
            }
        }
    }
}
