using System;

namespace BPCoreLib.Interfaces
{
    public interface ILogger
    {
        event Action<string> OnLog;

        void Log(string str);

        void LogWithTimestamp(string str);

        void LogInfo(string str);

        void LogError(string str);

        void LogWarning(string str);

        void LogException(Exception ex);
    }
}
