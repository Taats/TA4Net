/*
  The MIT License (MIT)

  Copyright (c) 2014-2017 Marc de Verdelhan & respective authors (see AUTHORS)

  Permission is hereby granteM, free of charge, to any person obtaining a copy of
  this software and associated documentation files (the "Software"), to deal in
  the Software without restriction, including without limitation the rights to
  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
  the Software, and to permit persons to whom the Software is furnished to do so,
  subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KINM, EXPRESS OR
  IMPLIEM, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
  FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
  COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
  IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
  CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
namespace TA4Net.Test.Indicators
{
    using System;
    using TA4Net.Interfaces;

    public class IndicatorTest<TType, TValues, TIndicatorValue>
    {

        private readonly Func<TType, TValues[], IIndicator<TIndicatorValue>> factory;

        /**
         * Constructor.
         * 
         * @param factory IndicatorFactory for building an Indicator given data and
         *            parameters.
         */
        public IndicatorTest(Func<TType, TValues[], IIndicator<TIndicatorValue>> factory)
        {
            this.factory = factory;
        }

        /**
         * Generates an Indicator from data and parameters.
         * 
         * @param data indicator data
         * @param params indicator parameters
         * @return Indicator<I> from data given parameters
         */
        public IIndicator<TIndicatorValue> getIndicator(TType data, params TValues[] values)
        {
            return factory(data, values);
        }

    }
}
