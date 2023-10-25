using UnityEngine;

namespace CommonUtility.Extensions
{
    public static class RectTransformExtensions
    {
        private static readonly Vector3[] _corners = new Vector3[4];

        /// <summary>
        /// Not thread-safe
        /// </summary>
        public static Vector3 GetCenter(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(_corners);

            Vector3 result = Vector3.zero;
            for (int i = 0; i < _corners.Length; i++)
                result += _corners[i];

            return result / _corners.Length;
        }
    }
}