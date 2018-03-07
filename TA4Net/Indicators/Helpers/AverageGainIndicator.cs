using TA4Net.Extensions;
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers
{
    public class AverageGainIndicator : CachedIndicator<decimal>
    {
        private readonly CumulatedGainsIndicator _cumulatedGains;
        private readonly int _timeFrame;

        public AverageGainIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator)
        {
            _timeFrame = timeFrame;
            _cumulatedGains = new CumulatedGainsIndicator(indicator, timeFrame);
        }

        protected override decimal Calculate(int index)
        {
            int realTimeFrame = Math.Min(_timeFrame, index + 1);
            return _cumulatedGains.GetValue(index).DividedBy(realTimeFrame);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, CumulatedGainsIndicator: {_cumulatedGains.GetConfiguration()}";
        }
    }
}
