using UnityEngine;
using Random = System.Random;

namespace CommonUtility.Extensions
{
    public static class SystemRandomExtensions
    {
        public static Vector3 InsideUnitSphere(this Random random)
        {
            Quaternion rotation = Quaternion.Euler(random.NextFloat(360f), random.NextFloat(360f), 0);
            return rotation * Vector3.forward;
        }

        /// <returns>Random float between zero and max</returns>
        public static float NextFloat(this Random random, float max) =>
            random.NextFloat(0f, max);

        public static float NextFloat(this Random random) =>
            random.NextFloat(float.MinValue, float.MaxValue);

        public static float NextFloat(this Random random, float min, float max)
        {
            double sample = random.NextDouble();
            double range = (double)max - (double)min;
            double scaled = sample * range + min;
            return (float)scaled;
        }

        public static Random New(object seed) => new Random(seed.GetHashCode());
    }
}