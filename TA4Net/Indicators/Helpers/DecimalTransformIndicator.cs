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
using TA4Net.Indicators.Helpers.Types;

namespace TA4Net.Indicators.Helpers
{
    /**
     * Simple decimal transform indicator.
     * </p>
     * @apiNote Minimal deviations in last decimal places possible. During the calculations this indicator converts {@link decimal decimal/Bigdecimal} to to {@link double double}
     * Transforms any indicator by using common math operations.
     */
    public class DecimalTransformIndicator : CachedIndicator<decimal>
    {
        private decimal _coefficient;
        private IIndicator<decimal> _indicator;
        private decimalTransformType? _type;
        private decimalTransformSimpleType? _simpleType;

        /**
         * Constructor.
         * 
         * @param indicator the indicator
         * @param coefficient the value for transFormation
         * @param type the type of the transFormation
         */
        public DecimalTransformIndicator(IIndicator<decimal> indicator, decimal coefficient, decimalTransformType type)
            : base(indicator)
        {
            _indicator = indicator;
            _coefficient = coefficient;
            _type = type;
        }

        /**
         * Constructor.
         * 
         * @param indicator the indicator
         * @param type the type of the transFormation
         */
        public DecimalTransformIndicator(IIndicator<decimal> indicator, decimalTransformSimpleType type)
            : base(indicator)
        {
            _indicator = indicator;
            _simpleType = type;
        }


        protected override decimal Calculate(int index)
        {

            decimal val = _indicator.GetValue(index);

            if (_type != null)
            {
                switch (_type)
                {
                    case decimalTransformType.Plus:
                        return val.Plus(_coefficient);
                    case decimalTransformType.Minus:
                        return val.Minus(_coefficient);
                    case decimalTransformType.multiply:
                        return val.MultipliedBy(_coefficient);
                    case decimalTransformType.divide:
                        return val.DividedBy(_coefficient);
                    case decimalTransformType.Max:
                        return val.Max(_coefficient);
                    case decimalTransformType.Min:
                        return val.Min(_coefficient);
                }
            }

            else if (_simpleType != null)
            {
                switch (_simpleType)
                {
                    case decimalTransformSimpleType.sqrt:
                        return val.Sqrt();
                    case decimalTransformSimpleType.abs:
                        return val.Abs();
                    case decimalTransformSimpleType.log:
                        return val.Log();
                }
            }

            return val;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, Coefficient: {_coefficient}, SimpleType: {_simpleType}, Type: {_type}, Indicator: {_indicator.GetConfiguration()}";
        }
    }
}
