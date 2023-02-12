using BPCoreLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BPCoreLib.Util
{
    public class TimeLogFormatter : LogFormatter
    {
        public DateTime Time => UseUtc ? DateTime.UtcNow : DateTime.Now;

        public bool UseUtc { get; set; } = false;
        
        public string TimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fff";

        public override string Format(LogLevel level, object message, Exception exception = null)
        {
            return $"{Time.ToString(TimeFormat, CultureInfo.InvariantCulture)}@{level}: {message}";
        }
    }
    
    public class UnityDebugLogProvider : LogProvider
    {
        public override void Log(LogLevel level, object message, Exception exception = null)
        {
            switch (level)
            {
                case LogLevel.Information:
                    UnityEngine.Debug.Log(message);
                    break;
                case LogLevel.Warning:
                    UnityEngine.Debug.LogWarning(message);
                    break;
                case LogLevel.Error:
                    UnityEngine.Debug.LogError(message);
                    break;
                case LogLevel.Exception:
                    UnityEngine.Debug.LogException(exception);
                    break;
                case LogLevel.Assertion:
                    UnityEngine.Debug.LogAssertion(message);
                    break;
                case LogLevel.Unknown:
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }
    
    public abstract class LogFormatter
    {
        public abstract string Format(LogLevel level, object message, Exception exception = null);
    }

    public abstract class LogProvider
    {
        public LogFormatter Formatter { get; set; }
        
        public virtual void PreLog(LogLevel level, object message, Exception exception = null)
        {
            object messageString = message;
            if (Formatter != null)
            {
                messageString = Formatter.Format(level, messageString, exception);
            }
            Log(level, messageString, exception);
        }
        
        public abstract void Log(LogLevel level, object message, Exception exception = null);
    }

    public enum LogLevel
    {
        Unknown = 0,
        Information = 1,
        Warning = 2,
        Error = 3,
        Exception = 4,
        Assertion = 5,
    }

    public class uLogger
    {
        public List<LogProvider> LogProviders { get; } = new List<LogProvider>();
        
        public void RegisterProvider<T>() where T : LogProvider, new()
        {
            LogProviders.Add(new T());
        }

        public void RegisterProvider<T>(T provider) where T : LogProvider
        {
            LogProviders.Add(provider);
        }
        
        public void Log(LogLevel level, object message, Exception exception = null)
        {
            foreach (LogProvider logProvider in LogProviders)
            {
                logProvider.Log(level, message, exception);
            }
        }
        
        public void LogInformation(object message)
        {
            Log(LogLevel.Information, message);
        }
        
        public void LogError(object message)
        {
            Log(LogLevel.Error, message);
        }
        
        public void LogWarning(object message)
        {
            Log(LogLevel.Warning, message);
        }
        
        public void LogException(object message, Exception exception)
        {
            Log(LogLevel.Exception, message, exception);
        }
        
        public void LogAssertion(object message)
        {
            Log(LogLevel.Assertion, message);
        }
    }

    public class uLoggerFactory
    {
        public List<LogProvider> DefaultLogProviders { get; } = new List<LogProvider>();
        
        public uLogger Create()
        {
            uLogger logger = new uLogger();
            logger.LogProviders.AddRange(DefaultLogProviders);
            return logger;
        }
    }

    // TODO: generic
    // TODO: multiple providers
    // TODO: unity debug bit meh
    // TODO: datetime default formatting (global logger settings)
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
