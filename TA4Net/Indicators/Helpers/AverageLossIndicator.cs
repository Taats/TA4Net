using TA4Net.Extensions;
using TA4Net.Indicators;
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers
{
    public class AverageLossIndicator : RecursiveCachedIndicator<decimal>
    {
        private CumulatedLossesIndicator _cumulatedLossesIndicator;
        private readonly int _timeFrame;

        public AverageLossIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator)
        {
            _timeFrame = timeFrame;
            _cumulatedLossesIndicator = new CumulatedLossesIndicator(indicator, timeFrame);
        }

        protected override decimal Calculate(int index)
        {
            int realTimeFrame = Math.Min(_timeFrame, index + 1);
            return _cumulatedLossesIndicator.GetValue(index).DividedBy(realTimeFrame);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, CumuletedLossesIndicator: {_cumulatedLossesIndicator.GetConfiguration()}";
        }
    }
}
