using UnityEngine;

namespace CommonUtility.Logger
{
    public interface ILogsListener
    {
        public void Log(LogType type, string log);
    }
}