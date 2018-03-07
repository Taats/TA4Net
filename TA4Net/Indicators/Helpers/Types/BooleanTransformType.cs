using System;
using System.Collections.Generic;
using System.Text;

namespace TA4Net.Indicators.Helpers.Types
{
    public enum BooleanTransformType
    {

        /**
         * Transforms the decimal indicator to a bool indicator by
         * indicator.Equals(coefficient).
         */
        equals,

        /**
         * Transforms the decimal indicator to a bool indicator by
         * indicator.IsGreaterThan(coefficient).
         */
        IsGreaterThan,

        /**
         * Transforms the decimal indicator to a bool indicator by
         * indicator.IsGreaterThanOrEqual(coefficient).
         */
        IsGreaterThanOrEqual,

        /**
         * Transforms the decimal indicator to a bool indicator by
         * indicator.IsLessThan(coefficient).
         */
        IsLessThan,

        /**
         * Transforms the decimal indicator to a bool indicator by
         * indicator.IsLessThanOrEqual(coefficient).
         */
        IsLessThanOrEqual
    }

}
