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
namespace TA4Net
{

    /**
     * Immutable, arbitrary-precision signed decimal numbers designed for technical analysis.
     * <p>
     * A {@code decimal} consists of a {@code Bigdecimal} with arbitrary {@link MathContext} (precision and rounding mode).
     *
     * @see Bigdecimal
     * @see MathContext
     * @see RoundingMode
     */
    public static class Decimals
    {
        /** Not-a-Number instance (infinite error) */
        public static decimal NaN = decimal.MaxValue;
        public static readonly decimal MINUSONE = -1;
        public static readonly decimal Zero = 0;
        public static readonly decimal ONE = 1;
        public static readonly decimal TWO = 2;
        public static readonly decimal THREE = 3;
        public static readonly decimal FOUR = 4;
        public static readonly decimal TEN = 10;
        public static readonly decimal HUNDRED = 100;
        public static readonly decimal THOUSAND = 1000;

        public static decimal ZERO { get; internal set; }
    }
}