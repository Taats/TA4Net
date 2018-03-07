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
using TA4Net;
using TA4Net.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TA4Net.Interfaces;

namespace TA4Net.Test
{
    [TestClass]
    public class BarTest
    {

        private IBar bar;

        private DateTime beginTime;

        private DateTime endTime;

        [TestInitialize]
        public void setUp()
        {
            beginTime = new DateTime(2014, 6, 25, 0, 0, 0, 0);
            endTime = new DateTime(2014, 6, 25, 1, 0, 0, 0);
            bar = new BaseBar(new TimeSpan(1, 0, 0), endTime);
        }

        [TestMethod]
        public void addTrades()
        {

            bar.AddTrade(3.0M, 200.0M);
            bar.AddTrade(4.0M, 201.0M);
            bar.AddTrade(2.0M, 198.0M);

            Assert.AreEqual(3, bar.NumberOfTrades);
            Assert.AreEqual(bar.Amount, 3 * 200 + 4 * 201 + 2 * 198);
            Assert.AreEqual(bar.OpenPrice, 200);
            Assert.AreEqual(bar.ClosePrice, 198);
            Assert.AreEqual(bar.MinPrice, 198);
            Assert.AreEqual(bar.MaxPrice, 201);
            Assert.AreEqual(bar.Volume, 9);
        }

        [TestMethod]
        public void TimePeriod()
        {
            Assert.AreEqual(beginTime, bar.EndTime - (bar.TimePeriod));
        }

        [TestMethod]
        public void BeginTime()
        {
            Assert.AreEqual(beginTime, bar.BeginTime);
        }

        [TestMethod]
        public void inPeriod()
        {
            //   Assert.IsFalse(bar.inPeriod(null));

            Assert.IsFalse(bar.InPeriod(beginTime.withDayOfMonth(24)));
            Assert.IsFalse(bar.InPeriod(beginTime.withDayOfMonth(26)));
            Assert.IsTrue(bar.InPeriod(beginTime.withMinute(30)));

            Assert.IsTrue(bar.InPeriod(beginTime));
            Assert.IsFalse(bar.InPeriod(endTime));
        }
    }
}
