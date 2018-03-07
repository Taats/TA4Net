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
     * Buy and hold criterion.
     * <p></p>
     * @see <a href="http://en.wikipedia.org/wiki/Buy_and_hold">http://en.wikipedia.org/wiki/Buy_and_hold</a>
     */
    public class BuyAndHoldCriterion : AbstractAnalysisCriterion
    {
        public override decimal Calculate(ITimeSeries series, ITradingRecord tradingRecord)
        {
            return series.GetBar(series.GetEndIndex()).ClosePrice.DividedBy(series.GetBar(series.GetBeginIndex()).ClosePrice);
        }


        public override decimal Calculate(ITimeSeries series, Trade trade)
        {
            int entryIndex = trade.GetEntry().getIndex();
            int exitIndex = trade.GetExit().getIndex();

            if (trade.GetEntry().isBuy())
            {
                return series.GetBar(exitIndex).ClosePrice.DividedBy(series.GetBar(entryIndex).ClosePrice);
            }
            else
            {
                return series.GetBar(entryIndex).ClosePrice.DividedBy(series.GetBar(exitIndex).ClosePrice);
            }
        }


        public override bool BetterThan(decimal criterionValue1, decimal criterionValue2)
        {
            return criterionValue1 > criterionValue2;
        }

        public override string ToString()
        {
            return "Buy And Hold";
        }
    }
}
