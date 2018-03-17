using System.Linq;
using TA4Net.Extensions;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers
{
    public class MinusIndicator : CachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _mainIndicator;
        private readonly IIndicator<decimal>[] _minusIndicators;

        /**
         * Constructor.
         * (operand0 Minus operand1 Minus []  Minus operandN)
         * @param operands the operand indicators for substraction
         * IE. IndicatorX - IndicatorY - IndicatorZ
         */
        public MinusIndicator(IIndicator<decimal> mainIndicator, params IIndicator<decimal>[] minusIndicators)
            : base(minusIndicators[0])
        {
            _mainIndicator = mainIndicator;
            _minusIndicators = minusIndicators.ToArray();
        }

        protected override decimal Calculate(int index)
        {
            decimal value = _mainIndicator.GetValue(index);
            for (int i = 0; i < _minusIndicators.Length; i++)
            {
                value = value.Minus(_minusIndicators[i].GetValue(index));
            }
            return value;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, Operands: {string.Join(",", _minusIndicators.Select(_ => _.GetConfiguration()).ToArray())}";
        }
    }
}
