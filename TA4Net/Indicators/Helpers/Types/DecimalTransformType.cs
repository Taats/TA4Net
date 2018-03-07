namespace TA4Net.Indicators.Helpers.Types
{
    public enum decimalTransformType
    {

        /**
         * Transforms the input indicator 
         * by indicator.Plus(coefficient).
         */
        Plus,

        /**
         * Transforms the input indicator 
         * by indicator.Minus(coefficient).
         */
        Minus,

        /**
         * Transforms the input indicator by
         * indicator.MultipliedBy(coefficient).
         */
        multiply,

        /**
         * Transforms the input indicator 
         * by indicator.DividedBy(coefficient).
         */
        divide,

        /**
         * Transforms the input indicator 
         * by indicator.Max(coefficient).
         */
        Max,

        /**
         * Transforms the input indicator 
         * by indicator.Min(coefficient).
         */
        Min
    }

}
