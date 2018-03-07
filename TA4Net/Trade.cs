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
using TA4Net.Trading.Rules.Types;

namespace TA4Net
{
    /**
     * Pair of two {@link Order orders}.
     * <p></p>
     * The exit order has the complement type of the entry order.<br>
     * I.e.:
     *   entry == BUY --> exit == SELL
     *   entry == SELL --> exit == BUY
     */
    public class Trade {

        /** The entry order */
        private Order _entry;

        /** The exit order */
        private Order _exit;

        /** The type of the entry order */
        private OrderType _startingType;

        /**
         * Constructor.
         */
        public Trade()
            : this(OrderType.BUY)
        {
        }

        /**
         * Constructor.
         * @param startingType the starting {@link OrderType order type} of the trade (i.e. type of the entry order)
         */
        public Trade(OrderType startingType) {
            _startingType = startingType;
        }

        /**
         * Constructor.
         * @param entry the entry {@link Order order}
         * @param exit the exit {@link Order order}
         */
        public Trade(Order entry, Order exit) {
            if (entry.GetOrderType().Equals(exit.GetOrderType())) {
                throw new ArgumentException("Both orders must have different types");
            }
            _startingType = entry.GetOrderType();
            _entry = entry;
            _exit = exit;
        }

        /**
         * @return the entry {@link Order order} of the trade
         */
        public Order GetEntry() {
            return _entry;
        }

        /**
         * @return the exit {@link Order order} of the trade
         */
        public Order GetExit() {
            return _exit;
        }


        public override bool Equals(object obj) {
            if (obj is Trade t)
            {
                return _entry.Equals(t.GetEntry()) && _exit.Equals(t.GetExit());
            }
            return false;
        }


        public override int GetHashCode() {
            return  _entry?.GetHashCode() ?? 0 ^ _exit?.GetHashCode() ?? 0;
        }

        /**
         * Operates the trade at the index-th position
         * @param index the bar index
         * @return the order
         */
        public Order Operate(int index) {
            return Operate(index, Decimals.NaN, Decimals.NaN);
        }

        /**
         * Operates the trade at the index-th position
         * @param index the bar index
         * @param price the price
         * @param amount the amount
         * @return the order
         */
        public Order Operate(int index, decimal price, decimal amount) {
            Order order = null;
            if (IsNew()) {
                order = new Order(index, _startingType, price, amount);
                _entry = order;
            } else if (IsOpened()) {
                if (index < _entry.getIndex()) {
                    throw new NotSupportedException("The index i is less than the entryOrder index");
                }
                order = new Order(index, _startingType == OrderType.BUY ? OrderType.SELL : OrderType.BUY, price, amount);
                _exit = order;
            }
            return order;
        }

        /**
         * @return true if the trade is closed, false otherwise
         */
        public bool IsClosed() {
            return (_entry != null) && (_exit != null);
        }

        /**
         * @return true if the trade is opened, false otherwise
         */
        public bool IsOpened() {
            return (_entry != null) && (_exit == null);
        }

        /**
         * @return true if the trade is new, false otherwise
         */
        public bool IsNew() {
            return (_entry == null) && (_exit == null);
        }


        public override string ToString() {
            return "Entry: " + _entry + " exit: " + _exit;
        }
    }
}
