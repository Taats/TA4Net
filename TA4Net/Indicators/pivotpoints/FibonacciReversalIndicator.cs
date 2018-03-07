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
    public class FibonacciFactor
    {
        public static FibonacciFactor Factor1 { get; }
        public static FibonacciFactor Factor2 { get; }
        public static FibonacciFactor Factor3 { get; }

        public decimal Factor { get; }

        static FibonacciFactor()
        {
            Factor1 = new FibonacciFactor(0.382M);
            Factor2 = new FibonacciFactor(0.618M);
            Factor3 = new FibonacciFactor(1M);
        }

        private FibonacciFactor(decimal factor)
        {
            Factor = factor;
        }
    }

    /**
     * Fibonacci Reversal Indicator.
     * <p></p>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:pivot_points">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:pivot_points</a>
     */
    public class FibonacciReversalIndicator : RecursiveCachedIndicator<decimal>
    {

        private readonly PivotPointIndicator _pivotPointIndicator;
        private readonly FibReversalType _fibReversalType;
        private readonly decimal _fibonacciFactor;

        /**Constructor.
         * <p>
         * Calculates a (fibonacci) reversal
         * @param pivotPointIndicator the {@link PivotPointIndicator} for this reversal
         * @param fibonacciFactor the fibonacci factor for this reversal
         * @param fibReversalTyp the FibonacciReversalIndicator.FibReversalTyp of the reversal (SUPPORT, RESISTANCE)
         */
        public FibonacciReversalIndicator(PivotPointIndicator pivotPointIndicator, decimal fibonacciFactor, FibReversalType fibReversalType)
            : base(pivotPointIndicator)
        {
            _pivotPointIndicator = pivotPointIndicator;
            _fibonacciFactor = fibonacciFactor;
            _fibReversalType = fibReversalType;
        }

        /**Constructor.
         * <p>
         * Calculates a (fibonacci) reversal
         * @param pivotPointIndicator the {@link PivotPointIndicator} for this reversal
         * @param fibonacciFactor the {@link FibonacciFactor} factor for this reversal
         * @param fibReversalTyp the FibonacciReversalIndicator.FibReversalTyp of the reversal (SUPPORT, RESISTANCE)
         */
        public FibonacciReversalIndicator(PivotPointIndicator pivotPointIndicator, FibonacciFactor fibonacciFactor, FibReversalType fibReversalType)
            : this(pivotPointIndicator, fibonacciFactor.Factor, fibReversalType)
        {
        }


        protected override decimal Calculate(int index)
        {
            List<int> barsOfPreviousPeriod = _pivotPointIndicator.GetBarsOfPreviousPeriod(index);
            if (barsOfPreviousPeriod.isEmpty())
                return Decimals.NaN;
            IBar bar = TimeSeries.GetBar(barsOfPreviousPeriod[0]);
            decimal high = bar.MaxPrice;
            decimal low = bar.MinPrice;
            foreach (int i in barsOfPreviousPeriod)
            {
                high = (TimeSeries.GetBar(i).MaxPrice).Max(high);
                low = (TimeSeries.GetBar(i).MinPrice).Min(low);
            }

            if (_fibReversalType == FibReversalType.RESISTANCE)
            {
                return _pivotPointIndicator.GetValue(index).Plus(_fibonacciFactor.MultipliedBy(high.Minus(low)));
            }
            return _pivotPointIndicator.GetValue(index).Minus(_fibonacciFactor.MultipliedBy(high.Minus(low)));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, FibonacciFactor: {_fibonacciFactor}, FibonacciReversalType: {_fibReversalType}, PivotPointIndicator: {_pivotPointIndicator.GetConfiguration()}";
        }
    }
}
