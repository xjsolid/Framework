using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Logger
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class Logfile
    {
        private string ToFileName;//日志存储路径
        private bool isLog = true;
        bool isSaveLog = true;

        public Logfile(string path, bool isLog)
        {
            this.ToFileName = path;
            this.isLog = isLog;
        }

        public bool IsLog
        {
            get { return this.isLog; }
            set { this.isLog = value; }
        }

        public void LogAsync(string message, string title)
        {
            LogParameter lp = new LogParameter(message, title);
            Thread thread = new Thread(new ParameterizedThreadStart(LogAsyncProgress));
            thread.Start(lp);
        }

        static object locker = new object();
        void LogAsyncProgress(object o)
        {
            lock(locker)
            {
                LogParameter lp = o as LogParameter;
                Log(lp.Message, lp.Title);
            }
        }


        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="title">日志标题</param>
        public void Log(string message, string title)
        {
            OnLog(new LogEventArgs(message + "\n"));
            if (!isLog)
            {
                return;
            }
            string path = this.ToFileName;
            string filename = path + "-Log.txt";
            string cont = "";
            FileInfo fileInf = new FileInfo(filename);
            if (File.Exists(filename))//如何文件存在 则在文件后面累加
            {
                FileStream myFss = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamReader r = new StreamReader(myFss);
                cont = r.ReadToEnd();
                r.Close();
                myFss.Close();
            }
            #region 生成文件日志
            FileStream myFs = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter n = new StreamWriter(myFs);
            //n.WriteLine(cont);
            //n.WriteLine("*****" + title + "*****");
            //n.WriteLine("时间：" + DateTime.Now.ToString());
            //n.WriteLine("信息：" + message);
            String logInfo = String.Format("[{0}][{1}][{2}] ", DateTime.Now.ToString(), title, message);
            n.WriteLine(logInfo);
            n.Close();
            myFs.Close();
            //
            if (fileInf.Length >= 1024 * 1024 * 200)
            {
                string NewName = path + "Log" + time() + ".txt";
                File.Move(filename, NewName);
            }
            
            #endregion
        }
        /// <summary>
        /// 系统时间
        /// </summary>
        /// <returns></returns>
        public string time()
        {
            string dNow = DateTime.Now.ToString().Trim().Replace("/", "").Replace(":", "");
            string fileName = dNow.ToString();
            return fileName;
        }

        public delegate void LogEventHandler(object sender, LogEventArgs e);
        public event LogEventHandler Logged;
        private void OnLog(LogEventArgs e)
        {
            LogEventHandler handler = Logged;
            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, e);
            }
        }
    }

    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(string logmessage)
        {
            this.LogContent = logmessage;
        }

        public string LogContent
        {
            get;
            set;
        }
    }

    class LogParameter
    {
        public LogParameter(string message, string title)
        {
            Message = message;
            Title = title;
        }

        public string Message { get; set; }
        public string Title { get; set; }
    }
}
