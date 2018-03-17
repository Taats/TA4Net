using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers
{
    /// <summary>
    /// This indicator can not return a value higher the given max value.
    /// </summary>
    public class MaximumValueIndicator : CachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _indicator;
        private readonly decimal _maxValue;

        public MaximumValueIndicator(IIndicator<decimal> indicator, decimal maxValue) : base(indicator)
        {
            _indicator = indicator;
            _maxValue = maxValue;
        }
        
        public override string GetConfiguration()
        {
            return $" {GetType()}, MaxValue: {_maxValue}, Indicator: {_indicator.GetConfiguration()}";
        }

        protected override decimal Calculate(int index)
        {
            var value = _indicator.GetValue(index);
            return value < _maxValue ? value : _maxValue;
        }
    }
}
