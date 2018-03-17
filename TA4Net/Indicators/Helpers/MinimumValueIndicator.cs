using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers
{
    /// <summary>
    /// This indicator can not return a lower higher the given min value.
    /// </summary>
    public class MinimumValueIndicator : CachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _indicator;
        private readonly decimal _minValue;

        public MinimumValueIndicator(IIndicator<decimal> indicator, decimal minValue) : base(indicator)
        {
            _indicator = indicator;
            _minValue = minValue;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, MinValue: {_minValue}, Indicator: {_indicator.GetConfiguration()}";
        }

        protected override decimal Calculate(int index)
        {
            var value = _indicator.GetValue(index);
            return _minValue < value ? value : _minValue;
        }
    }
}
