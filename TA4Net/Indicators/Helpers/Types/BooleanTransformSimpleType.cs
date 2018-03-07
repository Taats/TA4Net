namespace TA4Net.Indicators.Helpers.Types
{
    /**
             * Select the type for transFormation.
             */
    public enum BooleanTransformSimpleType
    {
        /**
         * Transforms the decimal indicator to a bool indicator by
         * indicator.IsNaN().
         */
        isNaN,

        /**
         * Transforms the decimal indicator to a bool indicator by
         * indicator.isNegative().
         */
        isNegative,

        /**
         * Transforms the decimal indicator to a bool indicator by
         * indicator.isNegativeOrZero().
         */
        isNegativeOrZero,

        /**
         * Transforms the decimal indicator to a bool indicator by
         * indicator.isPositive().
         */
        isPositive,

        /**
         * Transforms the decimal indicator to a bool indicator by
         * indicator.isPositiveOrZero().
         */
        isPositiveOrZero,

        /**
         * Transforms the decimal indicator to a bool indicator by
         * indicator.IsZero().
         */
        isZero
    }
}
