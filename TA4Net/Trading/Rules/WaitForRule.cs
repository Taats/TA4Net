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

namespace TA4Net.Trading.Rules
{
    /**
     * A {@link org.ta4j.core.Rule} which waits for a number of {@link Bar} after an order.
     * <p></p>
     * Satisfied after a fixed number of bars since the last order.
     */
    public class WaitForRule : AbstractRule
    {

        /** The type of the order since we have to wait for */
        private readonly OrderType _orderType;

        /** The number of bars to wait for */
        private readonly int _numberOfBars;

        /**
         * Constructor.
         * @param orderType the type of the order since we have to wait for
         * @param numberOfBars the number of bars to wait for
         */
        public WaitForRule(OrderType orderType, int numberOfBars)
        {
            _orderType = orderType;
            _numberOfBars = numberOfBars;
        }
        
        public override bool IsSatisfied(int index, ITradingRecord tradingRecord)
        {
            bool satisfied = false;
            // No trading history, no need to wait
            if (tradingRecord != null)
            {
                Order lastOrder = tradingRecord.GetLastOrder(_orderType);
                if (lastOrder != null)
                {
                    int currentNumberOfBars = index - lastOrder.getIndex();
                    satisfied = currentNumberOfBars >= _numberOfBars;
                }
            }

            traceIsSatisfied(index, satisfied);
            return satisfied;
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, OrderType: {_orderType}, NumberOfBars: {_numberOfBars}";
        }
    }
}
