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
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators
{
    /**
     * The Kaufman's Adaptive Moving Average (KAMA)  Indicator.
     * 
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:kaufman_s_adaptive_moving_average">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:kaufman_s_adaptive_moving_average</a>
     */
    public class KAMAIndicator : RecursiveCachedIndicator<decimal>
    {
        private readonly IIndicator<decimal> _price;
        private readonly int _timeFrameEffectiveRatio;
        private readonly decimal _fastest;
        private readonly decimal _slowest;

        /**
         * Constructor.
         *
         * @param price the price
         * @param timeFrameEffectiveRatio the time frame of the effective ratio (usually 10)
         * @param timeFrameFast the time frame fast (usually 2)
         * @param timeFrameSlow the time frame slow (usually 30)
         */
        public KAMAIndicator(IIndicator<decimal> price, int timeFrameEffectiveRatio, int timeFrameFast, int timeFrameSlow)
            : base(price)
        {
            _price = price;
            _timeFrameEffectiveRatio = timeFrameEffectiveRatio;
            _fastest = Decimals.TWO.DividedBy(timeFrameFast + 1);
            _slowest = Decimals.TWO.DividedBy(timeFrameSlow + 1);
        }


        protected override decimal Calculate(int index)
        {
            decimal currentPrice = _price.GetValue(index);
            if (index < _timeFrameEffectiveRatio)
            {
                return currentPrice;
            }
            /*
             * Efficiency Ratio (ER)
             * ER = Change/Volatility
             * Change = ABS(Close - Close (10 periods ago))
             * Volatility = Sum10(ABS(Close - Prior Close))
             * Volatility is the sum of the absolute value of the last ten price changes (Close - Prior Close).
             */
            int startChangeIndex = Math.Max(0, index - _timeFrameEffectiveRatio);
            decimal change = currentPrice.Minus(_price.GetValue(startChangeIndex)).Abs();
            decimal volatility = Decimals.Zero;
            for (int i = startChangeIndex; i < index; i++)
            {
                volatility = volatility.Plus(_price.GetValue(i + 1).Minus(_price.GetValue(i)).Abs());
            }
            decimal er = change.DividedBy(volatility);
            /*
             * Smoothing Constant (SC)
             * SC = [ER x (fastest SC - slowest SC) + slowest SC]2
             * SC = [ER x (2/(2+1) - 2/(30+1)) + 2/(30+1)]2
             */
            decimal sc = er.MultipliedBy(_fastest.Minus(_slowest)).Plus(_slowest).Pow(2);
            /*
             * KAMA
             * Current KAMA = Prior KAMA + SC x (Price - Prior KAMA)
             */
            decimal priorKAMA = GetValue(index - 1);
            return priorKAMA.Plus(sc.MultipliedBy(currentPrice.Minus(priorKAMA)));
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, Fastest: {_fastest}, Slowest: {_slowest}, TimeFrameEffectiveRatio: {_timeFrameEffectiveRatio}, PriceIndicator: {_price.GetConfiguration()}";
        }
    }
}
