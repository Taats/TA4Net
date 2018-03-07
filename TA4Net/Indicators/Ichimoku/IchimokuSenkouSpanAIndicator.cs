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

namespace TA4Net.Indicators.Ichimoku
{
    /**
     * Ichimoku clouds: Senkou Span A (Leading Span A) indicator
     * <p></p>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:ichimoku_cloud">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:ichimoku_cloud</a>
     */
    public class IchimokuSenkouSpanAIndicator : CachedIndicator<decimal>
    {

        /** The Tenkan-sen indicator */
        private readonly IchimokuTenkanSenIndicator _conversionLine;

        /** The Kijun-sen indicator */
        private readonly IchimokuKijunSenIndicator _baseLine;

        /**
         * Constructor.
         * @param series the series
         */
        public IchimokuSenkouSpanAIndicator(ITimeSeries series)
           : this(series, new IchimokuTenkanSenIndicator(series), new IchimokuKijunSenIndicator(series))
        {
        }

        /**
         * Constructor.
         * @param series the series
         * @param timeFrameConversionLine the time frame for the conversion line (usually 9)
         * @param timeFrameBaseLine the time frame for the base line (usually 26)
         */
        public IchimokuSenkouSpanAIndicator(ITimeSeries series, int timeFrameConversionLine, int timeFrameBaseLine)
            : this(series, new IchimokuTenkanSenIndicator(series, timeFrameConversionLine), new IchimokuKijunSenIndicator(series, timeFrameBaseLine))
        {
        }

        /**
         * Constructor.
         * @param series the series
         * @param conversionLine the conversion line
         * @param baseLine the base line
         */
        public IchimokuSenkouSpanAIndicator(ITimeSeries series, IchimokuTenkanSenIndicator conversionLine, IchimokuKijunSenIndicator baseLine)
            : base(series)
        {
            _conversionLine = conversionLine;
            _baseLine = baseLine;
        }


        protected override decimal Calculate(int index)
        {
            return _conversionLine.GetValue(index).Plus(_baseLine.GetValue(index)).DividedBy(Decimals.TWO);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, BaseLineIndicator: {_baseLine.GetConfiguration()}, ConversionLineIndicator: {_conversionLine.GetConfiguration()}";
        }
    }
}
