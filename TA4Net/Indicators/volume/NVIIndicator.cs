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

namespace TA4Net.Indicators.Volume
{
    /**
     * Negative Volume Index (NVI) indicator.
     * <p></p>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:negative_volume_inde">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:negative_volume_inde</a>
     * @see <a href="http://www.metastock.com/Customer/Resources/TAAZ/.aspx?p=75">
     *     http://www.metastock.com/Customer/Resources/TAAZ/.aspx?p=75</a>
     * @see <a href="http://www.investopedia.com/terms/n/nvi.asp">
     *     http://www.investopedia.com/terms/n/nvi.asp</a>
     */
    public class NVIIndicator : RecursiveCachedIndicator<decimal>
    {
        private readonly ITimeSeries _series;

        public NVIIndicator(ITimeSeries series)
            : base(series)
        {
            _series = series;
        }


        protected override decimal Calculate(int index)
        {
            if (index == 0)
            {
                return Decimals.THOUSAND;
            }

            IBar currentBar = _series.GetBar(index);
            IBar previousBar = _series.GetBar(index - 1);
            decimal previousValue = GetValue(index - 1);

            if (currentBar.Volume.IsLessThan(previousBar.Volume))
            {
                decimal currentPrice = currentBar.ClosePrice;
                decimal previousPrice = previousBar.ClosePrice;
                decimal priceChangeRatio = currentPrice.Minus(previousPrice).DividedBy(previousPrice);
                return previousValue.Plus(priceChangeRatio.MultipliedBy(previousValue));
            }
            return previousValue;
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}";
        }
    }
}