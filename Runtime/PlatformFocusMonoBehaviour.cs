using UnityEngine;

namespace CommonUtility
{
    public abstract class PlatformFocusMonoBehaviour : MonoBehaviour
    {
        [SerializeField]
        private bool _focusLog;

        protected abstract void OnFocusChanged(bool focus);

#if UNITY_IOS
    private void OnApplicationPause(bool pause)
    {
        if (_focusLog)
            Debug.Log($"OnApplicationPause {pause}");

        bool focus = pause == false;
        OnFocusChanged(focus);
    }

#else // UNITY_ANDROID
        private void OnApplicationFocus(bool focus)
        {
            if (_focusLog)
                Debug.Log($"OnApplicationFocus {focus}");

            OnFocusChanged(focus);
        }
#endif

    }
}
