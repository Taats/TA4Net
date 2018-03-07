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
     * Versus "buy and hold" criterion.
     * <p></p>
     * Compares the value of a provided {@link AnalysisCriterion criterion} with the value of a {@link BuyAndHoldCriterion "buy and hold" criterion}.
     */
    public class VersusBuyAndHoldCriterion : AbstractAnalysisCriterion
    {
        private readonly IAnalysisCriterion _criterion;

        /**
         * Constructor.
         * @param criterion an analysis criterion to be compared
         */
        public VersusBuyAndHoldCriterion(IAnalysisCriterion criterion)
        {
            _criterion = criterion;
        }


        public override decimal Calculate(ITimeSeries series, ITradingRecord tradingRecord)
        {
            ITradingRecord fakeRecord = new BaseTradingRecord();
            fakeRecord.Enter(series.GetBeginIndex());
            fakeRecord.Exit(series.GetEndIndex());

            return _criterion.Calculate(series, tradingRecord) / _criterion.Calculate(series, fakeRecord);
        }


        public override decimal Calculate(ITimeSeries series, Trade trade)
        {
            ITradingRecord fakeRecord = new BaseTradingRecord();
            fakeRecord.Enter(series.GetBeginIndex());
            fakeRecord.Exit(series.GetEndIndex());

            return _criterion.Calculate(series, trade) / _criterion.Calculate(series, fakeRecord);
        }


        public override bool BetterThan(decimal criterionValue1, decimal criterionValue2)
        {
            return criterionValue1 > criterionValue2;
        }


        public override string ToString()
        {
            return base.ToString() + " (" + _criterion + ')';
        }
    }
}
