using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonUtility.Extensions
{
    public static class EnumExtensions
    {
        private static readonly Dictionary<Type, object> _cashed = new Dictionary<Type, object>();

        public static IReadOnlyList<TEnum> All<TEnum>(this Type type) where TEnum : Enum
        {
            if (type != typeof(TEnum))
                throw new ArgumentException($"type != typeof(TEnum), type: {type}, TEnum: {typeof(TEnum)}");

            if (_cashed.TryGetValue(type, out var cachedValuesObj) && cachedValuesObj is List<TEnum> cachedValues)
                return cachedValues;

            List<TEnum> values = Enum.GetValues(type).Cast<TEnum>().ToList();
            _cashed[type] = values;

            return values;
        }
    }
}