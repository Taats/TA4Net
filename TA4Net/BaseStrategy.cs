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
using TA4Net.Interfaces;

namespace TA4Net
{
    /**
     * Base implementation of a {@link Strategy}.
     */
    public class BaseStrategy : IStrategy
    {
        /** Name of the strategy */
        private string _name;

        /** The entry rule */
        private IRule _entryRule;

        /** The exit rule */
        private IRule _exitRule;

        /**
         * The unstable period (number of bars).<br>
         * During the unstable period of the strategy any order placement will be cancelled.<br>
         * I.e. no entry/exit signal will be fired before index == unstablePeriod.
         */
        private int _unstablePeriod;

        /**
         * Constructor.
         * @param entryRule the entry rule
         * @param exitRule the exit rule
         */
        public BaseStrategy(IRule entryRule, IRule exitRule)
            : this(null, entryRule, exitRule, 0)
        {
        }

        /**
        * Constructor.
        * @param entryRule the entry rule
        * @param exitRule the exit rule
        * @param unstablePeriod strategy will ignore possible signals at <code>index</code> < <code>unstablePeriod</code>
        */
        public BaseStrategy(IRule entryRule, IRule exitRule, int unstablePeriod)
            : this(null, entryRule, exitRule, unstablePeriod)
        {
        }

        /**
         * Constructor.
         * @param name the name of the strategy
         * @param entryRule the entry rule
         * @param exitRule the exit rule
         */
        public BaseStrategy(string name, IRule entryRule, IRule exitRule)
            : this(name, entryRule, exitRule, 0)
        {
        }

        /**
         * Constructor.
         * @param name the name of the strategy
         * @param entryRule the entry rule
         * @param exitRule the exit rule
         * @param unstablePeriod strategy will ignore possible signals at <code>index</code> < <code>unstablePeriod</code>
         */
        public BaseStrategy(string name, IRule entryRule, IRule exitRule, int unstablePeriod)
        {
            if (entryRule == null || exitRule == null)
            {
                throw new ArgumentException("Rules cannot be null");
            }
            if (unstablePeriod < 0)
            {
                throw new ArgumentException("Unstable period bar count must be >= 0");
            }
            _name = name;
            _entryRule = entryRule;
            _exitRule = exitRule;
            _unstablePeriod = unstablePeriod;
        }


        public string Name
        {
            get { return _name; }
        }


        public IRule EntryRule
        {
            get { return _entryRule; }
        }


        public IRule ExitRule
        {
            get { return _exitRule; }
        }


        public int GetUnstablePeriod()
        {
            return _unstablePeriod;
        }


        public void SetUnstablePeriod(int unstablePeriod)
        {
            _unstablePeriod = unstablePeriod;
        }


        public bool IsUnstableAt(int index)
        {
            return index < _unstablePeriod;
        }

        public IStrategy And(IStrategy strategy)
        {
            string andName = "and(" + _name + "," + strategy.Name + ")";
            int unstable = _unstablePeriod > strategy.GetUnstablePeriod() ? _unstablePeriod : strategy.GetUnstablePeriod();
            return And(andName, strategy, unstable);
        }


        public IStrategy Or(IStrategy strategy)
        {
            string orName = "or(" + _name + "," + strategy.Name + ")";
            int unstable = _unstablePeriod > strategy.GetUnstablePeriod() ? _unstablePeriod : strategy.GetUnstablePeriod();
            return Or(orName, strategy, unstable);
        }


        public IStrategy Opposite()
        {
            return new BaseStrategy("opposite(" + _name + ")", _exitRule, _entryRule, _unstablePeriod);
        }


        public IStrategy And(string name, IStrategy strategy, int unstablePeriod)
        {
            return new BaseStrategy(name, _entryRule.And(strategy.EntryRule), _exitRule.And(strategy.ExitRule), unstablePeriod);
        }


        public IStrategy Or(string name, IStrategy strategy, int unstablePeriod)
        {
            return new BaseStrategy(name, _entryRule.Or(strategy.EntryRule), _exitRule.Or(strategy.ExitRule), unstablePeriod);
        }

        /**
         * @param index the bar index
         * @param tradingRecord the potentially needed trading history
         * @return true to recommend an order, false otherwise (no recommendation)
         */
        public bool ShouldOperate(int index, ITradingRecord tradingRecord)
        {
            Trade trade = tradingRecord.GetCurrentTrade();
            if (trade.IsNew())
            {
                return ShouldEnter(index, tradingRecord);
            }
            else if (trade.IsOpened())
            {
                return ShouldExit(index, tradingRecord);
            }
            return false;
        }

        /**
         * @param index the bar index
         * @return true to recommend to enter, false otherwise
         */
        public bool ShouldEnter(int index)
        {
            return ShouldEnter(index, null);
        }

        /**
         * @param index the bar index
         * @param tradingRecord the potentially needed trading history
         * @return true to recommend to enter, false otherwise
         */
        public bool ShouldEnter(int index, ITradingRecord tradingRecord)
        {
            if (IsUnstableAt(index))
            {
                return false;
            }
            return EntryRule.IsSatisfied(index, tradingRecord);
        }

        /**
         * @param index the bar index
         * @return true to recommend to exit, false otherwise
         */
        public bool ShouldExit(int index)
        {
            return ShouldExit(index, null);
        }

        /**
         * @param index the bar index
         * @param tradingRecord the potentially needed trading history
         * @return true to recommend to exit, false otherwise
         */
        public bool ShouldExit(int index, ITradingRecord tradingRecord)
        {
            if (IsUnstableAt(index))
            {
                return false;
            }
            return ExitRule.IsSatisfied(index, tradingRecord);
        }

        public override string ToString()
        {
            return $"Strategy '{Name}' with {Environment.NewLine} EntryRule: {_entryRule.GetConfiguration()} {Environment.NewLine} ExitRule: {_exitRule.GetConfiguration()}";
        }
    }
}
