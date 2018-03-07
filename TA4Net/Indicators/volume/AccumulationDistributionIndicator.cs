/*
  The MIT License (MIT)

  Copyright (c) 2014-2017 Marc de Verdelhan & respective authors (see AUTHORS)

  Permission is hereby granted, free of charge, to any person obtaining a copy of
  this software and associated documentation files (the "Software"), to deal in
  the Software without restriction, including without limitation the rights to
  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
  the Software, and to permit persons to whom the Software is furnished to do so,
  subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
  FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
  COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
  IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
  CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using TA4Net.Extensions;
using TA4Net.Indicators.Helpers;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Volume
{
    /**
     * Accumulation-distribution indicator.
     * <p></p>
     */
    public class AccumulationDistributionIndicator : RecursiveCachedIndicator<decimal>
    {
        private ITimeSeries _series;
        private CloseLocationValueIndicator _clvIndicator;

        public AccumulationDistributionIndicator(ITimeSeries series)
            : base(series)
        {
            _series = series;
            _clvIndicator = new CloseLocationValueIndicator(series);
        }

        protected override decimal Calculate(int index)
        {
            if (index == 0)
            {
                return Decimals.Zero;
            }

            // Calculating the money flow multiplier
            decimal moneyFlowMultiplier = _clvIndicator.GetValue(index);

            // Calculating the money flow volume
            decimal moneyFlowVolume = moneyFlowMultiplier.MultipliedBy(_series.GetBar(index).Volume);

            return moneyFlowVolume.Plus(GetValue(index - 1));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, CloseLocationValueIndicator: {_clvIndicator.GetConfiguration()}";
        }


    }
}
