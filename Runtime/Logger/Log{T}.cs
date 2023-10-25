using System;
using UnityEngine;

using Object = UnityEngine.Object;

namespace CommonUtility.Logger
{
    public static class Log<T>
    {
        private const int _maxSymbols = 2000;

        private static readonly Type _type = typeof(T);

        public static void Info(string message, Object context = null) =>
            Message(LogType.Log, message, context);

        public static void Warn(string message, Object context = null) =>
            Message(LogType.Warning, message, context);

        public static void Error(string message, Object context = null) =>
            Message(LogType.Error, message, context);

        public static void Error(Exception e) =>
            Message(LogType.Error, e.Message, null, e.StackTrace);

        private static void Message(LogType logType, string message, Object context, string stackTrace = null)
        {
            string fullMessage = Format(message, stackTrace);
            LogListenersProvider.Track(logType, fullMessage);

            if (message.Length > _maxSymbols)
                fullMessage = Format($"{message.Remove(80)} ... Length: {message.Length}", stackTrace);

            switch (logType)
            {
                case LogType.Warning:
                    Debug.LogWarning(fullMessage, context);
                    break;
                case LogType.Log:
                    Debug.Log(fullMessage, context);
                    break;
                default:
                    Debug.LogError(fullMessage, context);
                    break;
            }
        }

        private static string Format(string message, string stackTrace = null)
        {
            if (string.IsNullOrEmpty(stackTrace))
                stackTrace = Environment.StackTrace;

            return $"[{DateTime.UtcNow.ToString("hh:mm:ss")}][{_type}] {message} \n {stackTrace}";
        }
    }
}
