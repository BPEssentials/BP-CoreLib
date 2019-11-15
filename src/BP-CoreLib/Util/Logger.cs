using System;
using System.Globalization;
using BPCoreLib.Interfaces;

namespace BPCoreLib.Util
{
    public class Logger : ILogger
    {
        public event Action<string> OnLog;

        public void Log(string str)
        {
            OnLog?.Invoke(str);
            UnityEngine.Debug.Log(str);
        }

        public void LogWithTimestamp(string str)
        {
            Log(DateTime.UtcNow.ToString("[yyyy-MM-dd HH:mm:ss.fff] ", CultureInfo.InvariantCulture) + str);
        }

        public void LogInfo(string str)
        {
            LogWithTimestamp("[INFO] " + str);
        }

        public void LogError(string str)
        {
            LogWithTimestamp("[ERROR] " + str);
        }

        public void LogWarning(string str)
        {
            LogWithTimestamp("[WARNING] " + str);
        }

        public void LogException(Exception ex)
        {
            LogError("[EXCEPTION] " + ex);
        }
    }
}
