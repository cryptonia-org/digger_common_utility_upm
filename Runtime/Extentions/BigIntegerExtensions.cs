using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using UnityEngine;

namespace CommonUtility.Extensions
{
    public static class BigIntegerExtensions
    {
        private const int _step = 1000;

        private static readonly int charA = Convert.ToInt32('A');

        private static readonly Dictionary<int, string> _units = new Dictionary<int, string>
    {
        {0, ""},
        {1, "K"},
        {2, "M"},
        {3, "B"},
        {4, "T"}
    };

        public static string Format(this BigInteger value)
        {
            if (value < 1)
            {
                return "0";
            }

            int n = (int)BigInteger.Log(value, 1000);
            string unit;

            if (n < _units.Count)
            {
                unit = _units[n];
            }
            else
            {
                var unitInt = n - _units.Count;
                var secondUnit = unitInt % 26;
                var firstUnit = unitInt / 26;
                unit = Convert.ToChar(firstUnit + charA).ToString() + Convert.ToChar(secondUnit + charA).ToString();
            }

            if (n >= 1)
            {
                BigInteger m = value / BigInteger.Pow(1000, n - 1);
                return ((double)m / 1000).ToString("F") + unit;
            }
            else
            {
                BigInteger m = value / BigInteger.Pow(1000, n);
                return (Math.Floor((double)m * 100) / 100).ToString("0.##") + unit;
            }


        }

        public static string FormatTime(this BigInteger value, bool inMilliseconds = true)
        {
            if (inMilliseconds)
                value = value.ToSec();

            if (value < int.MaxValue)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds((long)value);
                string text = "";
                if (timeSpan.Days > 0)
                    text = $"{Math.Round(timeSpan.TotalDays)}d";
                else if (timeSpan.Hours > 0)
                    text = $"{Math.Round(timeSpan.TotalHours)}h";
                else if (timeSpan.Minutes > 0)
                    text = $"{timeSpan.Minutes}m";
                else
                    text = $"{timeSpan.Seconds}s";

                return text;
            }
            else
            {
                return "Infinity";
            }
        }

        public static string FormatTimeLong(this BigInteger value, bool inMilliseconds = true, bool days = false, bool hours = false, bool minutes = false, bool seconds = false)
        {
            if (inMilliseconds)
                value = value.ToSec();

            if (value < int.MaxValue)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds((long)value);
                StringBuilder stringBuilder = new StringBuilder();
                if (days)
                {
                    stringBuilder.Append($"{timeSpan.Days}d");
                }
                if (hours)
                {
                    stringBuilder.Append($" {timeSpan.Hours}h");
                }
                if (minutes)
                {
                    stringBuilder.Append(" ");
                    if (timeSpan.Minutes < 10)
                        stringBuilder.Append("0");
                    stringBuilder.Append($"{timeSpan.Minutes}m");
                }
                if (seconds)
                {
                    stringBuilder.Append(" ");
                    if (timeSpan.Seconds < 10)
                        stringBuilder.Append("0");
                    stringBuilder.Append($"{timeSpan.Seconds}s");
                }

                return stringBuilder.ToString();
            }
            else
            {
                return "Infinity";
            }
        }

        public static float Normalize(this BigInteger value, BigInteger length)
        {
            if (value > length)
                return 1;

            if (length > 1000)
            {
                BigInteger diwider = BigInteger.Pow(10, length.Length() - 3);
                length /= diwider;
                value /= diwider;
            }

            return (float)value / (float)length;
        }

        public static int Length(this BigInteger value)
        {
            if (value <= 0)
                return 1;

            return (int)Math.Floor(BigInteger.Log10(value)) + 1;
        }

        public static BigInteger Abs(this BigInteger value)
        {
            return value * value.Sign;
        }

        public static BigInteger Lerp(BigInteger a, BigInteger b, float t)
        {
            int tInt = Mathf.RoundToInt(t * _step);
            tInt = Mathf.Clamp(tInt, 0, _step);

            BigInteger delta = (b - a) * tInt / _step;

            return a + delta;
        }

        public static BigInteger Multiply(this BigInteger value1, float value2)
        {
            BigInteger stepV2 = Mathf.RoundToInt(value2 * _step);
            return value1 * stepV2 / _step;
        }
    }
}