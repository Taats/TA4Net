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
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Candles
{
    /**
     * Three black crows indicator.
     * <p></p>
     * @see <a href="http://www.investopedia.com/terms/t/three_black_crows.asp">
     *     http://www.investopedia.com/terms/t/three_black_crows.asp</a>
     */
    public class ThreeBlackCrowsIndicator : CachedIndicator<bool>
    {

        private readonly ITimeSeries _series;

        /** Lower shadow */
        private readonly LowerShadowIndicator _lowerShadowInd;
        /** Average lower shadow */
        private readonly SMAIndicator _averageLowerShadowInd;
        /** Factor used when checking if a candle has a very short lower shadow */
        private readonly decimal _factor;

        private int whiteCandleIndex = -1;

        public LowerShadowIndicator LowerShadowInd => _lowerShadowInd;

        /**
         * Constructor.
         * @param series a time series
         * @param timeFrame the number of bars used to Calculate the average lower shadow
         * @param factor the factor used when checking if a candle has a very short lower shadow
         */
        public ThreeBlackCrowsIndicator(ITimeSeries series, int timeFrame, decimal factor)
            : base(series)
        {
            _series = series;
            _lowerShadowInd = new LowerShadowIndicator(series);
            _averageLowerShadowInd = new SMAIndicator(_lowerShadowInd, timeFrame);
            _factor = factor;
        }


        protected override bool Calculate(int index)
        {
            if (index < 3)
            {
                // We need 4 candles: 1 white, 3 black
                return false;
            }
            whiteCandleIndex = index - 3;
            return _series.GetBar(whiteCandleIndex).IsBullish()
                    && isBlackCrow(index - 2)
                    && isBlackCrow(index - 1)
                    && isBlackCrow(index);
        }

        /**
         * @param index the bar/candle index
         * @return true if the bar/candle has a very short lower shadow, false otherwise
         */
        private bool hasVeryShortLowerShadow(int index)
        {
            decimal currentLowerShadow = _lowerShadowInd.GetValue(index);
            // We use the white candle index to remove to bias of the previous crows
            decimal averageLowerShadow = _averageLowerShadowInd.GetValue(whiteCandleIndex);

            return currentLowerShadow.IsLessThan(averageLowerShadow.MultipliedBy(_factor));
        }

        /**
         * @param index the current bar/candle index
         * @return true if the current bar/candle is declining, false otherwise
         */
        private bool isDeclining(int index)
        {
            IBar prevBar = _series.GetBar(index - 1);
            IBar currBar = _series.GetBar(index);
            decimal prevOpenPrice = prevBar.OpenPrice;
            decimal prevClosePrice = prevBar.ClosePrice;
            decimal currOpenPrice = currBar.OpenPrice;
            decimal currClosePrice = currBar.ClosePrice;

            // Opens within the body of the previous candle
            return currOpenPrice.IsLessThan(prevOpenPrice) && currOpenPrice.IsGreaterThan(prevClosePrice)
                    // Closes below the previous close price
                    && currClosePrice.IsLessThan(prevClosePrice);
        }

        /**
         * @param index the current bar/candle index
         * @return true if the current bar/candle is a black crow, false otherwise
         */
        private bool isBlackCrow(int index)
        {
            IBar prevBar = _series.GetBar(index - 1);
            IBar currBar = _series.GetBar(index);
            if (currBar.IsBearish())
            {
                if (prevBar.IsBullish())
                {
                    // First crow case
                    return hasVeryShortLowerShadow(index)
                            && currBar.OpenPrice.IsLessThan(prevBar.MaxPrice);
                }
                else
                {
                    return hasVeryShortLowerShadow(index) && isDeclining(index);
                }
            }
            return false;
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, Factor: {_factor}, AverageLowerShadowIndicator: {_averageLowerShadowInd.GetConfiguration()}, LowerShadowIndicator: {_lowerShadowInd.GetConfiguration()}";
        }
    }
}
