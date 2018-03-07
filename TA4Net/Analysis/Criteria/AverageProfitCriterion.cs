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
using System;
using TA4Net.Interfaces;

namespace TA4Net.Analysis.Criteria
{
    /**
     * Average profit criterion.
     * <p></p>
     * The {@link TotalProfitCriterion total profit} over the {@link NumberOfBarsCriterion number of bars}.
     */
    public class AverageProfitCriterion : AbstractAnalysisCriterion
    {

        private IAnalysisCriterion _totalProfit = new TotalProfitCriterion();
        private IAnalysisCriterion _numberOfBars = new NumberOfBarsCriterion();


        public override decimal Calculate(ITimeSeries series, ITradingRecord tradingRecord)
        {
            decimal bars = _numberOfBars.Calculate(series, tradingRecord);
            if (bars == 0)
            {
                return 1;
            }
            return _totalProfit.Calculate(series, tradingRecord).Pow(1M / bars);
        }


        public override decimal Calculate(ITimeSeries series, Trade trade)
        {
            decimal bars = _numberOfBars.Calculate(series, trade);
            if (bars == 0)
            {
                return 1;
            }
            return _totalProfit.Calculate(series, trade).Pow(1M / bars);
        }


        public override bool BetterThan(decimal criterionValue1, decimal criterionValue2)
        {
            return criterionValue1 > criterionValue2;
        }

        public override string ToString()
        {
            return "Average Profit";
        }
    }
}
