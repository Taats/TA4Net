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

namespace TA4Net.Indicators.PivotPoints
{
    /**
     * Pivot Reversal Indicator.
     * <p></p>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:pivot_points">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:pivot_points</a>
     */
    public class StandardReversalIndicator : RecursiveCachedIndicator<decimal>
    {

        private readonly PivotPointIndicator _pivotPointIndicator;
        private readonly PivotLevel _level;

        /**
         * Constructor.
         * <p>
         * Calculates the (standard) reversal for the corresponding pivot level
         * @param pivotPointIndicator the {@link PivotPointIndicator} for this reversal
         * @param level the {@link PivotLevel} for this reversal
         */
        public StandardReversalIndicator(PivotPointIndicator pivotPointIndicator, PivotLevel level)
            : base(pivotPointIndicator)
        {
            _pivotPointIndicator = pivotPointIndicator;
            _level = level;
        }


        protected override decimal Calculate(int index)
        {
            List<int> barsOfPreviousPeriod = _pivotPointIndicator.GetBarsOfPreviousPeriod(index);
            if (barsOfPreviousPeriod.isEmpty())
            {
                return Decimals.NaN;
            }
            switch (_level)
            {
                case PivotLevel.RESISTANCE_3:
                    return CalculateR3(barsOfPreviousPeriod, index);
                case PivotLevel.RESISTANCE_2:
                    return CalculateR2(barsOfPreviousPeriod, index);
                case PivotLevel.RESISTANCE_1:
                    return CalculateR1(barsOfPreviousPeriod, index);
                case PivotLevel.SUPPORT_1:
                    return CalculateS1(barsOfPreviousPeriod, index);
                case PivotLevel.SUPPORT_2:
                    return CalculateS2(barsOfPreviousPeriod, index);
                case PivotLevel.SUPPORT_3:
                    return CalculateS3(barsOfPreviousPeriod, index);
                default: return Decimals.NaN;
            }

        }

        private decimal CalculateR3(List<int> barsOfPreviousPeriod, int index)
        {
            IBar bar = TimeSeries.GetBar(barsOfPreviousPeriod[0]);
            decimal low = bar.MinPrice;
            decimal high = bar.MaxPrice;
            foreach (int i in barsOfPreviousPeriod)
            {
                low = (TimeSeries.GetBar(i).MinPrice).Min(low);
                high = (TimeSeries.GetBar(i).MaxPrice).Max(high);
            }
            return high.Plus(Decimals.TWO.MultipliedBy((_pivotPointIndicator.GetValue(index).Minus(low))));
        }

        private decimal CalculateR2(List<int> barsOfPreviousPeriod, int index)
        {
            IBar bar = TimeSeries.GetBar(barsOfPreviousPeriod[0]);
            decimal low = bar.MinPrice;
            decimal high = bar.MaxPrice;
            foreach (int i in barsOfPreviousPeriod)
            {
                low = (TimeSeries.GetBar(i).MinPrice).Min(low);
                high = (TimeSeries.GetBar(i).MaxPrice).Max(high);
            }
            return _pivotPointIndicator.GetValue(index).Plus((high.Minus(low)));
        }

        private decimal CalculateR1(List<int> barsOfPreviousPeriod, int index)
        {
            decimal low = TimeSeries.GetBar(barsOfPreviousPeriod[0]).MinPrice;
            foreach (int i in barsOfPreviousPeriod)
            {
                low = (TimeSeries.GetBar(i).MinPrice).Min(low);
            }
            return Decimals.TWO.MultipliedBy(_pivotPointIndicator.GetValue(index)).Minus(low);
        }

        private decimal CalculateS1(List<int> barsOfPreviousPeriod, int index)
        {
            decimal high = TimeSeries.GetBar(barsOfPreviousPeriod[0]).MaxPrice;
            foreach (int i in barsOfPreviousPeriod)
            {
                high = (TimeSeries.GetBar(i).MaxPrice).Max(high);
            }
            return Decimals.TWO.MultipliedBy(_pivotPointIndicator.GetValue(index)).Minus(high);
        }

        private decimal CalculateS2(List<int> barsOfPreviousPeriod, int index)
        {
            IBar bar = TimeSeries.GetBar(barsOfPreviousPeriod[0]);
            decimal high = bar.MaxPrice;
            decimal low = bar.MinPrice;
            foreach (int i in barsOfPreviousPeriod)
            {
                high = (TimeSeries.GetBar(i).MaxPrice).Max(high);
                low = (TimeSeries.GetBar(i).MinPrice).Min(low);
            }
            return _pivotPointIndicator.GetValue(index).Minus((high.Minus(low)));
        }

        private decimal CalculateS3(List<int> barsOfPreviousPeriod, int index)
        {
            IBar bar = TimeSeries.GetBar(barsOfPreviousPeriod[0]);
            decimal high = bar.MaxPrice;
            decimal low = bar.MinPrice;
            foreach (int i in barsOfPreviousPeriod)
            {
                high = (TimeSeries.GetBar(i).MaxPrice).Max(high);
                low = (TimeSeries.GetBar(i).MinPrice).Min(low);
            }
            return low.Minus(Decimals.TWO.MultipliedBy((high.Minus(_pivotPointIndicator.GetValue(index)))));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, Level: {_level}, PivotPointIndicator: {_pivotPointIndicator.GetConfiguration()}";
        }
    }
}
