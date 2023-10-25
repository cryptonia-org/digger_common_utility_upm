using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DisposableSubscriptions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CommonUtility.SceneManagement
{
    public static class SceneLoader
    {
        private static readonly Updatable<SceneType> _current = new Updatable<SceneType>((SceneType)0);
        private static AsyncOperation _loading;

        public static IUpdatable<SceneType> Current => _current;

        private static bool Loading => _loading != null && _loading.isDone == false;

        public static async Task LoadSceneAsync<T>(T args) where T : ISceneArgs
        {
            if (Loading)
                throw new InvalidOperationException("Scene already loading");

            AsyncOperation loading = SceneManager.LoadSceneAsync((int)args.Scene, LoadSceneMode.Single);
            _loading = loading;

            while (loading.isDone == false)
                await Task.Yield();

            FindHandler<T>()?.OnSceneLoaded(args);
            _current.Update(args.Scene);
        }

        private static ISceneLoadedHandler<T> FindHandler<T>() where T : ISceneArgs
        {
            List<ISceneLoadedHandler<T>> callbacks = new List<ISceneLoadedHandler<T>>();
            foreach (ISceneLoadedHandler callback in FindObjectsOfType<ISceneLoadedHandler>())
            {
                if (callback is ISceneLoadedHandler<T>)
                    callbacks.Add((ISceneLoadedHandler<T>)callback);
                else
                    Debug.LogWarning($"Find foreign type SceneLoadedHandler : {callback.GetType()} {((Component)callback)?.gameObject?.name}, on scene {SceneManager.GetActiveScene().name} needed type {typeof(T)}", ((Component)callback)?.gameObject);
            }

            if (callbacks.Count == 0)
            {
                Debug.LogWarning($"Cannot find SceneLoadedHandler typeof {typeof(T)} in {SceneManager.GetActiveScene().name} scene");
                return null;
            }

            if (callbacks.Count > 1)
                Debug.LogWarning($"There is multiple SceneLoadedHandler's typeof {typeof(T)} in {SceneManager.GetActiveScene().name} scene");

            return callbacks[0];
        }

        private static IEnumerable<T> FindObjectsOfType<T>()
        {
            List<T> result = new List<T>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                GameObject[] rootObjs = SceneManager.GetSceneAt(i).GetRootGameObjects();
                foreach (GameObject root in rootObjs)
                    result.AddRange(root.GetComponentsInChildren<T>(true));
            }

            return result;
        }
    }
}