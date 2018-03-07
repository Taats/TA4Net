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
     * Intraday Intensity Index
     * see https://www.investopedia.com/terms/i/intradayintensityindex.asp
     *     https://www.investopedia.com/terms/i/intradayintensityindex.asp
     */
    public class IIIIndicator : CachedIndicator<decimal>
    {
        private readonly ClosePriceIndicator _closePriceIndicator;
        private readonly MaxPriceIndicator _maxPriceIndicator;
        private readonly MinPriceIndicator _minPriceIndicator;
        private readonly VolumeIndicator _volumeIndicator;

        public IIIIndicator(ITimeSeries series)
            : base(series)
        {
            _closePriceIndicator = new ClosePriceIndicator(series);
            _maxPriceIndicator = new MaxPriceIndicator(series);
            _minPriceIndicator = new MinPriceIndicator(series);
            _volumeIndicator = new VolumeIndicator(series);
        }

        protected override decimal Calculate(int index)
        {

            if (index == TimeSeries.GetBeginIndex())
            {
                return Decimals.Zero;
            }
            decimal doubleClosePrice = 2.MultipliedBy(_closePriceIndicator.GetValue(index));
            decimal highmlow = _maxPriceIndicator.GetValue(index).Minus(_minPriceIndicator.GetValue(index));
            decimal highplow = _maxPriceIndicator.GetValue(index).Plus(_minPriceIndicator.GetValue(index));

            return (doubleClosePrice.Minus(highplow)).DividedBy(highmlow.MultipliedBy(_volumeIndicator.GetValue(index)));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, ClosePriceIndicator: {_closePriceIndicator.GetConfiguration()}, MinPriceIndicator: {_minPriceIndicator.GetConfiguration()}, MaxPriceIndicator: {_maxPriceIndicator.GetConfiguration()}, VolumeIndicator: {_volumeIndicator.GetConfiguration()}";
        }
    }
}
