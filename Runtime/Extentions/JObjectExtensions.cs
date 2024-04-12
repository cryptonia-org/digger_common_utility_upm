using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace CommonUtility.Extensions
{
    public static class JObjectExtensions
    {
        private const BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField | BindingFlags.GetProperty;

        private static readonly Dictionary<Type, string[]> _cashedFields = new Dictionary<Type, string[]>();

        public static bool Is<T>(this JObject jObject)
        {
            string[] fields = GetFields<T>();

            if (fields.Length != jObject.Count)
                return false;

            return jObject.CanCast(fields);
        }

        public static bool CanCast<T>(this JObject jObject) => jObject.CanCast(GetFields<T>());

        public static bool CanCast(this JObject jObject, Type type) => jObject.CanCast(GetFields(type));

        private static bool CanCast(this JObject jObject, string[] fields)
        {
            if (jObject == null || jObject.Count == 0)
                return false;

            for (int i = 0; i < fields.Length; i++)
            {
                if (jObject.ContainsKey(fields[i]) == false)
                    return false;
            }

            return true;
        }

        private static string[] GetFields<T>() => GetFields(typeof(T));

        private static string[] GetFields(Type type)
        {
            if (_cashedFields.TryGetValue(type, out string[] fields) == false)
            {
                fields = 
                    type.GetFields(_bindingFlags).Select(Convert)
                    .Concat(type.GetProperties(_bindingFlags).Select(Convert))
                    .ToArray();

                _cashedFields.Add(type, fields);
            }

            return fields;
        }

        
        private static string Convert(MemberInfo field)
        {
            foreach (var attribute in field.CustomAttributes)
            {
                // Looking for JsonPropertyAttribute(string propertyName)

                if (attribute.AttributeType != typeof(Newtonsoft.Json.JsonPropertyAttribute))
                    continue;

                if (attribute.ConstructorArguments.Count == 0)
                    continue;

                foreach (var arg in attribute.ConstructorArguments)
                {
                    if (arg.ArgumentType != typeof(string))
                        continue;

                    return (string)arg.Value;
                }
            }
            return field.Name;
        }
    }
}