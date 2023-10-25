using Newtonsoft.Json;
using UnityEngine;

namespace CommonUtility.TypedPlayerPrefs
{
    public static class PlayerPrefsExtra
    {
        private const string _addName = "Extra69";

        public static bool HasKey<T>(string key) =>
            PlayerPrefs.HasKey(GetTrueKey<T>(key));

        public static T GetObject<T>(string key) =>
            GetObject<T>(key, default);

        public static T GetObject<T>(string key, T @default)
        {
            string json = PlayerPrefs.GetString(GetTrueKey<T>(key));

            if (string.IsNullOrEmpty(json))
                return @default;

            try
            {
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
            catch
            {
                return @default;
            }
        }

        public static void SetObject<T>(string key, T @object)
        {
            string json = JsonConvert.SerializeObject(@object);

            PlayerPrefs.SetString(GetTrueKey<T>(key), json);
        }

        private static string GetTrueKey<T>(string key)
        {
            string type = typeof(T).Name;

            return string.Format("{0}_{1}_{2}", key, type, _addName);
        }
    }
}