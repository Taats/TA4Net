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
using System.Text;
using TA4Net.Interfaces;

namespace TA4Net
{
    /**
     * Base implementation of a {@link TimeSeries}.
     * <p></p>
     */
    public class BaseTimeSeries : ITimeSeries
    {
        /** Name for unnamed series */
        private static readonly string UNNAMED_SERIES_NAME = "unamed_series";
        /** The logger */
        //  private readonly Logger log = LoggerFactory.getLogger(getClass());
        /** Name of the series */
        private readonly string _name;
        /** Begin index of the time series */
        private int _seriesBeginIndex = -1;
        /** End index of the time series */
        private int _seriesEndIndex = -1;
        /** List of bars */
        private readonly List<IBar> _bars;
        /** Maximum number of bars for the time series */
        private int _maximumBarCount = int.MaxValue;
        /** Number of removed bars */
        private int _removedBarsCount = 0;
        /** True if the current series is constrained (i.e. its indexes cannot change), false otherwise */
        private bool _constrained = false;

        /**
         * Constructor of an unnamed series.
         */
        public BaseTimeSeries()
            : this(UNNAMED_SERIES_NAME)
        {
        }

        /**
         * Constructor.
         * @param name the name of the series
         */
        public BaseTimeSeries(string name)
            : this(name, new List<IBar>())
        {
        }

        /**
         * Constructor of an unnamed series.
         * @param bars the list of bars of the series
         */
        public BaseTimeSeries(IList<IBar> bars)
            : this(UNNAMED_SERIES_NAME, bars)
        {
        }

        /**
        * Constructor.
        * <p>
        * Constructs a constrained time series from an original one.
        * @param defaultSeries the original time series to construct a constrained series from
        * @param seriesBeginIndex the begin index (inclusive) of the time series
        * @param seriesEndIndex the end index (inclusive) of the time series
        *
        * @deprecated use {@link #getSubSeries(int, int) getSubSeries(startIndex, endIndex)} to satisfy correct behaviour of
        * the new sub series in further calculations
        */
        [Obsolete]
        public BaseTimeSeries(ITimeSeries defaultSeries, int seriesBeginIndex, int seriesEndIndex)
            : this(defaultSeries.Name, defaultSeries.GetBarData().ToList(), seriesBeginIndex, seriesEndIndex, true)
        {
            if (defaultSeries.GetBarData() == null || defaultSeries.GetBarData().isEmpty())
            {
                throw new ArgumentException("Cannot create a constrained series from a time series with a null/empty list of bars");
            }
            if (defaultSeries.GetMaximumBarCount() != int.MaxValue)
            {
                throw new ArgumentException("Cannot create a constrained series from a time series for which a maximum bar count has been set");
            }
        }

        /**
         * Constructor.
         * @param name the name of the series
         * @param bars the list of bars of the series
         */
        public BaseTimeSeries(string name, IList<IBar> bars)
            : this(name, bars, 0, bars.Count - 1, false)
        {
        }

        /**
         * Constructor.
         * @param name the name of the series
         * @param bars the list of bars of the series
         * @param seriesBeginIndex the begin index (inclusive) of the time series
         * @param seriesEndIndex the end index (inclusive) of the time series
         * @param constrained true to constrain the time series (i.e. indexes cannot change), false otherwise
         */
        private BaseTimeSeries(string name, IList<IBar> bars, int seriesBeginIndex, int seriesEndIndex, bool constrained)
        {
            _bars = bars as List<IBar> ?? bars.ToList();
            _name = name;
            if (bars.isEmpty())
            {
                // Bar list empty
                seriesBeginIndex = -1;
                seriesEndIndex = -1;
                constrained = false;
                return;
            }
            // Bar list not empty: checking indexes
            if (seriesEndIndex < seriesBeginIndex - 1)
            {
                throw new ArgumentException("End index must be >= to begin index - 1");
            }
            if (seriesEndIndex >= bars.Count)
            {
                throw new ArgumentException("End index must be < to the bar list size");
            }
            _seriesBeginIndex = seriesBeginIndex;
            _seriesEndIndex = seriesEndIndex;
            _constrained = constrained;
        }

        /**
         * Returns a new BaseTimeSeries that is a subset of this BaseTimeSeries.
         * The new series holds a copy of all {@link Bar bars} between <tt>startIndex</tt> (inclusive) and <tt>endIndex</tt> (exclusive)
         * of this TimeSeries.
         * The indices of this TimeSeries and the new subset TimeSeries can be different. I. e. index 0 of the new TimeSeries will
         * be index <tt>startIndex</tt> of this TimeSeries.
         * If <tt>startIndex</tt> < seriesBeginIndex the new TimeSeries will start with the first available Bar of this TimeSeries.
         * If <tt>endIndex</tt> > seriesEndIndex+1 the new TimeSeries will end at the last available Bar of this TimeSeries
         * @param startIndex the startIndex
         * @param endIndex the endIndex (exclusive)
         * @return a new BaseTimeSeries with Bars from <tt>startIndex</tt> to <tt>endIndex</tt>-1
         * @throws ArgumentException if <tt>endIndex</tt> < <tt>startIndex</tt>
         */

        public ITimeSeries GetSubSeries(int startIndex, int endIndex)
        {
            if (startIndex > endIndex)
            {
                throw new ArgumentException
                        (string.Format("the endIndex: %s must be bigger than startIndex: %s", endIndex, startIndex));
            }
            if (!_bars.isEmpty())
            {
                int start = Math.Max(startIndex, _seriesBeginIndex);
                int end = Math.Min(endIndex, _seriesEndIndex + 1);
                return new BaseTimeSeries(Name, Cut(_bars, start, end));
            }
            return new BaseTimeSeries(_name);

        }


        public string Name
        {
            get { return _name; }
        }


        public IBar GetBar(int i)
        {
            int innerIndex = i - _removedBarsCount;
            if (innerIndex < 0)
            {
                if (i < 0)
                {
                    // Cannot return the i-th bar if i < 0
                    throw new IndexOutOfRangeException(BuildOutOfBoundsMessage(this, i));
                }
                // log.trace("Time series `{}` ({} bars): bar {} already removed, use {}-th instead", name, bars.Count, i, removedBarsCount);
                if (_bars.isEmpty())
                {
                    throw new IndexOutOfRangeException(BuildOutOfBoundsMessage(this, _removedBarsCount));
                }
                innerIndex = 0;
            }
            else if (innerIndex >= _bars.Count)
            {
                // Cannot return the n-th bar if n >= bars.Count
                throw new IndexOutOfRangeException(BuildOutOfBoundsMessage(this, i));
            }
            return _bars[innerIndex];
        }


        public int GetBarCount()
        {
            if (_seriesEndIndex < 0)
            {
                return 0;
            }
            int startIndex = Math.Max(_removedBarsCount, _seriesBeginIndex);
            return _seriesEndIndex - startIndex + 1;
        }


        public IReadOnlyList<IBar> GetBarData()
        {
            return _bars;
        }


        public int GetBeginIndex()
        {
            return _seriesBeginIndex;
        }


        public int GetEndIndex()
        {
            return _seriesEndIndex;
        }


        public void SetMaximumBarCount(int MaximumBarCount)
        {
            if (_constrained)
            {
                throw new NotSupportedException("Cannot set a Maximum bar count on a constrained time series");
            }
            if (MaximumBarCount <= 0)
            {
                throw new ArgumentException("Maximum bar count must be strictly positive");
            }
            _maximumBarCount = MaximumBarCount;
            RemoveExceedingBars();
        }


        public int GetMaximumBarCount()
        {
            return _maximumBarCount;
        }


        public int GetRemovedBarsCount()
        {
            return _removedBarsCount;
        }


        public void AddBar(IBar bar)
        {
            if (bar == null)
            {
                throw new ArgumentException("Cannot add null bar");
            }

            if (!_bars.isEmpty())
            {
                int lastBarIndex = _bars.Count - 1;
                DateTime seriesEndTime = _bars[lastBarIndex].EndTime;
                if (!bar.EndTime.IsAfter(seriesEndTime))
                {
                    throw new ArgumentException("Cannot add a bar with end time <= to series end time");
                }
            }

            _bars.Add(bar);
            if (_seriesBeginIndex == -1)
            {
                // Begin index set to 0 only if if wasn't initialized
                _seriesBeginIndex = 0;
            }
            _seriesEndIndex++;
            RemoveExceedingBars();
        }

        /**
         * @return the first bar of the series
         */
        public IBar GetFirstBar()
        {
            return GetBar(GetBeginIndex());
        }

        /**
         * @return the last bar of the series
         */
        public IBar GetLastBar()
        {
            return GetBar(GetEndIndex());
        }

        /**
         * @return true if the series is empty, false otherwise
         */
        public bool IsEmpty()
        {
            return GetBarCount() == 0;
        }

        /**
    * @return the description of the series period (e.g. "from 12:00 21/01/2014 to 12:15 21/01/2014")
    */
        public string GetSeriesPeriodDescription()
        {
            StringBuilder sb = new StringBuilder();
            if (!GetBarData().isEmpty())
            {
                IBar firstBar = GetFirstBar();
                IBar lastBar = GetLastBar();
                sb.Append(firstBar.EndTime.ToString())
                        .Append(" - ")
                        .Append(lastBar.EndTime.ToString());
            }
            return sb.ToString();
        }

        /**
         * Removes the N first bars which exceed the Maximum bar count.
         */
        private void RemoveExceedingBars()
        {
            int barCount = _bars.Count;
            if (barCount > _maximumBarCount)
            {
                // Removing old bars
                int nbBarsToRemove = barCount - _maximumBarCount;
                for (int i = 0; i < nbBarsToRemove; i++)
                {
                    _bars.RemoveAt(0);
                }
                // Updating removed bars count
                _removedBarsCount += nbBarsToRemove;
            }
        }

        /**
         * Cuts a list of bars into a new list of bars that is a subset of it
         * @param bars the list of {@link Bar bars}
         * @param startIndex start index of the subset
         * @param endIndex end index of the subset
         * @return a new list of bars with tick from startIndex (inclusive) to endIndex (exclusive)
         */
        private static List<IBar> Cut(IList<IBar> bars, int startIndex, int endIndex)
        {
            return new List<IBar>(bars.Skip(startIndex).Take(endIndex - startIndex));
        }

        /**
         * @param series a time series
         * @param index an out of bounds bar index
         * @return a message for an OutOfBoundsException
         */
        private static string BuildOutOfBoundsMessage(BaseTimeSeries series, int index)
        {
            //return "Size of series: " + series.bars.Count + " bars, "
            //        + series.RemoveAtdBarsCount + " bars removed, index = " + index;

            return string.Empty;
        }
    }
}
