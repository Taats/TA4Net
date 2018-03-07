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
using System.Collections.Generic;
using TA4Net.Interfaces;
using TA4Net.Indicators.pivotpoints.Types;

namespace TA4Net.Indicators.PivotPoints
{
    /**
     * DeMark Reversal Indicator.
     * <p></p>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:pivot_points">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:pivot_points</a>
     */
    public class DeMarkReversalIndicator : RecursiveCachedIndicator<decimal>
    {
        private readonly DeMarkPivotPointIndicator _pivotPointIndicator;
        private readonly DeMarkPivotLevel _level;

        /**
         * Constructor.
         * <p>
         * Calculates the DeMark reversal for the corresponding pivot level
         * @param pivotPointIndicator the {@link DeMarkPivotPointIndicator} for this reversal
         * @param level the {@link DeMarkPivotLevel} for this reversal (RESISTANT, SUPPORT)
         */
        public DeMarkReversalIndicator(DeMarkPivotPointIndicator pivotPointIndicator, DeMarkPivotLevel level)
            : base(pivotPointIndicator)
        {
            _pivotPointIndicator = pivotPointIndicator;
            _level = level;
        }


        protected override decimal Calculate(int index)
        {
            decimal x = _pivotPointIndicator.GetValue(index).MultipliedBy(4);
            decimal result;

            if (_level == DeMarkPivotLevel.SUPPORT)
            {
                result = CalculateSupport(x, index);
            }
            else
            {
                result = CalculateResistance(x, index);
            }

            return result;

        }

        private decimal CalculateResistance(decimal x, int index)
        {
            List<int> barsOfPreviousPeriod = _pivotPointIndicator.GetBarsOfPreviousPeriod(index);
            if (barsOfPreviousPeriod.isEmpty())
            {
                return Decimals.NaN;
            }
            IBar bar = TimeSeries.GetBar(barsOfPreviousPeriod[0]);
            decimal low = bar.MinPrice;
            foreach (int i in barsOfPreviousPeriod)
            {
                low = TimeSeries.GetBar(i).MinPrice.Min(low);
            }

            return x.DividedBy(Decimals.TWO).Minus(low);
        }

        private decimal CalculateSupport(decimal x, int index)
        {
            List<int> barsOfPreviousPeriod = _pivotPointIndicator.GetBarsOfPreviousPeriod(index);
            if (barsOfPreviousPeriod.isEmpty())
            {
                return Decimals.NaN;
            }
            IBar bar = TimeSeries.GetBar(barsOfPreviousPeriod[0]);
            decimal high = bar.MaxPrice;
            foreach (int i in barsOfPreviousPeriod)
            {
                high = TimeSeries.GetBar(i).MaxPrice.Max(high);
            }

            return x.DividedBy(Decimals.TWO).Minus(high);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, Level: {_level}, PivorPointIndicator: {_pivotPointIndicator.GetConfiguration()}";
        }
    }
}
