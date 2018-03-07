
using TA4Net.Extensions;
using TA4Net.Indicators;
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers
{
    public class CumulatedGainsIndicator : CachedIndicator<decimal>
    {
        private IIndicator<decimal> _indicator;
        private readonly int _timeFrame;

        public CumulatedGainsIndicator(IIndicator<decimal> indicator, int timeFrame)
            : base(indicator)
        {
            _indicator = indicator;
            _timeFrame = timeFrame;
        }

        protected override decimal Calculate(int index)
        {
            decimal sumOfGains = Decimals.Zero;
            for (int i = Math.Max(1, index - _timeFrame + 1); i <= index; i++)
            {
                if (_indicator.GetValue(i).IsGreaterThan(_indicator.GetValue(i - 1)))
                {
                    sumOfGains = sumOfGains.Plus(_indicator.GetValue(i).Minus(_indicator.GetValue(i - 1)));
                }
            }
            return sumOfGains;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, Indicator: {_indicator.GetConfiguration()}";
        }
    }
}
