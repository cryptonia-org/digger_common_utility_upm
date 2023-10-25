using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace CommonUtility.Extensions
{
    public static class StringExtensions
    {
        public static string AddUrlParameter(this string url, string name, object data)
        {
            StringBuilder stringBuilder = new StringBuilder(url);
            stringBuilder.FormatToParametrizedUrl();
            stringBuilder.AddUrlParameter(name, data);
            return stringBuilder.ToString();
        }

        public static string AddUrlParameters(this string url, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            StringBuilder stringBuilder = new StringBuilder(url);
            stringBuilder.FormatToParametrizedUrl();
            foreach (KeyValuePair<string, object> item in parameters)
                stringBuilder.AddUrlParameter(item.Key, item.Value);

            return stringBuilder.ToString();
        }

        private static void AddUrlParameter(this StringBuilder url, string name, object data)
        {
            if (url[url.Length - 1] != '?')
                url.Append('&');

            url.Append(name);
            url.Append('=');
            url.Append(UnityWebRequest.EscapeURL(JsonConvert.SerializeObject(data)));
        }

        private static void FormatToParametrizedUrl(this StringBuilder url)
        {
            for (int i = 0; i < url.Length; i++)
            {
                if (url[i] == '?')
                    return;
            }

            url.Append('?');
        }

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: return string.Empty;
                case "": return string.Empty;
                default: return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }

        /// <summary>
        /// convert n-decimal system value string to decimal int
        /// </summary>
        public static int ToDecimal(this string str, int n, int from, int length)
        {
            int result = 0;
            int to = from + length;
            for (int j = from; j < to; j++)
            {
                char d = str[j];

                int i = d < '0' || d > '9' ? char.ToUpper(d) - 'A' + 10 : d - '0';
                result = result * n + i;
            }

            return result;
        }

        public static bool IsEmail(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            return text.Contains(".") && text.ToCharArray().Count(c => c == '@') == 1;
        }
    }
}