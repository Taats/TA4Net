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
namespace TA4Net.Indicators
{
    using TA4Net.Extensions;
    using TA4Net.Indicators.Helpers;
    using TA4Net.Interfaces;

    /**
     * Stochastic oscillator K.
     * <p></p>
     * Receives timeSeries and timeFrame and Calculates the StochasticOscillatorKIndicator
     * over ClosePriceIndicator, or receives an indicator, MaxPriceIndicator and
     * MinPriceIndicator and returns StochasticOsiclatorK over this indicator.
     * 
     */
    public class StochasticOscillatorKIndicator : CachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _indicator;
        private readonly int _timeFrame;
        private MaxPriceIndicator _maxPriceIndicator;
        private MinPriceIndicator _minPriceIndicator;

        public StochasticOscillatorKIndicator(ITimeSeries timeSeries, int timeFrame)
            : this(new ClosePriceIndicator(timeSeries), timeFrame, new MaxPriceIndicator(timeSeries), new MinPriceIndicator(timeSeries))
        {
        }

        public StochasticOscillatorKIndicator(IIndicator<decimal> indicator, int timeFrame,
                MaxPriceIndicator MaxPriceIndicator, MinPriceIndicator MinPriceIndicator)
            : base(indicator)
        {
            _indicator = indicator;
            _timeFrame = timeFrame;
            _maxPriceIndicator = MaxPriceIndicator;
            _minPriceIndicator = MinPriceIndicator;
        }


        protected override decimal Calculate(int index)
        {
            HighestValueIndicator highestHigh = new HighestValueIndicator(_maxPriceIndicator, _timeFrame);
            LowestValueIndicator lowestMin = new LowestValueIndicator(_minPriceIndicator, _timeFrame);

            decimal highestHighPrice = highestHigh.GetValue(index);
            decimal lowestLowPrice = lowestMin.GetValue(index);

            return _indicator.GetValue(index).Minus(lowestLowPrice)
                    .DividedBy(highestHighPrice.Minus(lowestLowPrice))
                    .MultipliedBy(Decimals.HUNDRED);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, Indicator: {_indicator.GetConfiguration()}, MaxPriceIndicator: {_maxPriceIndicator.GetConfiguration()}, MinPriceIndicator: {_minPriceIndicator.GetConfiguration()}";
        }
    }
}
