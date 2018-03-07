namespace TA4Net.Mocks
{
    using TA4Net;
    using TA4Net.Analysis.Criteria;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using TA4Net.Interfaces;

    [TestClass]
    public class MockAbstractAnalysisCriterion : AbstractAnalysisCriterion
    {

        ITimeSeries series;
        List<decimal> values;

        /**
         * Constructor.
         * 
         * @param series TimeSeries of the AbstractAnalysisCriterion
         * @param values AbstractAnalysisCriterion values
         */
        public MockAbstractAnalysisCriterion(ITimeSeries series, List<decimal> values)
        {
            this.series = series;
            this.values = values;
        }

        /**
         * Gets the readonly criterion value.
         * 
         * @param series TimeSeries is ignored
         * @param trade is ignored
         */
        public override decimal Calculate(ITimeSeries series, Trade trade)
        {
            return values[values.Count - 1];
        }

        /**
         * Gets the readonly criterion value.
         * 
         * @param series TimeSeries is ignored
         * @param tradingRecord is ignored
         */
        public override decimal Calculate(ITimeSeries series, ITradingRecord tradingRecord)
        {
            return values[values.Count - 1];
        }

        /**
         * Compares two criterion values and returns true if first value is greater
         * than second value, false otherwise.
         * 
         * @param criterionValue1 first value
         * @param criterionValue2 second value
         * @return bool indicating first value is greater than second value
         */
        public override bool BetterThan(decimal criterionValue1, decimal criterionValue2)
        {
            return (criterionValue1 > criterionValue2);
        }
    }
}