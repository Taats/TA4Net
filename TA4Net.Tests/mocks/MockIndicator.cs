namespace TA4Net.Mocks
{
    using TA4Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class MockIndicator : IIndicator<decimal>
    {

        private ITimeSeries _series;
        private List<decimal> _values;

        /**
         * Constructor.
         * 
         * @param series TimeSeries of the Indicator
         * @param values Indicator values
         */
        public MockIndicator(ITimeSeries series, List<decimal> values)
        {
            _series = series;
            _values = values;
        }

        /**
         * Gets a value from the Indicator
         * 
         * @param index Indicator value to get
         * @return decimal Indicator value at index
         */
        public decimal GetValue(int index)
        {
            return _values[index];
        }

        public string GetConfiguration()
        {
            return $"{GetType()}, Values: {string.Join(",", _values.ToArray())}";
        }

        /**
         * Gets the Indicator TimeSeries.
         * 
         * @return TimeSeries of the Indicator
         */
        public ITimeSeries TimeSeries
        {
            get { return _series; }
        }



    }
}
