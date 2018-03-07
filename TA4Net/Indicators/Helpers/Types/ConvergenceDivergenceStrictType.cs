namespace TA4Net.Indicators.Helpers.Types
{
    /**
         * Select the type of strict convergence or divergence.
         */
    public enum ConvergenceDivergenceStrictType
    {

        /**
         * Returns true for <b>"positiveConvergentStrict"</b> when the values of
         * the ref-{@link Indicator indicator} and the values of the
         * other-{@link Indicator indicator} increase consecutively within a
         * timeFrame. In short: "other" and "ref" makes strict higher highs.
         */
        positiveConvergentStrict,

        /**
         * Returns true for <b>"negativeConvergentStrict"</b> when the values of
         * the ref-{@link Indicator indicator} and the values of the
         * other-{@link Indicator indicator} decrease consecutively within a
         * timeFrame. In short: "other" and "ref" makes strict lower lows.
         */
        negativeConvergentStrict,

        /**
         * Returns true for <b>"positiveDivergentStrict"</b> when the values of
         * the ref-{@link Indicator indicator} increase consecutively and the
         * values of the other-{@link Indicator indicator} decrease
         * consecutively within a timeFrame. In short: "other" makes strict
         * higher highs and "ref" makes strict lower lows.
         */
        positiveDivergentStrict,

        /**
         * Returns true for <b>"negativeDivergentStrict"</b> when the values of
         * the ref-{@link Indicator indicator} decrease consecutively and the
         * values of the other-{@link Indicator indicator} increase
         * consecutively within a timeFrame. In short: "other" makes strict
         * lower lows and "ref" makes strict higher highs.
         */
        negativeDivergentStrict
    }

}
