using System.Collections.Generic;
using UnityEngine;

namespace CommonUtility.Extensions
{
    public static class Vector3Extensions
    {
        public static float InverseLerp(this Vector3 value, Vector3 a, Vector3 b)
        {
            Vector3 AB = b - a;
            Vector3 AV = value - a;
            return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
        }

        public static Vector3 GetBeziePoint(this IList<Vector3> curve, float time)
        {
            IList<Vector3> buffer = new List<Vector3>();
            return curve.GetBeziePoint(time, buffer);
        }

        public static Vector3 GetBeziePoint(this IList<Vector3> curve, float time, IList<Vector3> buffer)
        {
            int iterationsCount = curve.Count - 1;

            buffer.Clear();
            for (int i = 0; i < buffer.Count; i++)
                buffer.Add(curve[i]);

            for (int i = 0; i < iterationsCount; i++)
            {
                int pointsCount = iterationsCount - i;
                for (int p = 0; p < pointsCount; p++)
                    buffer[p] = Vector3.LerpUnclamped(buffer[p], buffer[p + 1], time);
            }

            return buffer[0];
        }
    }
}