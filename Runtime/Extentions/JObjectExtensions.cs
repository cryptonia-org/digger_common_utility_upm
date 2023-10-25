using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace CommonUtility.Extensions
{
    public static class JObjectExtensions
    {
        private const BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.Public;

        private static readonly Dictionary<Type, FieldInfo[]> _cashedFields = new Dictionary<Type, FieldInfo[]>();

        public static bool Is<T>(this JObject jObject)
        {
            FieldInfo[] fields = GetFields<T>();

            if (fields.Length != jObject.Count)
                return false;

            return jObject.CanCast(fields);
        }

        public static bool CanCast<T>(this JObject jObject) => jObject.CanCast(GetFields<T>());

        public static bool CanCast(this JObject jObject, Type type) => jObject.CanCast(GetFields(type));

        private static bool CanCast(this JObject jObject, FieldInfo[] fields)
        {
            if (jObject == null || jObject.Count == 0)
                return false;

            for (int i = 0; i < fields.Length; i++)
            {
                if (jObject.ContainsKey(fields[i].Name) == false)
                    return false;
            }

            return true;
        }

        private static FieldInfo[] GetFields<T>() => GetFields(typeof(T));

        private static FieldInfo[] GetFields(Type type)
        {
            if (_cashedFields.TryGetValue(type, out FieldInfo[] fields) == false)
            {
                fields = type.GetFields(_bindingFlags);
                _cashedFields.Add(type, fields);
            }

            return fields;
        }
    }
}