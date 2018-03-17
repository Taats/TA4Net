using System.Linq;
using TA4Net.Extensions;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers
{
    public class AverageIndicator : CachedIndicator<decimal>
    {
        private readonly IIndicator<decimal>[] _indicators;

        public AverageIndicator(ITimeSeries timeSeries) : this(
            new OpenPriceIndicator(timeSeries),
            new ClosePriceIndicator(timeSeries),
            new MaxPriceIndicator(timeSeries),
            new MinPriceIndicator(timeSeries))
        {

        }

        public AverageIndicator(params IIndicator<decimal>[] indicators)
            : base(indicators[0])
        {
            _indicators = indicators.ToArray();
        }


        protected override decimal Calculate(int index)
        {
            decimal value = Decimals.Zero;
            for (int i = 0; i < _indicators.Length; i++)
            {
                var indicatorValue = _indicators[i].GetValue(index);
                if (indicatorValue == Decimals.NaN)
                {
                    return Decimals.NaN;
                }

                value = value.Plus(indicatorValue);
            }
            return value.DividedBy(_indicators.Length);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, Operands: {string.Join(",", _indicators.Select(_ => _.GetConfiguration()).ToArray())}";
        }
    }
}
