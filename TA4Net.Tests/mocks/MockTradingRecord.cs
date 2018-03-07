namespace TA4Net.Mocks
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using TA4Net;

    [TestClass]
    public class MockTradingRecord : BaseTradingRecord
    {

        /*
         * Constructor. Builds a TradingRecord from a list of states. Initial state
         * value is zero. Then at each index where the state value changes, the
         * TradingRecord operates at that index.
         * 
         * @param states List<decimal> of state values
         */
        public MockTradingRecord(List<decimal> states)
            : base()
        {
            decimal lastState = 0M;
            for (int i = 0; i < states.Count; i++)
            {
                decimal state = states[i];
                if (state != lastState)
                {
                    Operate(i);
                }
                lastState = state;
            }
        }
    }
}