using TA4Net.Extensions;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Candles
{

    /// <summary>
    /// Heikin-Ashi Candlesticks are an offshoot from Japanese candlesticks. Heikin-Ashi Candlesticks use the open-close data
    /// from the prior period and the open-high-low-close data from the current period to create a combo candlestick. The resulting 
    /// candlestick filters out some noise in an effort to better capture the trend. In Japanese, Heikin means “average” and ashi
    /// means “pace” (EUDict.com). Taken together, Heikin-Ashi represents the average pace of prices. Heikin-Ashi Candlesticks are
    /// not used like normal candlesticks. Dozens of bullish or bearish reversal patterns consisting of 1-3 candlesticks are not to 
    /// be found. Instead, these candlesticks can be used to identify trending periods, potential reversal points and classic 
    /// technical analysis patterns.
    /// 
    /// http://stockcharts.com/school/doku.php?id=chart_school:chart_analysis:heikin_ashi
    /// https://www.investopedia.com/articles/technical/04/092204.asp
    /// </summary>
    /// <seealso cref="TA4Net.Indicators.RecursiveCachedIndicator{IBar}" />
    public class HeikinAshiIndicator : RecursiveCachedIndicator<IBar>
    {
        public HeikinAshiIndicator(ITimeSeries timeSeries) : base(timeSeries)
        {

        }

        public override string GetConfiguration()
        {
            return $" {GetType()}";
        }

        protected override IBar Calculate(int index)
        {
            var bar = TimeSeries.GetBar(index);

            if (index == 0)
            {
                return bar;
            }

            IBar previousHABar = GetValue(index - 1);

            var haClose = bar.OpenPrice.Plus(bar.ClosePrice).Plus(bar.MinPrice).Plus(bar.MaxPrice).DividedBy(Decimals.FOUR);
            var haOpen = previousHABar.OpenPrice.Plus(previousHABar.ClosePrice).DividedBy(Decimals.TWO);
            var haHigh = bar.MaxPrice.Max(haOpen).Max(haClose);
            var haLow = bar.MinPrice.Min(haOpen).Min(haClose);

            return new BaseBar(bar.TimePeriod, bar.EndTime, haOpen, haHigh, haLow, haClose, bar.Volume, bar.Amount);
        }
    }
}