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
using TA4Net.Extensions;
using TA4Net.Indicators.Helpers;
using TA4Net.Interfaces;

namespace TA4Net.Indicators
{
    /**
     * Parabolic SAR indicator.
     * team172011(Simon-Justus Wimmer), 18.09.2017
     */
    public class ParabolicSarIndicator : RecursiveCachedIndicator<decimal>
    {

        private decimal _accelerationFactor;
        private readonly decimal _maxAcceleration;
        private readonly decimal _accelerationIncrement;
        private readonly decimal _accelarationStart;
        private readonly ITimeSeries _series;

        private bool _currentTrend; // true if uptrend, false otherwise
        private int _startTrendIndex = 0; // index of start bar of the current trend
        private MinPriceIndicator _minPriceIndicator;
        private MaxPriceIndicator _maxPriceIndicator;
        private decimal _currentExtremePoint; // the extreme point of the current calculation
        private decimal _minMaxExtremePoint; // depending on trend the Maximum or Minimum extreme point value of trend

        /**
         * Constructor with  parameters
         * @param series the time series for this indicator
         */
        public ParabolicSarIndicator(ITimeSeries series)
            : this(series, 0.02M, 0.2M, 0.02M)
        {

        }

        /**
         * Constructor with custom parameters and  increment value
         * @param series the time series for this indicator
         * @param aF acceleration factor
         * @param MaxA Maximum acceleration
         */
        public ParabolicSarIndicator(ITimeSeries series, decimal aF, decimal MaxA)
           : this(series, aF, MaxA, 0.02M)
        {
        }

        /**
         * Constructor with custom parameters
         * @param series the time series for this indicator
         * @param aF acceleration factor
         * @param MaxA Maximum acceleration
         * @param increment the increment step
         */
        public ParabolicSarIndicator(ITimeSeries series, decimal aF, decimal MaxA, decimal increment)
            : base(series)
        {
            _series = series;
            _maxPriceIndicator = new MaxPriceIndicator(series);
            _minPriceIndicator = new MinPriceIndicator(series);
            _maxAcceleration = MaxA;
            _accelerationFactor = aF;
            _accelerationIncrement = increment;
            _accelarationStart = aF;
        }


        protected override decimal Calculate(int index)
        {
            decimal sar = Decimals.NaN;
            if (index == _series.GetBeginIndex())
            {
                return sar; // no trend detection possible for the first value
            }
            else if (index == _series.GetBeginIndex() + 1)
            {// start trend detection
                _currentTrend = _series.GetBar(_series.GetBeginIndex()).ClosePrice.IsLessThan(_series.GetBar(index).ClosePrice);
                if (!_currentTrend)
                { // down trend
                    sar = _maxPriceIndicator.GetValue(index); // put sar on Max price of candlestick
                    _currentExtremePoint = sar;
                    _minMaxExtremePoint = _currentExtremePoint;
                }
                else
                { // up trend
                    sar = _minPriceIndicator.GetValue(index); // put sar on Min price of candlestick
                    _currentExtremePoint = sar;
                    _minMaxExtremePoint = _currentExtremePoint;

                }
                return sar;
            }

            decimal priorSar = GetValue(index - 1);
            if (_currentTrend)
            { // if up trend
                sar = priorSar.Plus(_accelerationFactor.MultipliedBy((_currentExtremePoint.Minus(priorSar))));
                _currentTrend = _minPriceIndicator.GetValue(index).IsGreaterThan(sar);
                if (!_currentTrend)
                { // check if sar touches the Min price
                    sar = _minMaxExtremePoint; // sar starts at the highest extreme point of previous up trend
                    _currentTrend = false; // switch to down trend and reset values
                    _startTrendIndex = index;
                    _accelerationFactor = _accelarationStart;
                    _currentExtremePoint = _series.GetBar(index).MinPrice; // put point on Max
                    _minMaxExtremePoint = _currentExtremePoint;
                }
                else
                { // up trend is going on
                    _currentExtremePoint = new HighestValueIndicator(_maxPriceIndicator, index - _startTrendIndex).GetValue(index);
                    if (_currentExtremePoint.IsGreaterThan(_minMaxExtremePoint))
                    {
                        IncrementAcceleration();
                        _minMaxExtremePoint = _currentExtremePoint;
                    }

                }
            }
            else
            { // downtrend
                sar = priorSar.Minus(_accelerationFactor.MultipliedBy(((priorSar.Minus(_currentExtremePoint)))));
                _currentTrend = _maxPriceIndicator.GetValue(index).IsGreaterThanOrEqual(sar);
                if (_currentTrend)
                { // check if switch to up trend
                    sar = _minMaxExtremePoint; // sar starts at the lowest extreme point of previous down trend
                    _accelerationFactor = _accelarationStart;
                    _startTrendIndex = index;
                    _currentExtremePoint = _series.GetBar(index).MaxPrice;
                    _minMaxExtremePoint = _currentExtremePoint;
                }
                else
                { // down trend io going on
                    _currentExtremePoint = new LowestValueIndicator(_minPriceIndicator, index - _startTrendIndex).GetValue(index);
                    if (_currentExtremePoint.IsLessThan(_minMaxExtremePoint))
                    {
                        IncrementAcceleration();
                        _minMaxExtremePoint = _currentExtremePoint;
                    }
                }
            }
            return sar;

        }

        /**
         * Increments the acceleration factor.
         */
        private void IncrementAcceleration()
        {
            if (_accelerationFactor.IsGreaterThanOrEqual(_maxAcceleration))
            {
                _accelerationFactor = _maxAcceleration;
            }
            else
            {
                _accelerationFactor = _accelerationFactor.Plus(_accelerationIncrement);
            }
        }

        public override string GetConfiguration()
        {
            return $" {GetType()}, AccelarationStart: {_accelarationStart}, AccelarationFactor: {_accelerationFactor}, AccelarationIncrement: {_accelerationIncrement}, MaxAccelaration: {_maxAcceleration}, MinMaxExtremePoint: {_minMaxExtremePoint}, StartTrentIndex: {_startTrendIndex}, MaxPriceIndicator: {_maxPriceIndicator.GetConfiguration()}, MinPriceIndicator: {_minPriceIndicator.GetConfiguration()}";
        }
    }
}