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
using TA4Net.Indicators.Statistics.Types;

namespace TA4Net.Indicators.Statistics
{
    /**
     * Simple linear regression indicator.
     * <p></p>
     * A moving (i.e. over the time frame) simple linear regression (least squares).
     * y = slope * x + intercept
     * See also: http://introcs.cs.princeton.edu/java/97data/LinearRegression.java.html
     */
    public class SimpleLinearRegressionIndicator : CachedIndicator<decimal>
    {
        private IIndicator<decimal> _indicator;
        private int _timeFrame;
        private decimal _slope;
        private decimal _intercept;
        private SimpleLinearRegressionType _type;

        /**
         * Constructor for the y-values of the formula (y = slope * x + intercept).
         * 
         * @param indicator the indicator for the x-values of the formula.
         * @param timeFrame the time frame
         */
        public SimpleLinearRegressionIndicator(IIndicator<decimal> indicator, int timeFrame)
            : this(indicator, timeFrame, SimpleLinearRegressionType.y)
        {
        }

        /**
         * Constructor.
         * 
         * @param indicator the indicator for the x-values of the formula.
         * @param timeFrame the time frame
         * @param type the type of the outcome value (y, slope, intercept)
         */
        public SimpleLinearRegressionIndicator(IIndicator<decimal> indicator, int timeFrame, SimpleLinearRegressionType type)
            : base(indicator)
        {
            _indicator = indicator;
            _timeFrame = timeFrame;
            _type = type;
        }

        protected override decimal Calculate(int index)
        {
            int startIndex = Math.Max(0, index - _timeFrame + 1);
            if (index - startIndex + 1 < 2)
            {
                // Not enough observations to compute a regression line
                return Decimals.NaN;
            }
            CalculateRegressionLine(startIndex, index);

            if (_type == SimpleLinearRegressionType.slope)
            {
                return _slope;
            }

            if (_type == SimpleLinearRegressionType.intercept)
            {
                return _intercept;
            }

            return _slope.MultipliedBy(index).Plus(_intercept);
        }

        /**
         * Calculates the regression line.
         * @param startIndex the start index (inclusive) in the time series
         * @param endIndex the end index (inclusive) in the time series
         */
        private void CalculateRegressionLine(int startIndex, int endIndex)
        {
            // First pass: compute xBar and yBar
            decimal sumX = Decimals.Zero;
            decimal sumY = Decimals.Zero;
            for (int i = startIndex; i <= endIndex; i++) {
                sumX = sumX.Plus(i);
                sumY = sumY.Plus(_indicator.GetValue(i));
            }
            decimal nbObservations = endIndex - startIndex + 1;
            decimal xBar = sumX.DividedBy(nbObservations);
            decimal yBar = sumY.DividedBy(nbObservations);

            // Second pass: compute slope and intercept
            decimal xxBar = Decimals.Zero;
            decimal xyBar = Decimals.Zero;
            for (int i = startIndex; i <= endIndex; i++) {
                decimal dX =  - xBar;
                decimal dY = _indicator.GetValue(i).Minus(yBar);
                xxBar = xxBar.Plus(dX.MultipliedBy(dX));
                xyBar = xyBar.Plus(dX.MultipliedBy(dY));
            }

            _slope = xyBar.DividedBy(xxBar);
            _intercept = yBar.Minus(_slope.MultipliedBy(xBar));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, Intercept: {_intercept}, Slope: {_slope}, Type: {_type}, Indicator: {_indicator.GetConfiguration()}";
        }
    }
}
