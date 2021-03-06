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
using TA4Net.Indicators.Helpers;
using TA4Net.Interfaces;

namespace TA4Net.Indicators
{
    /**
     * Coppock Curve indicator.
     * <p></p>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:coppock_curve">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:coppock_curve</a>
     */
    public class CoppockCurveIndicator : CachedIndicator<decimal>
    {
        private readonly WMAIndicator _wma;

        /**
          * Constructor with  values: <br/>
          * - longRoCTimeFrame=14 <br/>
          * - shortRoCTimeFrame=11 <br/>
          * - wmaTimeFrame=10
          * 
          * @param indicator the indicator
        */
        public CoppockCurveIndicator(IIndicator<decimal> indicator)
            : this(indicator, 14, 11, 10)
        {
        }

        /**
         * Constructor.
         * @param indicator the indicator (usually close price)
         * @param longRoCTimeFrame the time frame for long term RoC
         * @param shortRoCTimeFrame the time frame for short term RoC
         * @param wmaTimeFrame the time frame (for WMA)
         */
        public CoppockCurveIndicator(IIndicator<decimal> indicator, int longRoCTimeFrame, int shortRoCTimeFrame, int wmaTimeFrame)
            : base(indicator)
        {
            SumIndicator sum = new SumIndicator(
                    new ROCIndicator(indicator, longRoCTimeFrame),
                    new ROCIndicator(indicator, shortRoCTimeFrame)
            );
            _wma = new WMAIndicator(sum, wmaTimeFrame);
        }

        protected override decimal Calculate(int index)
        {
            return _wma.GetValue(index);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, WMAIndicator: {_wma.GetConfiguration()}";
        }
    }
}
