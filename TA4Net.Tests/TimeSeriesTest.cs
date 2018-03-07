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
namespace TA4Net.Test
{
    using TA4Net;
   using TA4Net.Trading.Rules;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TA4Net.Mocks;
    using System;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class TimeSeriesTest
    {

        private ITimeSeries defaultSeries;
        private ITimeSeries constrainedSeries;
        private ITimeSeries emptySeries;
        private List<IBar> bars;

        private string _name;

        [TestInitialize]
        public void setUp()
        {
            bars = new List<IBar>();
            bars.Add(new MockBar(new DateTime(2014, 6, 13, 0, 0, 0, 0), 1M));
            bars.Add(new MockBar(new DateTime(2014, 6, 14, 0, 0, 0, 0), 2M));
            bars.Add(new MockBar(new DateTime(2014, 6, 15, 0, 0, 0, 0), 3M));
            bars.Add(new MockBar(new DateTime(2014, 6, 20, 0, 0, 0, 0), 4M));
            bars.Add(new MockBar(new DateTime(2014, 6, 25, 0, 0, 0, 0), 5M));
            bars.Add(new MockBar(new DateTime(2014, 6, 30, 0, 0, 0, 0), 6M));

            _name = "Series Name";

            defaultSeries = new BaseTimeSeries(_name, bars);

            constrainedSeries = new BaseTimeSeries(defaultSeries, 2, 4);
            emptySeries = new BaseTimeSeries();

            IStrategy strategy = new BaseStrategy(new FixedRule(0, 2, 3, 6), new FixedRule(1, 4, 7, 8));
            strategy.SetUnstablePeriod(2); // Strategy would need a real test class
        }

        [TestMethod]
        public void getEndGetBeginGetBarCountIsEmpty()
        {

            //  series
            Assert.AreEqual(0, defaultSeries.GetBeginIndex());
            Assert.AreEqual(bars.Count - 1, defaultSeries.GetEndIndex());
            Assert.AreEqual(bars.Count, defaultSeries.GetBarCount());
            Assert.IsFalse(defaultSeries.IsEmpty());
            // Constrained series
            Assert.AreEqual(2, constrainedSeries.GetBeginIndex());
            Assert.AreEqual(4, constrainedSeries.GetEndIndex());
            Assert.AreEqual(3, constrainedSeries.GetBarCount());
            Assert.IsFalse(constrainedSeries.IsEmpty());
            // Empty series
            Assert.AreEqual(-1, emptySeries.GetBeginIndex());
            Assert.AreEqual(-1, emptySeries.GetEndIndex());
            Assert.AreEqual(0, emptySeries.GetBarCount());
            Assert.IsTrue(emptySeries.IsEmpty());
        }

        [TestMethod]
        public void getBarData()
        {
            //  series
            Assert.AreEqual(bars, defaultSeries.GetBarData());
            // Constrained series
            Assert.AreEqual(bars.Count, constrainedSeries.GetBarData().Count);
            Assert.AreEqual(bars[0], constrainedSeries.GetBarData()[0]);
            // Empty series
            Assert.AreEqual(0, emptySeries.GetBarData().Count);
        }

        [TestMethod]
        public void Name()
        {
            Assert.AreEqual(_name, defaultSeries.Name);
            Assert.AreEqual(_name, constrainedSeries.Name);
        }

        [TestMethod]
        public void getBarWithRemovedIndexOnMovingSeriesShouldReturnFirstRemainingBar()
        {
            IBar bar = defaultSeries.GetBar(4);
            defaultSeries.SetMaximumBarCount(2);

            Assert.AreEqual(bar, defaultSeries.GetBar(0));
            Assert.AreEqual(bar, defaultSeries.GetBar(1));
            Assert.AreEqual(bar, defaultSeries.GetBar(2));
            Assert.AreEqual(bar, defaultSeries.GetBar(3));
            Assert.AreEqual(bar, defaultSeries.GetBar(4));
            Assert.AreNotEqual(bar, defaultSeries.GetBar(5));
        }

        public void getBarOnMovingAndEmptySeriesShouldThrowException()
        {
            defaultSeries.SetMaximumBarCount(2);
            Assert.ThrowsException<NotSupportedException>(() => bars.Clear()); // Should not be used like this
            defaultSeries.GetBar(1);
        }

        public void getBarWithNegativeIndexShouldThrowException()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => defaultSeries.GetBar(-1));
        }

        public void getBarWithIndexGreaterThanBarCountShouldThrowException()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => defaultSeries.GetBar(10));
        }

        [TestMethod]
        public void getBarOnMovingSeries()
        {
            IBar bar = defaultSeries.GetBar(4);
            defaultSeries.SetMaximumBarCount(2);
            Assert.AreEqual(bar, defaultSeries.GetBar(4));
        }

        [TestMethod]
        public void subSeriesCreation()
        {
            ITimeSeries subSeries = defaultSeries.GetSubSeries(2, 5);
            Assert.AreEqual(defaultSeries.Name, subSeries.Name);
            Assert.AreEqual(0, subSeries.GetBeginIndex());
            Assert.AreEqual(defaultSeries.GetBeginIndex(), subSeries.GetBeginIndex());
            Assert.AreEqual(2, subSeries.GetEndIndex());
            Assert.AreNotEqual(defaultSeries.GetEndIndex(), subSeries.GetEndIndex());
            Assert.AreEqual(3, subSeries.GetBarCount());

            subSeries = defaultSeries.GetSubSeries(-1000, 1000);
            Assert.AreEqual(0, subSeries.GetBeginIndex());
            Assert.AreEqual(defaultSeries.GetEndIndex(), subSeries.GetEndIndex());
        }

        public void SubseriesWithWrongArguments()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => defaultSeries.GetSubSeries(10, 9));
        }


        public void maximumBarCountOnConstrainedSeriesShouldThrowException()
        {
            Assert.ThrowsException<NotSupportedException>(() => constrainedSeries.SetMaximumBarCount(10));
        }

        public void negativeMaximumBarCountShouldThrowException()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => defaultSeries.SetMaximumBarCount(-1));
        }

        [TestMethod]
        public void setMaximumBarCount()
        {
            // Before
            Assert.AreEqual(0, defaultSeries.GetBeginIndex());
            Assert.AreEqual(bars.Count - 1, defaultSeries.GetEndIndex());
            Assert.AreEqual(bars.Count, defaultSeries.GetBarCount());

            defaultSeries.SetMaximumBarCount(3);

            // After
            Assert.AreEqual(0, defaultSeries.GetBeginIndex());
            Assert.AreEqual(5, defaultSeries.GetEndIndex());
            Assert.AreEqual(3, defaultSeries.GetBarCount());
        }

        public void addNullBarshouldThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => defaultSeries.AddBar(null));
        }

        public void addBarWithEndTimePriorToSeriesEndTimeShouldThrowException()
        {
            Assert.ThrowsException<NotSupportedException>(() => defaultSeries.AddBar(new MockBar(new DateTime(2000, 1, 1, 0, 0, 0, 0), 99M)));
        }

        [TestMethod]
        public void addBar()
        {
            defaultSeries = new BaseTimeSeries();
            IBar firstBar = new MockBar(new DateTime(2014, 6, 13, 0, 0, 0, 0), 1M);
            IBar secondBar = new MockBar(new DateTime(2014, 6, 14, 0, 0, 0, 0), 2M);

            Assert.AreEqual(0, defaultSeries.GetBarCount());
            Assert.AreEqual(-1, defaultSeries.GetBeginIndex());
            Assert.AreEqual(-1, defaultSeries.GetEndIndex());

            defaultSeries.AddBar(firstBar);
            Assert.AreEqual(1, defaultSeries.GetBarCount());
            Assert.AreEqual(0, defaultSeries.GetBeginIndex());
            Assert.AreEqual(0, defaultSeries.GetEndIndex());

            defaultSeries.AddBar(secondBar);
            Assert.AreEqual(2, defaultSeries.GetBarCount());
            Assert.AreEqual(0, defaultSeries.GetBeginIndex());
            Assert.AreEqual(1, defaultSeries.GetEndIndex());
        }
    }
}