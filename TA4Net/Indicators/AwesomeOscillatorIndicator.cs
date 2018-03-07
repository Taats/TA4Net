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

namespace TA4Net.Indicators
{
    /**
     * Awesome oscillator. (AO)
     * <p/>
     * see https://www.tradingview.com/wiki/Awesome_Oscillator_(AO)
     */
    public class AwesomeOscillatorIndicator : CachedIndicator<decimal>
    {
        private readonly SMAIndicator _sma5;
        private readonly SMAIndicator _sma34;

        /**
         * Constructor.
         * 
         * @param indicator (normally {@link MedianPriceIndicator})
         * @param timeFrameSma1 (normally 5)
         * @param timeFrameSma2 (normally 34)
         */
        public AwesomeOscillatorIndicator(IIndicator<decimal> indicator, int timeFrameSma1, int timeFrameSma2)
            : base(indicator)
        {
            _sma5 = new SMAIndicator(indicator, timeFrameSma1);
            _sma34 = new SMAIndicator(indicator, timeFrameSma2);
        }

        /**
         * Constructor.
         * 
         * @param indicator (normally {@link MedianPriceIndicator})
         */
        public AwesomeOscillatorIndicator(IIndicator<decimal> indicator)
            : this(indicator, 5, 34)
        {
        }

        /**
         * Constructor.
         * 
         * @param series the timeSeries
         */
        public AwesomeOscillatorIndicator(ITimeSeries series)
            : this(new MedianPriceIndicator(series), 5, 34)
        {
        }


        protected override decimal Calculate(int index)
        {
            return _sma5.GetValue(index).Minus(_sma34.GetValue(index));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, SMA34: {_sma34.GetConfiguration()}, SMA5: {_sma5.GetConfiguration()}";
        }
    }
}
