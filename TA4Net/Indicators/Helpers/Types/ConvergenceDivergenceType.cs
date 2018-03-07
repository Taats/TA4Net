namespace TA4Net.Indicators.Helpers.Types
{
    public enum ConvergenceDivergenceType
    {
        /**
         * Returns true for <b>"positiveConvergent"</b> when the values of the
         * ref-{@link Indicator indicator} and the values of the
         * other-{@link Indicator indicator} increase within the timeFrame. In
         * short: "other" and "ref" makes higher highs.
         */
        positiveConvergent,

        /**
         * Returns true for <b>"negativeConvergent"</b> when the values of the
         * ref-{@link Indicator indicator} and the values of the
         * other-{@link Indicator indicator} decrease within the timeFrame. In
         * short: "other" and "ref" makes lower lows.
         */
        negativeConvergent,

        /**
         * Returns true for <b>"positiveDivergent"</b> when the values of the
         * ref-{@link Indicator indicator} increase and the values of the
         * other-{@link Indicator indicator} decrease within a timeFrame. In
         * short: "other" makes lower lows while "ref" makes higher highs.
         */
        positiveDivergent,

        /**
         * Returns true for <b>"negativeDivergent"</b> when the values of the
         * ref-{@link Indicator indicator} decrease and the values of the
         * other-{@link Indicator indicator} increase within a timeFrame. In
         * short: "other" makes higher highs while "ref" makes lower lows.
         */
        negativeDivergent
    }

}
