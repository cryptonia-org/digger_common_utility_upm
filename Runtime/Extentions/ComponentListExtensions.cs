using System;
using System.Collections.Generic;
using UnityEngine;

namespace CommonUtility.Extensions
{
    public static class ComponentListExtensions
    {
        public static T Spawn<T>(this List<T> list, T prefab, bool enableObject = true) where T : Component
        {
            T spawnedObject = UnityEngine.Object.Instantiate(prefab, prefab.transform.parent);

            if (enableObject)
                spawnedObject.gameObject.SetActive(true);

            list.Add(spawnedObject);

            return spawnedObject;
        }

        public static void DestroyAll<T>(this List<T> list, Action<T> unitAction) where T : Component
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] != null)
                {
                    unitAction.Invoke(list[i]);
                    UnityEngine.Object.Destroy(list[i].gameObject);
                }

            list.Clear();
        }

        public static void DestroyAll<T>(this List<T> list) where T : Component
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] != null)
                    UnityEngine.Object.Destroy(list[i].gameObject);

            list.Clear();
        }
    }
}