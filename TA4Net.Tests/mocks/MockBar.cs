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
namespace TA4Net.Mocks
{
    using System;
    using TA4Net;
    using TA4Net.Interfaces;

    /**
     * A mock bar with sample data.
     */
    public class MockBar : BaseBar, IBar
    {

        private decimal amount = Decimals.ZERO;

        private int trades = 0;

        public MockBar(decimal closePrice)
            : this(DateTime.Now, closePrice)
        {
        }

        public MockBar(decimal closePrice, decimal volume)
            : base(DateTime.Now, 0, 0, 0, closePrice, volume)
        {
        }

        public MockBar(DateTime endTime, decimal closePrice)
            : base(endTime, 0, 0, 0, closePrice, 0)
        {
        }

        public MockBar(decimal openPrice, decimal closePrice, decimal maxPrice, decimal minPrice)
            : base(DateTime.Now, openPrice, maxPrice, minPrice, closePrice, 1)
        {
        }

        public MockBar(decimal openPrice, decimal closePrice, decimal maxPrice, decimal minPrice, decimal volume)
            : base(DateTime.Now, openPrice, maxPrice, minPrice, closePrice, volume)
        {
        }

        public MockBar(DateTime endTime, decimal openPrice, decimal closePrice, decimal maxPrice, decimal minPrice, decimal amount, decimal volume, int trades)
            : base(endTime, openPrice, maxPrice, minPrice, closePrice, volume)
        {
            this.amount = amount;
            this.trades = trades;
        }
    }
}
