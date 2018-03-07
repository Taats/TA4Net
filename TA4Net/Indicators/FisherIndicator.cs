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
using TA4Net.Indicators.Helpers;
using TA4Net.Interfaces;

namespace TA4Net.Indicators
{

    /**
     * The Fisher Indicator.
     * 
     * <p/>
     * @apiNote Minimal deviations in last decimal places possible. During the calculations this indicator converts {@link Decimal Decimal/BigDecimal} to to {@link Double double}
     * see http://www.tradingsystemlab.com/files/The%20Fisher%20Transform.pdf
     */
    public class FisherIndicator : RecursiveCachedIndicator<decimal>
    {
        class IntermediateIndicator : RecursiveCachedIndicator<decimal>
        {
            private IIndicator<decimal> _indicator;
            private IIndicator<decimal> _periodHigh;
            private IIndicator<decimal> _periodLow;
            private readonly decimal _alpha;
            private readonly decimal _beta;
            private readonly decimal _densityFactor;

            public IntermediateIndicator(
                IIndicator<decimal> indicator, 
                int timeFrame,
                decimal alpha, decimal beta, decimal densityFactor, 
                bool isPriceIndicator)
                : base(indicator)
            {
                _alpha = alpha;
                _beta = beta;
                _densityFactor = densityFactor;
                _indicator = indicator;
                _periodHigh = new HighestValueIndicator(isPriceIndicator ? new MaxPriceIndicator(_indicator.TimeSeries) : _indicator, timeFrame);
                _periodLow = new LowestValueIndicator(isPriceIndicator ? new MinPriceIndicator(_indicator.TimeSeries) : _indicator, timeFrame);
            }

            protected override decimal Calculate(int index)
            {
                if (index <= 0)
                {
                    return Decimals.ZERO;
                }

                // Value = (alpha * 2 * ((ref - MinL) / (MaxH - MinL) - 0.5) + beta * priorValue) / densityFactor
                decimal currentRef = _indicator.GetValue(index);
                decimal minL = _periodLow.GetValue(index);
                decimal maxH = _periodHigh.GetValue(index);
                decimal term1 = currentRef.Minus(minL).DividedBy(maxH.Minus(minL)).Minus(ZERO_DOT_FIVE);
                decimal term2 = _alpha.MultipliedBy(Decimals.TWO).MultipliedBy(term1);
                decimal term3 = term2.Plus(_beta.MultipliedBy(GetValue(index - 1)));
                return term3.DividedBy(_densityFactor);
            }

            public override string GetConfiguration()
            {
                return $" {GetType()}, Alpha: {_alpha}, Beta: {_beta}, Density: {_densityFactor}, Indicator: {_indicator}, PeriodHighIndicator: {_periodHigh.GetConfiguration()}, PeriodLowIndicator: {_periodLow.GetConfiguration()}";
            }
        }

        private static decimal ZERO_DOT_FIVE = 0.5M;
        private static decimal VALUE_MAX = 0.999M;
        private static decimal VALUE_MIN = -0.999M;

        private IIndicator<decimal> _indicator;
        private IIndicator<decimal> _intermediateValue;
        private decimal _densityFactor;
        private decimal _gamma;
        private decimal _delta;

        /**
         * Constructor.
         *
         * @param series the series
         */
        public FisherIndicator(ITimeSeries series)
            : this(new MedianPriceIndicator(series), 10)
        {
        }

        /**
         * Constructor (with alpha 0.33, beta 0.67, gamma 0.5, delta 0.5).
         *
         * @param price the price indicator (usually {@link MedianPriceIndicator})
         * @param timeFrame the time frame (usually 10)
         */
        public FisherIndicator(IIndicator<decimal> price, int timeFrame)
            : this(price, timeFrame, 0.33M, 0.67M, ZERO_DOT_FIVE, ZERO_DOT_FIVE, Decimals.ONE, true)
        {
        }

        /**
         * Constructor (with gamma 0.5, delta 0.5).
         * 
         * @param price the price indicator (usually {@link MedianPriceIndicator})
         * @param timeFrame the time frame (usually 10)
         * @param alpha the alpha (usually 0.33 or 0.5)
         * @param beta the beta (usually 0.67 0.5 or)
         */
        public FisherIndicator(IIndicator<decimal> price, int timeFrame, double alpha, double beta)
           : this(price, timeFrame, (decimal)alpha, (decimal)beta, ZERO_DOT_FIVE, ZERO_DOT_FIVE, Decimals.ONE, true)
        {
        }

        /**
         * Constructor.
         * 
         * @param price the price indicator (usually {@link MedianPriceIndicator})
         * @param timeFrame the time frame (usually 10)
         * @param alpha the alpha (usually 0.33 or 0.5)
         * @param beta the beta (usually 0.67 or 0.5)
         * @param gamma the gamma (usually 0.25 or 0.5)
         * @param delta the delta (usually 0.5)
         */
        public FisherIndicator(IIndicator<decimal> price, int timeFrame, double alpha, double beta, double gamma, double delta)
            : this(price, timeFrame, (decimal)alpha, (decimal)beta, (decimal)gamma, (decimal)delta, Decimals.ONE, true)
        {
        }

        /**
         * Constructor (with alpha 0.33, beta 0.67, gamma 0.5, delta 0.5).
         * 
         * @param ref the indicator
         * @param timeFrame the time frame (usually 10)
         * @param isPriceIndicator use true, if "ref" is a price indicator
         */
        public FisherIndicator(IIndicator<decimal> indicator, int timeFrame, bool isPriceIndicator)
            : this(indicator, timeFrame, 0.33M, 0.67M, ZERO_DOT_FIVE, ZERO_DOT_FIVE, Decimals.ONE, isPriceIndicator)
        {
        }

        /**
         * Constructor (with alpha 0.33, beta 0.67, gamma 0.5, delta 0.5).
         * 
         * @param ref the indicator
         * @param timeFrame the time frame (usually 10)
         * @param densityFactor the density factor (usually 1.0)
         * @param isPriceIndicator use true, if "ref" is a price indicator
         */
        public FisherIndicator(IIndicator<decimal> indicator, int timeFrame, double densityFactor, bool isPriceIndicator)
            : this(indicator, timeFrame, 0.33M, 0.67M, ZERO_DOT_FIVE, ZERO_DOT_FIVE, (decimal)densityFactor, isPriceIndicator)
        {
        }

        /**
         * Constructor.
         * 
         * @param ref the indicator
         * @param timeFrame the time frame (usually 10)
         * @param alpha the alpha (usually 0.33 or 0.5)
         * @param beta the beta (usually 0.67 or 0.5)
         * @param gamma the gamma (usually 0.25 or 0.5)
         * @param delta the delta (usually 0.5)
         * @param densityFactor the density factor (usually 1.0)
         * @param isPriceIndicator use true, if "ref" is a price indicator
         */
        public FisherIndicator(IIndicator<decimal> indicator, int timeFrame, double alpha, double beta, double gamma, double delta, double densityFactor, bool isPriceIndicator)
            : this(indicator, timeFrame, (decimal)alpha, (decimal)beta, (decimal)gamma, (decimal)delta, (decimal)densityFactor, isPriceIndicator)
        {
        }

        /**
         * Constructor
         *
         * @param ref the indicator
         * @param timeFrame the time frame (usually 10)
         * @param alpha the alpha (usually 0.33 or 0.5)
         * @param beta the beta (usually 0.67 or 0.5)
         * @param gamma the gamma (usually 0.25 or 0.5)
         * @param delta the delta (usually 0.5)
         * @param densityFactor the density factor (usually 1.0)
         * @param isPriceIndicator use true, if "ref" is a price indicator
         */
        public FisherIndicator(IIndicator<decimal> indicator, int timeFrame, decimal alpha, decimal beta, decimal gamma, decimal delta, decimal densityFactor, bool isPriceIndicator)
            : base(indicator)
        {
            this._indicator = indicator;
            this._gamma = gamma;
            this._delta = delta;

            if (densityFactor.IsNaN())
            {
                this._densityFactor = Decimals.ONE;
            }
            else
            {
                this._densityFactor = densityFactor;
            }


            _intermediateValue = new IntermediateIndicator(indicator, timeFrame, alpha, beta, densityFactor, isPriceIndicator);

        }

        protected override decimal Calculate(int index)
        {
            if (index <= 0)
            {
                return Decimals.ZERO;
            }

            decimal value = _intermediateValue.GetValue(index);

            if (value.IsGreaterThan(VALUE_MAX))
            {
                value = VALUE_MAX;
            }
            else if (value.IsLessThan(VALUE_MIN))
            {
                value = VALUE_MIN;
            }

            // Fisher = gamma * Log((1 + Value) / (1 - Value)) + delta * priorFisher
            decimal term1 = (Decimals.ONE.Plus(value).DividedBy(Decimals.ONE.Minus(value))).Log();
            decimal term2 = GetValue(index - 1);
            return _gamma.MultipliedBy(term1).Plus(_delta.MultipliedBy(term2));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, Delta: {_delta}, DensityFactor: {_densityFactor}, Gamma: {_gamma}, Indicator: {_indicator}, IntermediateValueIndicator: {_intermediateValue}";
        }
    }
}
