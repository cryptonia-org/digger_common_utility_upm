using System.Text;
using UnityEngine;

namespace CommonUtility.Logger
{
    public static class LogExtensions
    {
        private const string _default = "Null object";
        private const char _separator = '.';

        public static string GetHierarchyName(this Component obj)
        {
            if (obj == null)
                return _default;

            return obj.gameObject.GetHierarchyName();
        }

        public static string GetHierarchyName(this GameObject obj)
        {
            if (obj == null)
                return _default;

            StringBuilder stringBuilder = new StringBuilder();
            Transform target = obj.transform;

            while (true)
            {
                stringBuilder.Append(target.name);
                target = target.parent;

                if (target == null)
                    break;

                stringBuilder.Append(_separator);
            }

            return stringBuilder.ToString();
        }
    }
}
