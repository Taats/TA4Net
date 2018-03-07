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
using TA4Net.Extensions;
using TA4Net.Interfaces;

namespace TA4Net.Analysis.Criteria
{
    /**
     * Reward risk ratio criterion.
     * <p></p>
     * (i.e. the {@link TotalProfitCriterion total profit} over the {@link MaximumDrawdownCriterion Maximum drawdown}.
     */
    public class RewardRiskRatioCriterion : AbstractAnalysisCriterion
    {

        private IAnalysisCriterion _totalProfit = new TotalProfitCriterion();
        private IAnalysisCriterion _maxDrawdown = new MaximumDrawdownCriterion();


        public override decimal Calculate(ITimeSeries series, ITradingRecord tradingRecord)
        {
            return _totalProfit.Calculate(series, tradingRecord).DividedBy(_maxDrawdown.Calculate(series, tradingRecord));
        }


        public override bool BetterThan(decimal criterionValue1, decimal criterionValue2)
        {
            return criterionValue1 > criterionValue2;
        }


        public override decimal Calculate(ITimeSeries series, Trade trade)
        {
            return _totalProfit.Calculate(series, trade) / _maxDrawdown.Calculate(series, trade);
        }

        public override string ToString()
        {
            return "Reward Risk Ratio";
        }
    }
}
