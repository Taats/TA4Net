
using TA4Net.Extensions;
using TA4Net.Indicators.Statistics;
using TA4Net.Trading.Rules;
using System;
using TA4Net.Interfaces;
using TA4Net.Indicators.Helpers.Types;
/**
* The MIT License (MIT)
*
* Copyright (c) 2014-2017 Marc de Verdelhan & respective authors (see AUTHORS)
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of
* this software and associated documentation files (the "Software"), to deal in
* the Software without restriction, including without limitation the rights to
* use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
* the Software, and to permit persons to whom the Software is furnished to do so,
* subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
* FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
* COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
* IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
* CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
namespace TA4Net.Indicators.Helpers
{
    /**
     * Indicator-convergence-divergence.
     */
    public class ConvergenceDivergenceIndicator : CachedIndicator<bool>
    {
        /** The actual indicator. */
        private readonly IIndicator<decimal> _indicator;

        /** The other indicator. */
        private readonly IIndicator<decimal> _other;

        /** The timeFrame. */
        private readonly int _timeFrame;

        /** The type of the convergence or divergence **/
        private readonly ConvergenceDivergenceType? _type;

        /** The type of the strict convergence or strict divergence **/
        private readonly ConvergenceDivergenceStrictType? _strictType;

        /** The Minimum strenght for convergence or divergence. **/
        private decimal _minStrenght;

        /** The Minimum slope for convergence or divergence. **/
        private decimal _minSlope;

        /**
         * Constructor. <br/>
         * <br/>
         * 
         * The <b>"MinStrenght"</b> is the Minimum required strenght for convergence or divergence
         * and must be a number between "0.1" and "1.0": <br/>
         * <br/>
         * 0.1: very weak <br/>
         * 0.8: strong (recommended) <br/>
         * 1.0: very strong <br/>
         * 
         * <br/>
         * 
         * The <b>"MinSlope"</b> is the Minimum required slope for convergence or divergence
         * and must be a number between "0.1" and "1.0": <br/>
         * <br/>
         * 0.1: very unstrict<br/>
         * 0.3: strict (recommended) <br/>
         * 1.0: very strict <br/>
         * 
         * @param ref the indicator
         * @param other the other indicator
         * @param timeFrame the time frame
         * @param type of convergence or divergence
         * @param MinStrenght the Minimum required strenght for convergence or divergence
         * @param MinSlope the Minimum required slope for convergence or divergence
         */
        public ConvergenceDivergenceIndicator(IIndicator<decimal> indicator, IIndicator<decimal> other, int timeFrame, ConvergenceDivergenceType type, decimal minStrenght, decimal minSlope)
            : base(indicator.TimeSeries)
        {
            _other = other;
            _timeFrame = timeFrame;
            _type = type;
            _strictType = null;
            _minStrenght = minStrenght.Abs();
            _minSlope = minSlope;
            _indicator = indicator;
        }

        /**
         * Constructor for strong convergence or divergence.
         * 
         * @param ref the indicator
         * @param other the other indicator
         * @param timeFrame the time frame
         * @param type of convergence or divergence
         */
        public ConvergenceDivergenceIndicator(IIndicator<decimal> indicator, IIndicator<decimal> other, int timeFrame, ConvergenceDivergenceType type)
            : base(indicator.TimeSeries)
        {
            _other = other;
            _timeFrame = timeFrame;
            _type = type;
            _strictType = null;
            _minStrenght = 0.8M.Abs();
            _minSlope = 0.3M;
            _indicator = indicator;
        }

        /**
         * Constructor for strict convergence or divergence.
         * 
         * @param ref the indicator
         * @param other the other indicator
         * @param timeFrame the time frame
         */
        public ConvergenceDivergenceIndicator(IIndicator<decimal> indicator, IIndicator<decimal> other, int timeFrame, ConvergenceDivergenceStrictType strictType)
            : base(indicator.TimeSeries)
        {
            _other = other;
            _timeFrame = timeFrame;
            _type = null;
            _strictType = strictType;
            _minStrenght = 0;
            _minSlope = 0;
            _indicator = indicator;
        }


        protected override bool Calculate(int index)
        {

            if (_minStrenght.IsZero())
            {
                return false;
            }

            if (_minStrenght.IsGreaterThan(Decimals.ONE))
            {
                _minStrenght = Decimals.ONE;
            }

            if (_type != null)
            {
                switch (_type)
                {
                    case ConvergenceDivergenceType.positiveConvergent:
                        return CalculatePositiveConvergence(index);
                    case ConvergenceDivergenceType.negativeConvergent:
                        return CalculateNegativeConvergence(index);
                    case ConvergenceDivergenceType.positiveDivergent:
                        return CalculatePositiveDivergence(index);
                    case ConvergenceDivergenceType.negativeDivergent:
                        return CalculateNegativeDivergence(index);
                }
            }

            else if (_strictType != null)
            {
                switch (_strictType)
                {
                    case ConvergenceDivergenceStrictType.positiveConvergentStrict:
                        return CalculatePositiveConvergenceStrict(index);
                    case ConvergenceDivergenceStrictType.negativeConvergentStrict:
                        return CalculateNegativeConvergenceStrict(index);
                    case ConvergenceDivergenceStrictType.positiveDivergentStrict:
                        return CalculatePositiveDivergenceStrict(index);
                    case ConvergenceDivergenceStrictType.negativeDivergentStrict:
                        return CalculateNegativeDivergenceStrict(index);
                }
            }

            return false;
        }

        /**
         * @param index the actual index
         * @return true, if strict positive convergent
         */
        private bool CalculatePositiveConvergenceStrict(int index)
        {
            IRule refIsRising = new IsRisingRule(_indicator, _timeFrame);
            IRule otherIsRising = new IsRisingRule(_indicator, _timeFrame);

            return (refIsRising.And(otherIsRising)).IsSatisfied(index);
        }

        /**
         * @param index the actual index
         * @return true, if strict negative convergent
         */
        private bool CalculateNegativeConvergenceStrict(int index)
        {
            IRule refIsFalling = new IsFallingRule(_indicator, _timeFrame);
            IRule otherIsFalling = new IsFallingRule(_indicator, _timeFrame);

            return (refIsFalling.And(otherIsFalling)).IsSatisfied(index);
        }

        /**
         * @param index the actual index
         * @return true, if positive divergent
         */
        private bool CalculatePositiveDivergenceStrict(int index)
        {
            IRule refIsRising = new IsRisingRule(_indicator, _timeFrame);
            IRule otherIsFalling = new IsFallingRule(_indicator, _timeFrame);

            return (refIsRising.And(otherIsFalling)).IsSatisfied(index);
        }

        /**
         * @param index the actual index
         * @return true, if negative divergent
         */
        private bool CalculateNegativeDivergenceStrict(int index)
        {
            IRule refIsFalling = new IsFallingRule(_indicator, _timeFrame);
            IRule otherIsRising = new IsRisingRule(_indicator, _timeFrame);

            return (refIsFalling.And(otherIsRising)).IsSatisfied(index);
        }

        /**
         * @param index the actual index
         * @return true, if positive convergent
         */
        private bool CalculatePositiveConvergence(int index)
        {
            CorrelationCoefficientIndicator cc = new CorrelationCoefficientIndicator(_indicator, _other, _timeFrame);
            bool isConvergent = cc.GetValue(index).IsGreaterThanOrEqual(_minStrenght);

            decimal slope = CalculateSlopeRel(index);
            bool isPositive = slope.IsGreaterThanOrEqual(_minSlope.Abs());

            return isConvergent && isPositive;
        }


        /**
         * @param index the actual index
         * @return true, if negative convergent
         */
        private bool CalculateNegativeConvergence(int index)
        {
            CorrelationCoefficientIndicator cc = new CorrelationCoefficientIndicator(_indicator, _other, _timeFrame);
            bool isConvergent = cc.GetValue(index).IsGreaterThanOrEqual(_minStrenght);

            decimal slope = CalculateSlopeRel(index);
            bool isNegative = slope.IsLessThanOrEqual(_minSlope.Abs().MultipliedBy(-1));

            return isConvergent && isNegative;
        }

        /**
         * @param index the actual index
         * @return true, if positive divergent
         */
        private bool CalculatePositiveDivergence(int index)
        {

            CorrelationCoefficientIndicator cc = new CorrelationCoefficientIndicator(_indicator, _other, _timeFrame);
            bool isDivergent = cc.GetValue(index).IsLessThanOrEqual(_minStrenght.MultipliedBy(-1));

            if (isDivergent)
            {
                // If "isDivergent" and "ref" is positive, then "other" must be negative.
                decimal slope = CalculateSlopeRel(index);
                return slope.IsGreaterThanOrEqual(_minSlope.Abs());
            }

            return false;
        }


        /**
         * @param index the actual index
         * @return true, if negative divergent
         */
        private bool CalculateNegativeDivergence(int index)
        {

            CorrelationCoefficientIndicator cc = new CorrelationCoefficientIndicator(_indicator, _other, _timeFrame);
            bool isDivergent = cc.GetValue(index).IsLessThanOrEqual(_minStrenght.MultipliedBy(-1));

            if (isDivergent)
            {
                // If "isDivergent" and "ref" is positive, then "other" must be negative.
                decimal slope = CalculateSlopeRel(index);
                return slope.IsLessThanOrEqual(_minSlope.Abs().MultipliedBy(-1));
            }

            return false;
        }

        /**
         * @param index the actual index
         * @return the absolute slope
         */
        private decimal CalculateSlopeAbs(int index)
        {
            SimpleLinearRegressionIndicator slrRef = new SimpleLinearRegressionIndicator(_indicator, _timeFrame);
            int firstIndex = Math.Max(0, index - _timeFrame + 1);
            return (slrRef.GetValue(index).Minus(slrRef.GetValue(firstIndex)))
                    .DividedBy(_timeFrame).Minus(firstIndex);
        }

        /**
         * @param index the actual index
         * @return the relative slope
         */
        private decimal CalculateSlopeRel(int index)
        {
            SimpleLinearRegressionIndicator slrRef = new SimpleLinearRegressionIndicator(_indicator, _timeFrame);
            int firstIndex = Math.Max(0, index - _timeFrame + 1);
            return (slrRef.GetValue(index).Minus(slrRef.GetValue(firstIndex)))
                    .DividedBy(slrRef.GetValue(index));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, MinSlope: {_minSlope}, MinStrength: {_minStrenght},  StrictType: {_strictType}, {_type}, Indicator: {_indicator.GetConfiguration()}, OtherIndicator: {_other.GetConfiguration()},";
        }
    }
}
