using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.PluginInterface
{
    enum MessageEnum
    {
        #region MainUI messages
        MainAppExit,
        #endregion

    }

    public class Messages
    {
        #region mainUI messages
        public static string MainAppExit
        {
            get { return MessageEnum.MainAppExit.ToString(); }
        }
        #endregion

    }

    public class LogContext
    {
        public LogContext(string logContent, string logPath, LogLevel logLevel, bool saveToFile)
        {
            this.LogContent = logContent;
            this.LogPath = logPath;
            this.LogLevel = logLevel;
            this.SaveToFile = saveToFile;
        }

        public LogContext(string logContent, string logPath)
        {
            this.LogContent = logContent;
            this.LogPath = logPath;
            this.LogLevel = LogLevel.Info;
            this.SaveToFile = true;
        }

        public string LogContent
        {
            get;set;
        }

        public string LogPath
        {
            get;set;
        }

        public LogLevel LogLevel
        {
            get;set;
        }

        public bool SaveToFile
        {
            get;set;
        }
    }

    public enum LogLevel
    {
        Info,
        Warnning,
        Error,
        Debug,
    }
}
