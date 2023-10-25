using UnityEngine;

namespace CommonUtility.Extensions
{
    public static class ObjectExtensions
    {
        public static void TryDestroy(this Object obj)
        {
            if (obj != null)
                Object.Destroy(obj);
        }

        public static void TryDestroyGameObject(this Component monoBehaviour)
        {
            if (monoBehaviour != null && monoBehaviour.gameObject != null)
                Object.Destroy(monoBehaviour.gameObject);
        }
    }
}