using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

namespace CommonUtility.ProjectConfiguration
{
    public abstract class AbstractConfig<T> : ScriptableObject where T : AbstractConfig<T>
    {
        private const string _configPath = "ScriptableObjects/Resources";
        private const string _extension = ".asset";

        private static string ConfigName = typeof(T).Name;

        protected static void SelectAsset()
        {
            #if UNITY_EDITOR
            Selection.activeObject = Load();
            EditorGUIUtility.PingObject(Selection.activeObject);
            #endif
        }

        private static T Load()
        {
            #if UNITY_EDITOR

            T result = Resources.Load<T>(ConfigName);
            if (result != null)
                return result;

            result = CreateInstance<T>();
            string systemPath = Path.Combine(Application.dataPath, _configPath);
            if (Directory.Exists(systemPath) == false)
            {
                Directory.CreateDirectory(systemPath);
                AssetDatabase.Refresh();
            }

            string fullPath = Path.Combine(Path.Combine("Assets", _configPath), ConfigName + _extension);
            AssetDatabase.CreateAsset(result, fullPath);
            return result;

            #else
            return default(T);
            #endif
        }

        protected virtual void OnValidate()
        {
            #if UNITY_EDITOR
            if (EditorApplication.isUpdating)
                return;

            if (EditorApplication.isCompiling)
                return;

            if (AssetDatabase.IsAssetImportWorkerProcess())
                return;

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("ProjectConfig saved");
            #endif
        }
    }
}