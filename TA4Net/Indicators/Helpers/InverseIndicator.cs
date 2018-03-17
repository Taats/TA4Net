using System;
using System.Collections.Generic;
using System.Text;
using TA4Net.Extensions;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Helpers
{
    public class InverseIndicator : CachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _indicator;

        public InverseIndicator(IIndicator<decimal> indicator) : base(indicator)
        {
            _indicator = indicator;
        }

        public override string GetConfiguration()
        {
            throw new NotImplementedException();
        }

        protected override decimal Calculate(int index)
        {
            return _indicator.GetValue(index).MultipliedBy(Decimals.MINUSONE);
        }
    }
}
