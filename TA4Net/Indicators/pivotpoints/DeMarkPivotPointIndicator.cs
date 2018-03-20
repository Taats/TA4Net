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
using System.Collections.Generic;
using System.Linq;
using TA4Net.Interfaces;

namespace TA4Net.Indicators.PivotPoints
{
    /**
     * DeMark Pivot Point indicator.
     * <p></p>
     * @see <a href="http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:pivot_points">
     *     http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:pivot_points</a>
     */
    public class DeMarkPivotPointIndicator : RecursiveCachedIndicator<decimal>
    {

        private readonly TimeLevel _timeLevel;

        /**
         * Constructor.
         * <p>
         * Calculates the deMark pivot point based on the time level parameter.
         * @param series the time series with adequate endTime of each bar for the given time level.
         * @param timeLevelId the corresponding time level for pivot calculation:
         *       <ul>
         *          <li>1-, 5-, 10- and 15-Minute charts use the prior days high, low and close: <b>timeLevelId</b> = PIVOT_TIME_LEVEL_ID_DAY (= 1)</li>
         *          <li>30- 60- and 120-Minute charts use the prior week's high, low, and close: <b>timeLevelId</b> =  PIVOT_TIME_LEVEL_ID_WEEK (= 2)</li>
         *          <li>Pivot Points for daily charts use the prior month's high, low and close: <b>timeLevelId</b> = PIVOT_TIME_LEVEL_ID_MONTH (= 3)</li>
         *          <li>Pivot Points for weekly and monthly charts use the prior year's high, low and close: <b>timeLevelId</b> = PIVOT_TIME_LEVEL_ID_YEAR (= 4)</li>
         *          <li> If you want to use just the last bar data: <b>timeLevelId</b> = PIVOT_TIME_LEVEL_ID_BARBASED (= 0)</li>
         *      </ul>
         * The user has to make sure that there are enough previous bars to Calculate correct pivots at the first bar that matters. For example for PIVOT_TIME_LEVEL_ID_MONTH
         * there will be only correct pivot point values (and reversals) after the first complete month
         */
        public DeMarkPivotPointIndicator(ITimeSeries series, TimeLevel timeLevel)
            : base(series)
        {
            _timeLevel = timeLevel;
        }


        protected override decimal Calculate(int index)
        {
            return CalcPivotPoint(GetBarsOfPreviousPeriod(index).ToArray());
        }


        private decimal CalcPivotPoint(int[] barsOfPreviousPeriod)
        {
            if (barsOfPreviousPeriod.isEmpty())
                return Decimals.NaN;

            IBar bar = TimeSeries.GetBar(barsOfPreviousPeriod[0]);
            decimal open = TimeSeries.GetBar(barsOfPreviousPeriod[barsOfPreviousPeriod.Count() - 1]).OpenPrice;
            decimal close = bar.ClosePrice;
            decimal high = bar.MaxPrice;
            decimal low = bar.MinPrice;

            foreach (int i in barsOfPreviousPeriod)
            {
                high = (TimeSeries.GetBar(i).MaxPrice).Max(high);
                low = (TimeSeries.GetBar(i).MinPrice).Min(low);
            }

            decimal x;

            if (close.IsLessThan(open))
            {
                x = high.Plus(Decimals.TWO.MultipliedBy(low)).Plus(close);
            }
            else if (close.IsGreaterThan(open))
            {
                x = Decimals.TWO.MultipliedBy(high).Plus(low).Plus(close);
            }
            else
            {
                x = high.Plus(low).Plus(Decimals.TWO.MultipliedBy(close));
            }

            return x.DividedBy(4);
        }

        /**
         * Calculates the indices of the bars of the previous period
         * @param index index of the current bar
         * @return list of indices of the bars of the previous period
         */
        public List<int> GetBarsOfPreviousPeriod(int index)
        {
            List<int> previousBars = new List<int>();

            if (_timeLevel == TimeLevel.BARBASED)
            {
                previousBars.Add(Math.Max(0, index - 1));
                return previousBars;
            }
            if (index == 0)
            {
                return previousBars;
            }

            IBar currentBar = TimeSeries.GetBar(index);

            // step back while bar-1 in same period (day, week, etc):
            while (index - 1 >= TimeSeries.GetBeginIndex() && GetPeriod(TimeSeries.GetBar(index - 1)) == GetPeriod(currentBar))
            {
                index--;
            }

            // index = last bar in same period, index-1 = first bar in previous period
            long previousPeriod = GetPreviousPeriod(currentBar, index - 1);
            while (index - 1 >= TimeSeries.GetBeginIndex() && GetPeriod(TimeSeries.GetBar(index - 1)) == previousPeriod)
            { // while bar-n in previous period
                index--;
                previousBars.Add(index);
            }
            return previousBars;
        }

        private long GetPreviousPeriod(IBar bar, int indexOfPreviousBar)
        {
            switch (_timeLevel)
            {
                case TimeLevel.DAY: // return previous day
                    int prevCalendarDay = bar.EndTime.AddDays( -1).DayOfYear;
                    // skip weekend and holidays:
                    while (TimeSeries.GetBar(indexOfPreviousBar).EndTime.DayOfYear != prevCalendarDay && indexOfPreviousBar > 0)
                    {
                        prevCalendarDay--;
                    }
                    return prevCalendarDay;
                case TimeLevel.WEEK: // return previous week
                    return bar.EndTime.AddDays(-7).GetIso8601WeekOfYear();
                case TimeLevel.MONTH: // return previous month
                    return bar.EndTime.AddMonths(-1).Month;
                default: return bar.EndTime.AddYears( -1).Year;
            }
        }

        private long GetPeriod(IBar bar)
        {
            switch (_timeLevel)
            {
                case TimeLevel.DAY:
                    return bar.EndTime.DayOfYear;
                    //throw new NotImplementedException("Bugged, see github");
                case TimeLevel.WEEK: 
                    return bar.EndTime.GetIso8601WeekOfYear();
                case TimeLevel.MONTH:
                    return bar.EndTime.Month;
                default:
                    return bar.EndTime.Year;
            }
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, TimeLevel: {_timeLevel}";
        }
    }
}
