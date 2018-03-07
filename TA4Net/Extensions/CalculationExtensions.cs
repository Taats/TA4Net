
using DecimalMath;
using System;

namespace TA4Net.Extensions
{
    public static class CalculationExtensions
    {
        public static decimal DividedBy(this decimal value, decimal divider)
        {
            if (value.IsNaN() || divider.IsNaN() ||  divider == 0)
            {
                return Decimals.NaN;
            }
            return value / divider;
        }

        public static decimal MultipliedBy(this decimal value, decimal multiplier)
        {
            if (value.IsNaN() || multiplier.IsNaN())
            {
                return Decimals.NaN;
            }

            return value * multiplier;
        }

        public static decimal MultipliedBy(this int value, decimal multiplier)
        {
            return MultipliedBy((decimal)value, multiplier);
        }

        public static decimal Minus(this decimal value, decimal minusValue)
        {
            if (value.IsNaN() || minusValue.IsNaN())
            {
                return Decimals.NaN;
            }

            return value - minusValue;
        }
        
        public static decimal Plus(this decimal value, decimal plusValue)
        {
            if (value.IsNaN() || plusValue.IsNaN())
            {
                return Decimals.NaN;
            }

            return value + plusValue;
        }

        public static bool IsZero(this decimal value)
        {
            return value == 0;
        }
        
        public static decimal Abs(this decimal value)
        {
            if (value.IsNaN())
            {
                return Decimals.NaN;
            }

            return Math.Abs(value);
        }

        public static bool IsLessThan(this decimal one, decimal other)
        {
            if (one.IsNaN() || other.IsNaN())
            {
                return false;
            } 

            return one.CompareTo(other) < 0;
        }

        public static bool IsLessThanOrEqual(this decimal one, decimal other)
        {
            if (one.IsNaN() || other.IsNaN())
            {
                return false;
            }
            return one.CompareTo(other) < 1;
        }

        public static bool IsGreaterThan(this decimal one, decimal other)
        {
            if (one.IsNaN() || other.IsNaN())
            {
                return false;
            }

            return one.CompareTo(other) > 0;
        }

        public static bool IsGreaterThanOrEqual(this decimal one, decimal other)
        {
            if (one.IsNaN() || other.IsNaN())
            {
                return false;
            }

            return one.CompareTo(other) > -1;
        }
        
        public static decimal Max(this decimal value, decimal other)
        {
            if (value.IsNaN() || other.IsNaN())
            {
                return value;
            }

            return Math.Max(value, other);
        }

        public static decimal Min(this decimal value, decimal other)
        {
            if (value.IsNaN() || other.IsNaN())
            {
                return value;
            }
            return Math.Min(value, other);
        }

        public static bool IsPositive(this decimal value)
        {
            if (value.IsNaN())
            {
                return false;
            }

            return value > 0;
        }

        public static bool IsPositiveOrZero(this decimal value)
        {
            if (value.IsNaN())
            {
                return false;
            }

            return value >= 0;
        }

        public static bool IsNegative(this decimal value)
        {
            if (value.IsNaN())
            {
                return false;
            }

            return value < 0;
        }

        public static bool IsNegativeOrZero(this decimal value)
        {
            if (value.IsNaN())
            {
                return false;
            }

            return value <= 0;
        }

        public static decimal Log(this decimal value)
        {
            if (value.IsNaN())
            {
                return Decimals.NaN;
            }

            return DecimalEx.Log(value);
        }

        public static decimal Pow(this decimal value, decimal other)
        {
            if (value.IsNaN() || other.IsNaN())
            {
                return Decimals.NaN;
            }

            return DecimalEx.Pow(value, other);
        }

        public static decimal Sqrt(this decimal value)
        {
            if (value.IsNaN())
            {
                return Decimals.NaN;
            }

            return DecimalEx.Sqrt(value);
        }

        public static bool IsNaN(this decimal value)
        {
            return value == Decimals.NaN;
        }

        public static decimal Floor(this decimal value)
        {
            if (value.IsNaN())
            {
                return Decimals.NaN;
            }

            return Math.Floor(value);
        }
    }
}
