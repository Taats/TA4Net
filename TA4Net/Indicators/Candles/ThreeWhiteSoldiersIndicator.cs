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
     * Three white soldiers indicator.
     * <p></p>
     * @see <a href="http://www.investopedia.com/terms/t/three_white_soldiers.asp">
     *     http://www.investopedia.com/terms/t/three_white_soldiers.asp</a>
     */
    public class ThreeWhiteSoldiersIndicator : CachedIndicator<bool>
    {

        private readonly ITimeSeries _series;

        /** Upper shadow */
        private readonly UpperShadowIndicator _upperShadowInd;
        /** Average upper shadow */
        private readonly SMAIndicator _averageUpperShadowInd;
        /** Factor used when checking if a candle has a very short upper shadow */
        private readonly decimal _factor;

        private int blackCandleIndex = -1;

        /**
         * Constructor.
         * @param series a time series
         * @param timeFrame the number of bars used to Calculate the average upper shadow
         * @param factor the factor used when checking if a candle has a very short upper shadow
         */
        public ThreeWhiteSoldiersIndicator(ITimeSeries series, int timeFrame, decimal factor)
            : base(series)
        {
            _series = series;
            _upperShadowInd = new UpperShadowIndicator(series);
            _averageUpperShadowInd = new SMAIndicator(_upperShadowInd, timeFrame);
            _factor = factor;
        }


        protected override bool Calculate(int index)
        {
            if (index < 3)
            {
                // We need 4 candles: 1 black, 3 white
                return false;
            }
            blackCandleIndex = index - 3;
            return _series.GetBar(blackCandleIndex).IsBearish()
                    && isWhiteSoldier(index - 2)
                    && isWhiteSoldier(index - 1)
                    && isWhiteSoldier(index);
        }

        /**
         * @param index the bar/candle index
         * @return true if the bar/candle has a very short upper shadow, false otherwise
         */
        private bool hasVeryShortUpperShadow(int index)
        {
            decimal currentUpperShadow = _upperShadowInd.GetValue(index);
            // We use the black candle index to remove to bias of the previous soldiers
            decimal averageUpperShadow = _averageUpperShadowInd.GetValue(blackCandleIndex);

            return currentUpperShadow.IsLessThan(averageUpperShadow.MultipliedBy(_factor));
        }

        /**
         * @param index the current bar/candle index
         * @return true if the current bar/candle is growing, false otherwise
         */
        private bool isGrowing(int index)
        {
            IBar prevBar = _series.GetBar(index - 1);
            IBar currBar = _series.GetBar(index);
            decimal prevOpenPrice = prevBar.OpenPrice;
            decimal prevClosePrice = prevBar.ClosePrice;
            decimal currOpenPrice = currBar.OpenPrice;
            decimal currClosePrice = currBar.ClosePrice;

            // Opens within the body of the previous candle
            return currOpenPrice.IsGreaterThan(prevOpenPrice) && currOpenPrice.IsLessThan(prevClosePrice)
                    // Closes above the previous close price
                    && currClosePrice.IsGreaterThan(prevClosePrice);
        }

        /**
         * @param index the current bar/candle index
         * @return true if the current bar/candle is a white soldier, false otherwise
         */
        private bool isWhiteSoldier(int index)
        {
            IBar prevBar = _series.GetBar(index - 1);
            IBar currBar = _series.GetBar(index);
            if (currBar.IsBullish())
            {
                if (prevBar.IsBearish())
                {
                    // First soldier case
                    return hasVeryShortUpperShadow(index)
                            && currBar.OpenPrice.IsGreaterThan(prevBar.MinPrice);
                }
                else
                {
                    return hasVeryShortUpperShadow(index) && isGrowing(index);
                }
            }
            return false;
        }

        public override string GetConfiguration()
        {
            return $"{GetType()}, Factor: {_factor}, UpperShadowIndicator: {_upperShadowInd.GetConfiguration()}, AverageShadowIndicator: {_averageUpperShadowInd}";
        }
    }
}
