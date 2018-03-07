
using TA4Net.Extensions;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers
{
    public class SmoothedAverageGainIndicator : RecursiveCachedIndicator<decimal>
    {
        private readonly AverageGainIndicator _averageGains;
        private readonly IIndicator<decimal> _indicator;
        private readonly int _timeFrame;

        public SmoothedAverageGainIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator)
        {
            _timeFrame = timeFrame;
            _indicator = indicator;
            _averageGains = new AverageGainIndicator(indicator, timeFrame);
        }

        protected override decimal Calculate(int index)
        {
            if (index > _timeFrame)
            {
                return GetValue(index - 1)
                    .MultipliedBy(_timeFrame - 1)
                    .Plus(calculateGain(index))
                    .DividedBy(_timeFrame);
            }
            return _averageGains.GetValue(index);
        }

        private decimal calculateGain(int index)
        {
            decimal gain = _indicator.GetValue(index).Minus(_indicator.GetValue(index - 1));
            return gain.IsPositive() ? gain : Decimals.Zero;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, AverageGainsIndicator: {_averageGains.GetConfiguration()}, Indicator: {_indicator}";
        }
    }
}
