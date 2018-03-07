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
using TA4Net.Indicators.Helpers.Types;
using TA4Net.Indicators.Statistics.Types;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers
{
    /**
     * Simple bool transform indicator.
     * </p>
     * Transforms any decimal indicator to a bool indicator by using common
     * logical operators.
     */
    public class BooleanTransformIndicator : CachedIndicator<bool>
    {
        private readonly IIndicator<decimal> _indicator;
        private readonly decimal _coefficient;
        private readonly BooleanTransformType? _type;
        private readonly BooleanTransformSimpleType? _simpleType;

        /**
         * Constructor.
         * 
         * @param indicator the indicator
         * @param coefficient the value for transFormation
         * @param type the type of the transFormation
         */
        public BooleanTransformIndicator(IIndicator<decimal> indicator, decimal coefficient, BooleanTransformType type)
            : base(indicator.TimeSeries)
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
        public BooleanTransformIndicator(IIndicator<decimal> indicator, BooleanTransformSimpleType type)
            : base(indicator.TimeSeries)
        {
            _indicator = indicator;
            _simpleType = type;
        }


        protected override bool Calculate(int index)
        {

            decimal val = _indicator.GetValue(index);

            if (_type != null) {
                switch (_type)
                {
                    case BooleanTransformType.equals:
                        return val.Equals(_coefficient);
                    case BooleanTransformType.IsGreaterThan:
                        return val.IsGreaterThan(_coefficient);
                    case BooleanTransformType.IsGreaterThanOrEqual:
                        return val.IsGreaterThanOrEqual(_coefficient);
                    case BooleanTransformType.IsLessThan:
                        return val.IsLessThan(_coefficient);
                    case BooleanTransformType.IsLessThanOrEqual:
                        return val.IsLessThanOrEqual(_coefficient);
                    default: break;
                }
            }

            else if (_simpleType != null)
            {
                switch (_simpleType)
                {
                    case BooleanTransformSimpleType.isNaN:
                        return val.IsNaN();
                    case BooleanTransformSimpleType.isNegative:
                        return val.IsNegative();
                    case BooleanTransformSimpleType.isNegativeOrZero:
                        return val.IsNegativeOrZero();
                    case BooleanTransformSimpleType.isPositive:
                        return val.IsPositive();
                    case BooleanTransformSimpleType.isPositiveOrZero:
                        return val.IsPositiveOrZero();
                    case BooleanTransformSimpleType.isZero:
                        return val.IsZero();
				    default: break;
                }
            }

            return false;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, Coefficient: {_coefficient}, SimpleType: {_simpleType}, Type: {_type}, Indicator: {_indicator.GetConfiguration()}";
        }
    }
}
