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
using System;
using System.Collections.Generic;
using TA4Net.Interfaces;

namespace TA4Net.Indicators
{
    /**
     * Cached {@link Indicator indicator}.
     * <p></p>
     * Caches the constructor of the indicator. Avoid to Calculate the same index of the indicator twice.
     */
    public abstract class CachedIndicator<T> : AbstractIndicator<T>
    {

        /** List of cached results */
        private readonly List<T> _results = new List<T>();

        /**
         * Should always be the index of the last result in the results list.
         * I.E. the last Calculated result.
         */
        protected int HighestResultIndex { get; set; }

        /**
         * Constructor.
         * @param series the related time series
         */
        public CachedIndicator(ITimeSeries series)
            : base(series)
        {
            HighestResultIndex = -1;
        }

        /**
         * Constructor.
         * @param indicator a related indicator (with a time series)
         */
        public CachedIndicator(IIndicator<T> indicator)
            : this(indicator.TimeSeries)
        {
            HighestResultIndex = -1;
        }

        public override T GetValue(int index)
        {
            ITimeSeries series = TimeSeries;
            if (series == null)
            {
                // Series is null; the indicator doesn't need cache.
                // (e.g. simple computation of the value)
                // --> Calculating the value
                return Calculate(index);
            }

            // Series is not null

            int removedTicksCount = series.GetRemovedBarsCount();
            int maximumResultCount = series.GetMaximumBarCount();

            T result;
            if (index < removedTicksCount)
            {
                // Result already removed from cache
                increaseLengthTo(removedTicksCount, maximumResultCount);
                HighestResultIndex = removedTicksCount;
                result = _results[0];
                if (Equals(result, default(T)))
                {
                    // It should be "result = calculate(removedTicksCount);".
                    // We use "result = calculate(0);" as a workaround
                    // to fix issue #120 (https://github.com/mdeverdelhan/ta4j/issues/120).
                    result = Calculate(0);
                    _results[0] = result;
                }
            }
            else
            {
                increaseLengthTo(index, maximumResultCount);
                if (index > HighestResultIndex)
                {
                    // Result not calculated yet
                    HighestResultIndex = index;
                    result = Calculate(index);
                    _results[_results.Count - 1] = result;
                }
                else
                {
                    // Result covered by current cache
                    int resultInnerIndex = _results.Count - 1 - (HighestResultIndex - index);
                    result = _results[resultInnerIndex];
                    if (Equals(result, default(T)))
                    {
                        result = Calculate(index);
                    }
                    _results[resultInnerIndex] = result;
                }
            }
            return result;
        }

        /**
         * @param index the bar index
         * @return the value of the indicator
         */
        protected abstract T Calculate(int index);

        /**
         * Increases the size of cached results buffer.
         * @param index the index to increase Length to
         * @param MaxLength the Maximum Length of the results buffer
         */
        private void increaseLengthTo(int index, int MaxLength)
        {
            if (HighestResultIndex > -1)
            {
                int newResultsCount = Math.Min(index - HighestResultIndex, MaxLength);
                if (newResultsCount == MaxLength)
                {
                    _results.Clear();
                    _results.AddRange(new T[MaxLength]);
                }
                else if (newResultsCount > 0)
                {
                    _results.AddRange(new T[newResultsCount]);
                    removeExceedingResults(MaxLength);
                }
            }
            else
            {
                // First use of cache
                if (!_results.isEmpty()) {
                    throw new Exception("Cache results list should be empty");
                }
                _results.AddRange(new T[Math.Min(index + 1, MaxLength)]);
            }
        }

        /**
         * Removes the N first results which exceed the Maximum bar count.
         * (i.e. keeps only the last MaximumResultCount results)
         * @param MaximumResultCount the number of results to keep
         */
        private void removeExceedingResults(int MaximumResultCount)
        {
            int resultCount = _results.Count;
            if (resultCount > MaximumResultCount)
            {
                // Removing old results
                int nbResultsToRemove = resultCount - MaximumResultCount;
                for (int i = 0; i < nbResultsToRemove; i++) {
                    _results.RemoveAt(0);
                }
            }
        }
    }
}
