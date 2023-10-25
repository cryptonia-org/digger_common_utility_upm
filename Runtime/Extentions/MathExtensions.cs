using System.Collections.Generic;
using System.Numerics;

using Random = UnityEngine.Random;

namespace CommonUtility.Extensions
{
    public static class MathExtensions
    {
        public static double Clamp(this double value, double min, double max) =>
         System.Math.Max(System.Math.Min(value, max), min);

        public static float Remap(this float value, float from1, float to1, float from2, float to2) => (value - from1) / (to1 - from1) * (to2 - from2) + from2;

        public static bool InRange(this int value, int min, int max) => value >= min && value <= max;

        public static bool InRange(this float value, float min, float max) => value >= min && value <= max;

        public static int Looped(this int value, int length)
        {
            if (value >= 0)
                return value % length;
            else
                return (length + value % length) % length;
        }

        public static float Looped(this float value, float length)
        {
            if (value >= 0)
                return value % length;
            else
                return (length + value % length) % length;
        }

        public static int LoopedStep(this int value, int length) => (value - value.Looped(length)) / length;

        public static int GetWeightedRandom(this IReadOnlyList<float> weights)
        {
            float sum = 0;
            for (int i = 0; i < weights.Count; i++)
                sum += weights[i];

            float rnd = Random.Range(0, sum);

            float previousSum = 0;

            for (int i = 0; i < weights.Count; i++)
            {
                previousSum += weights[i];

                if (rnd < previousSum)
                    return i;
            }

            return weights.Count - 1;
        }

        public static int ToMs(this int value) => value * 1000;

        public static BigInteger ToMs(this BigInteger value) => value * 1000;

        public static int ToSec(this int value) => value / 1000;

        public static BigInteger ToSec(this BigInteger value) => value / 1000;
    }
}