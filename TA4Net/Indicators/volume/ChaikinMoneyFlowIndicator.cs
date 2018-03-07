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
using System;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.Volume
{
    /* Chaikin Money Flow(CMF) indicator.
    * <p></p>
    * @see<a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:chaikin_money_flow_cmf">
    *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:chaikin_money_flow_cmf"</a>
    * @see<a href="http://www.fmlabs.com/reference/.htm?url=ChaikinMoneyFlow.htm">
    * http://www.fmlabs.com/reference/.htm?url=ChaikinMoneyFlow.htm</a>
    */
    public class ChaikinMoneyFlowIndicator : CachedIndicator<decimal>
    {
        private readonly ITimeSeries _series;
        private readonly CloseLocationValueIndicator _clvIndicator;
        private readonly VolumeIndicator _volumeIndicator;
        private readonly int _timeFrame;

        public ChaikinMoneyFlowIndicator(ITimeSeries series, int timeFrame)
            : base(series)
        {
            _series = series;
            _timeFrame = timeFrame;
            _clvIndicator = new CloseLocationValueIndicator(series);
            _volumeIndicator = new VolumeIndicator(series, timeFrame);
        }

        protected override decimal Calculate(int index)
        {
            int startIndex = Math.Max(0, index - _timeFrame + 1);
            decimal sumOfMoneyFlowVolume = Decimals.Zero;
            for (int i = startIndex; i <= index; i++)
            {
                sumOfMoneyFlowVolume = sumOfMoneyFlowVolume.Plus(getMoneyFlowVolume(i));
            }
            decimal sumOfVolume = _volumeIndicator.GetValue(index);

            return sumOfMoneyFlowVolume.DividedBy(sumOfVolume);
        }

        /**
         * @param index the bar index
         * @return the money flow volume for the i-th period/bar
         */
        private decimal getMoneyFlowVolume(int index)
        {
            return _clvIndicator.GetValue(index).MultipliedBy(_series.GetBar(index).Volume);
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeFrame: {_timeFrame}, CloseLocationValueIndicator: {_clvIndicator.GetConfiguration()}, VolumeIndicator: {_volumeIndicator.GetConfiguration()}";
        }
    }
}
