using TA4Net.Extensions;
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Statistics
{
    /**
     * Indicator-Pearson-Correlation
     * <p/>
     * see
     * http://www.statisticshowto.com/probability-and-statistics/correlation-coefficient-formula/
     */
    public class PearsonCorrelationIndicator : RecursiveCachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _indicator1;
        private readonly IIndicator<decimal> _indicator2;
        private readonly int _timeFrame;

        /**
         * Constructor.
         * 
         * @param indicator1 the first indicator
         * @param indicator2 the second indicator
         * @param timeFrame the time frame
         */
        public PearsonCorrelationIndicator(IIndicator<decimal> indicator1, IIndicator<decimal> indicator2, int timeFrame)
            : base(indicator1)
        {
            _indicator1 = indicator1;
            _indicator2 = indicator2;
            _timeFrame = timeFrame;
        }

        protected override decimal Calculate(int index)
        {

            decimal n = _timeFrame;

            decimal Sx = Decimals.Zero;
            decimal Sy = Decimals.Zero;
            decimal Sxx = Decimals.Zero;
            decimal Syy = Decimals.Zero;
            decimal Sxy = Decimals.Zero;

            for (int i = Math.Max(TimeSeries.GetBeginIndex(), index - _timeFrame + 1); i <= index; i++) {

                decimal x = _indicator1.GetValue(i);
                decimal y = _indicator2.GetValue(i);

                Sx = Sx.Plus(x);
                Sy = Sy.Plus(y);
                Sxy = Sxy.Plus(x.MultipliedBy(y));
                Sxx = Sxx.Plus(x.MultipliedBy(x));
                Syy = Syy.Plus(y.MultipliedBy(y));
            }

            // (n * Sxx - Sx * Sx) * (n * Syy - Sy * Sy)
            decimal toSqrt = (n.MultipliedBy(Sxx).Minus(Sx.MultipliedBy(Sx)))
                    .MultipliedBy(n.MultipliedBy(Syy).Minus(Sy.MultipliedBy(Sy)));

            if (toSqrt.IsGreaterThan(Decimals.Zero))
            {
                // pearson = (n * Sxy - Sx * Sy) / sqrt((n * Sxx - Sx * Sx) * (n * Syy - Sy * Sy))
                return (n.MultipliedBy(Sxy).Minus(Sx.MultipliedBy(Sy))).DividedBy(toSqrt.Sqrt());
            }

            return Decimals.NaN;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, Indicator1: {_indicator1.GetConfiguration()}, Indicator2: {_indicator2.GetConfiguration()}";
        }
    }
}
