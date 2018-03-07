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
using System.Collections.Generic;
using TA4Net.Interfaces;
using TA4Net.Trading.Rules.Types;

namespace TA4Net
{
    /**
     * Base implementation of a {@link TradingRecord}.
     * <p></p>
     */
    public class BaseTradingRecord : ITradingRecord
    {
        /** The recorded orders */
        private List<Order> orders = new List<Order>();

        /** The recorded BUY orders */
        private List<Order> buyOrders = new List<Order>();

        /** The recorded SELL orders */
        private List<Order> sellOrders = new List<Order>();

        /** The recorded entry orders */
        private List<Order> entryOrders = new List<Order>();

        /** The recorded exit orders */
        private List<Order> exitOrders = new List<Order>();

        /** The recorded trades */
        private List<Trade> trades = new List<Trade>();

        /** The entry type (BUY or SELL) in the trading session */
        private OrderType startingType;

        /** The current non-closed trade (there's always one) */
        private Trade currentTrade;

        /**
         * Constructor.
         */
        public BaseTradingRecord()
            : this(OrderType.BUY)
        {
        }

        /**
         * Constructor.
         * @param entryOrderType the {@link Order.OrderType order type} of entries in the trading session
         */
        public BaseTradingRecord(OrderType entryOrderType)
        {
            startingType = entryOrderType;
            currentTrade = new Trade(entryOrderType);
        }

        /**
         * Constructor.
         * @param orders the orders to be recorded (cannot be empty)
         */
        public BaseTradingRecord(params Order[] orders)
            : this(orders[0].GetOrderType())
        {
            foreach (Order o in orders)
            {
                bool newOrderWillBeAnEntry = currentTrade.IsNew();
                if (newOrderWillBeAnEntry && o.GetOrderType() != startingType)
                {
                    // Special case for entry/exit types reversal
                    // E.g.: BUY, SELL,
                    //    BUY, SELL,
                    //    SELL, BUY,
                    //    BUY, SELL
                    currentTrade = new Trade(o.GetOrderType());
                }
                Order newOrder = currentTrade.Operate(o.getIndex(), o.getPrice(), o.Amount);
                RecordOrder(newOrder, newOrderWillBeAnEntry);
            }
        }


        public Trade GetCurrentTrade()
        {
            return currentTrade;
        }


        public void Operate(int index, decimal price, decimal amount)
        {
            if (currentTrade.IsClosed())
            {
                // Current trade closed, should not occur
                throw new NotSupportedException("Current trade should not be closed");
            }
            bool newOrderWillBeAnEntry = currentTrade.IsNew();
            Order newOrder = currentTrade.Operate(index, price, amount);
            RecordOrder(newOrder, newOrderWillBeAnEntry);
        }


        public bool Enter(int index, decimal price, decimal amount)
        {
            if (currentTrade.IsNew())
            {
                Operate(index, price, amount);
                return true;
            }
            return false;
        }


        public bool Exit(int index, decimal price, decimal amount)
        {
            if (currentTrade.IsOpened())
            {
                Operate(index, price, amount);
                return true;
            }
            return false;
        }


        public IReadOnlyList<Trade> Trades
        {
            get { return trades; }
        }


        public Order GetLastOrder()
        {
            if (!orders.isEmpty())
            {
                return orders[orders.Count - 1];
            }
            return null;
        }


        public Order GetLastOrder(OrderType orderType)
        {
            if (OrderType.BUY.Equals(orderType) && !buyOrders.isEmpty())
            {
                return buyOrders[buyOrders.Count - 1];
            }
            else if (OrderType.SELL.Equals(orderType) && !sellOrders.isEmpty())
            {
                return sellOrders[sellOrders.Count - 1];
            }
            return null;
        }


        public Order GetLastEntry()
        {
            if (!entryOrders.isEmpty())
            {
                return entryOrders[entryOrders.Count - 1];
            }
            return null;
        }


        public Order GetLastExit()
        {
            if (!exitOrders.isEmpty())
            {
                return exitOrders[exitOrders.Count - 1];
            }
            return null;
        }

        /**
         * Operates an order in the trading record.
         * @param index the index to operate the order
         */
        public void Operate(int index)
        {
            Operate(index, Decimals.NaN, Decimals.NaN);
        }

        /**
         * Operates an entry order in the trading record.
         * @param index the index to operate the entry
         * @return true if the entry has been operated, false otherwise
         */
        public bool Enter(int index)
        {
            return Enter(index, Decimals.NaN, Decimals.NaN);
        }

        /**
         * Operates an exit order in the trading record.
         * @param index the index to operate the exit
         * @return true if the exit has been operated, false otherwise
         */
        public bool Exit(int index)
        {
            return Exit(index, Decimals.NaN, Decimals.NaN);
        }

        /**
         * @return true if no trade is open, false otherwise
         */
        public bool IsClosed()
        {
            return !GetCurrentTrade().IsOpened();
        }

        /**
         * @return the number of recorded trades
         */
        public int GetTradeCount()
        {
            return Trades.Count;
        }

        /**
         * @return the last trade recorded
         */
        public Trade GetLastTrade()
        {
            var trades = Trades;
            if (!trades.isEmpty())
            {
                return trades[trades.Count - 1];
            }
            return null;
        }

        /**
         * Records an order and the corresponding trade (if closed).
         * @param order the order to be recorded
         * @param isEntry true if the order is an entry, false otherwise (exit)
         */
        private void RecordOrder(Order order, bool isEntry)
        {
            if (order == null)
            {
                throw new ArgumentException("Order should not be null");
            }

            // Storing the new order in entries/exits lists
            if (isEntry)
            {
                entryOrders.Add(order);
            }
            else
            {
                exitOrders.Add(order);
            }

            // Storing the new order in orders list
            orders.Add(order);
            if (OrderType.BUY.Equals(order.GetOrderType()))
            {
                // Storing the new order in buy orders list
                buyOrders.Add(order);
            }
            else if (OrderType.SELL.Equals(order.GetOrderType()))
            {
                // Storing the new order in sell orders list
                sellOrders.Add(order);
            }

            // Storing the trade if closed
            if (currentTrade.IsClosed())
            {
                trades.Add(currentTrade);
                currentTrade = new Trade(startingType);
            }
        }
    }
}