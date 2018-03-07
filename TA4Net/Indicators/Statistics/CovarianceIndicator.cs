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

namespace TA4Net.Indicators.Statistics
{
    /**
     * Covariance indicator.
     * <p></p>
     */
    public class CovarianceIndicator : CachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _indicator1;
        private readonly IIndicator<decimal> _indicator2;
        private readonly int _timeFrame;
        private readonly SMAIndicator _sma1;
        private readonly SMAIndicator _sma2;

        /**
         * Constructor.
         * @param indicator1 the first indicator
         * @param indicator2 the second indicator
         * @param timeFrame the time frame
         */
        public CovarianceIndicator(IIndicator<decimal> indicator1, IIndicator<decimal> indicator2, int timeFrame)
            : base(indicator1)
        {
            _indicator1 = indicator1;
            _indicator2 = indicator2;
            _timeFrame = timeFrame;
            _sma1 = new SMAIndicator(indicator1, timeFrame);
            _sma2 = new SMAIndicator(indicator2, timeFrame);
        }


        protected override decimal Calculate(int index)
        {
            int startIndex = Math.Max(0, index - _timeFrame + 1);
            int numberOfObservations = index - startIndex + 1;
            decimal covariance = Decimals.Zero;
            decimal average1 = _sma1.GetValue(index);
            decimal average2 = _sma2.GetValue(index);
            for (int i = startIndex; i <= index; i++) {
                decimal mul = _indicator1.GetValue(i).Minus(average1).MultipliedBy(_indicator2.GetValue(i).Minus(average2));
                covariance = covariance.Plus(mul);
            }
            covariance = covariance.DividedBy(numberOfObservations);
            return covariance;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, Sma1Indicator: {_sma1.GetConfiguration()}, Sma2Indicator: {_sma2.GetConfiguration()}, Indicator1: {_indicator1.GetConfiguration()}, Indicator2: {_indicator2.GetConfiguration()}";
        }
    }
}
