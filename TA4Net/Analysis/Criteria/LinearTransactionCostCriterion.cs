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

namespace TA4Net.Analysis.Criteria
{
    /**
     * A linear transaction cost criterion.
     * <p></p>
     * That criterion Calculate the transaction cost according to an initial traded amount
     * and a linear function defined by a and b (a * x + b).
     */
    public class LinearTransactionCostCriterion : AbstractAnalysisCriterion
    {

        private decimal _initialAmount;
        private decimal _a;
        private decimal _b;

        private TotalProfitCriterion _profit;

        /**
         * Constructor.
         * (a * x)
         * @param initialAmount the initially traded amount
         * @param a the a coefficient (e.g. 0.005 for 0.5% per {@link Order order})
         */
        public LinearTransactionCostCriterion(decimal initialAmount, decimal a)
            : this(initialAmount, a, 0)
        {
        }

        /**
         * Constructor.
         * (a * x + b)
         * @param initialAmount the initially traded amount
         * @param a the a coefficient (e.g. 0.005 for 0.5% per {@link Order order})
         * @param b the b constant (e.g. 0.2 for $0.2 per {@link Order order})
         */
        public LinearTransactionCostCriterion(decimal initialAmount, decimal a, decimal b)
        {
            _initialAmount = initialAmount;
            _a = a;
            _b = b;
            _profit = new TotalProfitCriterion();
        }


        public override decimal Calculate(ITimeSeries series, Trade trade)
        {
            return getTradeCost(series, trade, _initialAmount);
        }


        public override decimal Calculate(ITimeSeries series, ITradingRecord tradingRecord)
        {
            decimal totalCosts = 0M;
            decimal tradedAmount = _initialAmount;

            foreach (Trade trade in tradingRecord.Trades)
            {
                decimal tradeCost = getTradeCost(series, trade, tradedAmount);
                totalCosts += tradeCost;
                // To Calculate the new traded amount:
                //    - Remove the cost of the *first* order
                //    - Multiply by the profit ratio
                //    - Remove the cost of the *second* order
                tradedAmount = (tradedAmount - getOrderCost(trade.GetEntry(), tradedAmount));
                tradedAmount *= _profit.Calculate(series, trade);
                tradedAmount -= getOrderCost(trade.GetExit(), tradedAmount);
            }

            // Special case: if the current trade is open
            Trade currentTrade = tradingRecord.GetCurrentTrade();
            if (currentTrade.IsOpened())
            {
                totalCosts += getOrderCost(currentTrade.GetEntry(), tradedAmount);
            }

            return totalCosts;
        }


        public override bool BetterThan(decimal criterionValue1, decimal criterionValue2)
        {
            return criterionValue1 < criterionValue2;
        }

        /**
         * @param order a trade order
         * @param tradedAmount the traded amount for the order
         * @return the absolute order cost
         */
        private decimal getOrderCost(Order order, decimal tradedAmount)
        {
            decimal orderCost = 0M;
            if (order != null)
            {
                return _a * tradedAmount + _b;
            }
            return orderCost;
        }

        /**
         * @param series the time series
         * @param trade a trade
         * @param initialAmount the initially traded amount for the trade
         * @return the absolute total cost of all orders in the trade
         */
        private decimal getTradeCost(ITimeSeries series, Trade trade, decimal initialAmount)
        {
            decimal totalTradeCost = 0M;
            if (trade != null)
            {
                if (trade.GetEntry() != null)
                {
                    totalTradeCost = getOrderCost(trade.GetEntry(), initialAmount);
                    if (trade.GetExit() != null)
                    {
                        // To Calculate the new traded amount:
                        //    - Remove the cost of the first order
                        //    - Multiply by the profit ratio
                        decimal newTradedAmount = (initialAmount - totalTradeCost) * _profit.Calculate(series, trade);
                        totalTradeCost += getOrderCost(trade.GetExit(), newTradedAmount);
                    }
                }
            }
            return totalTradeCost;
        }
    }
}
