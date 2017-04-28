using Framework.PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFoundation.Wpf;
using System.IO;

namespace Logger
{
    public class LoggerPlugin : IPlugin
    {
        #region ctor
        public LoggerPlugin()
        {

        }
        #endregion

        #region members
        Messenger messenger;
        Dictionary<string, Logfile> LogFiles = new Dictionary<string, Logfile>();
        #endregion

        #region props
        public string Name
        {
            get
            {
                return "Logger";
            }
        }
        public string Description
        {
            get
            {
                return "";
            }
        }
        public Messenger Messenger
        {
            get
            {
                return messenger;
            }

            set
            {
                messenger = value;
            }
        }
        #endregion

        public void Initialize()
        {
            messenger.Register<string>(Messages.LogCreate, OnLogCreate);
            messenger.Register<LogContext>(Messages.Log, OnLog);
            messenger.Register<LogContext>(Messages.LogDispose, OnLogDispose);
        }

        void OnLogCreate(string logPath)
        {
            FindLogFile(logPath);
        }

        Logfile FindLogFile(string logPath)
        {
            foreach (string logFilePath in LogFiles.Keys)
            {
                if (logFilePath.Equals(logPath))
                {
                    return LogFiles[logFilePath];
                }
            }

            // create new LogFile
            string logDir = Path.GetDirectoryName(logPath);
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
            Logfile logFile = new Logfile(logPath, true);
            LogFiles.Add(logPath, logFile);
            return logFile;
        }

        void OnLog(LogContext logContext)
        {
            Logfile logFile = FindLogFile(logContext.LogPath);
            logFile.IsLog = logContext.SaveToFile;
            logFile.LogAsync(logContext.LogContent, logContext.LogLevel.ToString());
        }

        void OnLogDispose(LogContext logContext)
        {
            LogFiles.Remove(logContext.LogPath);
        }

        public void Dispose()
        {
        }


    }
}
