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
using TA4Net.Trading.Rules.Types;

namespace TA4Net
{
    /**
     * An order.
     * <p></p>
     * The order is defined by:
     * <ul>
     *     <li>the index (in the {@link TimeSeries time series}) it is executed
     *     <li>a {@link OrderType type} (BUY or SELL)
     *     <li>a price (optional)
     *     <li>an amount to be (or that was) ordered (optional)
     * </ul>
     * A {@link Trade trade} is a pair of complementary orders.
     */
    public class Order {
        /**
         * The type of an {@link Order order}.
         * <p>
         * A BUY corresponds to a <i>BID</i> order.<p>
         * A SELL corresponds to an <i>ASK</i> order.
         */
       

        /** Type of the order */
        private OrderType _type;

        /** The index the order was executed */
        private int _index;

        /** The price for the order */
        private decimal _price;

        /** The amount to be (or that was) ordered */
        private decimal _amount;

        /**
         * Constructor.
         * @param index the index the order is executed
         * @param series the time series
         * @param type the type of the order
         */
        public Order(int index, ITimeSeries series, OrderType type) {
            _type = type;
            _index = index;
            _amount = 1;
            _price = series.GetBar(index).ClosePrice;
        }

        /**
         * Constructor.
         * @param index the index the order is executed
         * @param type the type of the order
         * @param price the price for the order
         * @param amount the amount to be (or that was) ordered
         */
        public Order(int index, OrderType type, decimal price, decimal amount) {
            _type = type;
            _index = index;
            _price = price;
            _amount = amount;
        }

        /**
         * @return the type of the order (BUY or SELL)
         */
        public OrderType GetOrderType() {
            return _type;
        }

        /**
         * @return true if this is a BUY order, false otherwise
         */
        public bool isBuy() {
            return _type == OrderType.BUY;
        }

        /**
         * @return true if this is a SELL order, false otherwise
         */
        public bool isSell() {
            return _type == OrderType.SELL;
        }

        /**
         * @return the index the order is executed
         */
        public int getIndex() {
            return _index;
        }

        /**
         * @return the price for the order
         */
        public decimal getPrice() {
            return _price;
        }

        /**
         * @return the amount to be (or that was) ordered
         */
        public decimal Amount {
            get { return _amount; }
        }


        public override int GetHashCode() {
            return
                _type.GetHashCode() ^
                _index.GetHashCode() ^
                _price.GetHashCode() ^
                _amount.GetHashCode();
        }


        public override bool Equals(object obj) {
            var other = obj as Order;
            if (other == null)
            {
                return false;
            }
            return
                other.getPrice() == _price &&
                other.getIndex() == _index &&
                other.GetOrderType() == _type &&
                other.Amount == _amount;
        }


        public override string ToString() {
            return "Order{" + "type=" + _type + ", index=" + _index + ", price=" + _price + ", amount=" + _amount + '}';
        }

        /**
         * @param index the index the order is executed
         * @return a BUY order
         */
        public static Order buyAt(int index, ITimeSeries series) {
            return new Order(index, series, OrderType.BUY);
        }

        /**
         * @param index the index the order is executed
         * @param price the price for the order
         * @param amount the amount to be (or that was) bought
         * @return a BUY order
         */
        public static Order buyAt(int index, decimal price, decimal amount) {
            return new Order(index, OrderType.BUY, price, amount);
        }

        /**
         * @param index the index the order is executed
         * @return a SELL order
         */
        public static Order sellAt(int index, ITimeSeries series) {
            return new Order(index, series, OrderType.SELL);
        }

        /**
         * @param index the index the order is executed
         * @param price the price for the order
         * @param amount the amount to be (or that was) sold
         * @return a SELL order
         */
        public static Order sellAt(int index, decimal price, decimal amount) {
            return new Order(index, OrderType.SELL, price, amount);
        }
    }
}
