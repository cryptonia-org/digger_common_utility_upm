using System.Collections.Generic;
using UnityEngine;

namespace CommonUtility.Logger
{
    public static class LogListenersProvider
    {
        private static readonly List<ILogsListener> _listeners = new List<ILogsListener>();

        public static void AddListener(ILogsListener listener)
        {
            if (_listeners.Contains(listener) == false)
                _listeners.Add(listener);
        }

        public static void RemoveListener(ILogsListener listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }

        public static void Track(LogType logType, string log)
        {
            if (_listeners.Count > 0)
            {
                foreach (ILogsListener listener in _listeners)
                    listener.Log(logType, log);
            }
        }
    }
}
