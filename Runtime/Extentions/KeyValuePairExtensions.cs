using System.Collections.Generic;

namespace CommonUtility.Extensions
{
    public static class KeyValuePairExtensions
    {
        public static KeyValuePair<T1, T2> Pair<T1, T2>(this T1 key, T2 value)
        {
            return new KeyValuePair<T1, T2>(key, value);
        }

        public static KeyValuePair<T1, object> PairAsObj<T1, T2>(this T1 key, T2 value)
        {
            return new KeyValuePair<T1, object>(key, value);
        }
    }
}