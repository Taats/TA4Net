
using TA4Net.Extensions;
using System;
using TA4Net.Indicators;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers 
{
    public class CumulatedLossesIndicator : RecursiveCachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _indicator;
        private readonly int _timeFrame;

        public CumulatedLossesIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator.TimeSeries)
        {
            _timeFrame = timeFrame;
            _indicator = indicator;
        }

        protected override decimal Calculate(int index)
        {
            decimal sumOfLosses = Decimals.Zero;
            for(int i = Math.Max(1, index - _timeFrame + 1); i <= index; i++)
            {
                if (_indicator.GetValue(i).IsLessThan(_indicator.GetValue(i - 1)))
                {
                    sumOfLosses = sumOfLosses.Plus(_indicator.GetValue(i - 1).Minus(_indicator.GetValue(i)));
                }
            }
            return sumOfLosses;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, Indicator: {_indicator.GetConfiguration()}";
        }
    }
}
