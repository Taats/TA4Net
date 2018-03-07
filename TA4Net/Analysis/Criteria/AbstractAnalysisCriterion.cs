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
using TA4Net.Interfaces;

namespace TA4Net.Analysis.Criteria
{
    /**
     * An abstract analysis criterion.
     * <p></p>
     */
    public abstract class AbstractAnalysisCriterion : IAnalysisCriterion
    {
        /**
        * @param series a time series
        * @param trade a trade
        * @return the criterion value for the trade
        */
        public abstract decimal Calculate(ITimeSeries series, Trade trade);

        /**
         * @param series a time series
         * @param tradingRecord a trading record
         * @return the criterion value for the trades
         */
        public abstract decimal Calculate(ITimeSeries series, ITradingRecord tradingRecord);


        /**
         * @param criterionValue1 the first value
         * @param criterionValue2 the second value
         * @return true if the first value is better than (according to the criterion) the second one, false otherwise
         */
        public abstract bool BetterThan(decimal criterionValue1, decimal criterionValue2);

        /**
         * @param manager the time series manager
         * @param strategies a list of strategies
         * @return the best strategy (among the provided ones) according to the criterion
         */
        public IStrategy ChooseBest(TimeSeriesManager manager, List<IStrategy> strategies)
        {

            IStrategy bestStrategy = strategies[0];
            decimal bestCriterionValue = Calculate(manager.TimeSeries, manager.Run(bestStrategy));

            for (int i = 1; i < strategies.Count; i++) {
                IStrategy currentStrategy = strategies[i];
                decimal currentCriterionValue = Calculate(manager.TimeSeries, manager.Run(currentStrategy));

                if (BetterThan(currentCriterionValue, bestCriterionValue))
                {
                    bestStrategy = currentStrategy;
                    bestCriterionValue = currentCriterionValue;
                }
            }
            return bestStrategy;
        }
    }
}
