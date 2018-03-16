using TA4Net.Extensions;
using TA4Net.Indicators.Helpers;
using TA4Net.Interfaces;

namespace TA4Net.Indicators
{

    /*  The True Strength Index (TSI) is a momentum-based indicator that was developed by William Blau and is a variation of the Relative Strength Index.
     *  It is used to help determine trend and identify overbought and oversold conditions. The indicator has very limited time lag compared to for example 
     *  a moving average, and so is particularly useful for spotting early changes of trend.
     *  The TSI will rise when the short term trend is up and fall when the shot term trend is down. An increasing TSI indicates increasing momentum in 
     *  the direction of the price movement. When the indicator is rising above zero, the price is always rising. When the indicator is falling below zero,
     *  the price is always falling. Technical analysts have their own preferences as to what are considered overbought and oversold levels. 
     *  Typically values of -25 and +25 are used to indicate levels whether the market is overbought or oversold, but +/-30 and +/-50 are also common.
     * 
     *  NB. +/- 50 is often used in a young market while +/- 25 is often used in a more mature market.
     *  
     *  http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:true_strength_index
     */
    public class TSIIndicator : CachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _indicator;

        private readonly DifferenceIndicator _differenceIndicator;
        private readonly EMAIndicator _ema25OfPriceChangeIndicator;
        private readonly EMAIndicator _ema13OfEma25OfPriceChangeIndicator;

        private readonly AbsoluteIndicator _absolutePriceChangeIndicator;
        private readonly EMAIndicator _ema25OfAbsolutePriceChangeIndicator;
        private readonly EMAIndicator _ema13OfEma25OfAbsolutePriceChangeIndicator;

        public TSIIndicator(ITimeSeries timeSeries) 
            : this(new ClosePriceIndicator(timeSeries), timeSeries)
        {

        }

        public TSIIndicator(IIndicator<decimal> indicator, ITimeSeries timeSeries) :base(timeSeries)
        {
            _indicator = indicator;

            // Double Smoothed price change
            _differenceIndicator = new DifferenceIndicator(indicator, new PreviousValueIndicator(indicator));
            _ema25OfPriceChangeIndicator = new EMAIndicator(_differenceIndicator, 25);
            _ema13OfEma25OfPriceChangeIndicator = new EMAIndicator(_ema25OfPriceChangeIndicator, 13);

            // Double Smoothed Absolute price change
            _absolutePriceChangeIndicator = new AbsoluteIndicator(_differenceIndicator);
            _ema25OfAbsolutePriceChangeIndicator = new EMAIndicator(_absolutePriceChangeIndicator, 25);
            _ema13OfEma25OfAbsolutePriceChangeIndicator = new EMAIndicator(_ema25OfAbsolutePriceChangeIndicator, 13);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, Indicator : {_indicator.GetConfiguration()}";
        }

        protected override decimal Calculate(int index)
        {
            return _ema13OfEma25OfPriceChangeIndicator.GetValue(index).DividedBy(_ema13OfEma25OfAbsolutePriceChangeIndicator.GetValue(index))
                .MultipliedBy(Decimals.HUNDRED);
        }
    }
}
