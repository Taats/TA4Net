
using TA4Net.Extensions;
using TA4Net.Indicators;
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers
{
    public class SmoothedAverageLossIndicator : RecursiveCachedIndicator<decimal>
    {
        private readonly AverageLossIndicator _averageLosses;
        private readonly IIndicator<decimal> _indicator;
        private readonly int _timeFrame;

        public SmoothedAverageLossIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator)
        {
            _indicator = indicator;
            _timeFrame = timeFrame;
            _averageLosses = new AverageLossIndicator(indicator, timeFrame);
        }

        protected override decimal Calculate(int index)
        {
            if (index > _timeFrame)
            {
                return GetValue(index - 1)
                    .MultipliedBy(_timeFrame - 1)
                    .Plus(calculateLoss(index))
                    .DividedBy(_timeFrame);
            }
            return _averageLosses.GetValue(index);
        }

        private decimal calculateLoss(int index)
        {
            decimal loss = _indicator.GetValue(index).Minus(_indicator.GetValue(index - 1));
            return loss.IsNegative() ? loss.Abs() : Decimals.Zero;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, AverageLossesIndictor: {_averageLosses.GetConfiguration()}, Indicator: {_indicator.GetConfiguration()}";
        }
    }
}
