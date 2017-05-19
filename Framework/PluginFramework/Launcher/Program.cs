using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Host;

namespace Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常   
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Console.WriteLine("Starting...");


            string pluginFolder = ConfigurationManager.AppSettings["PluginFolder"];
            pluginFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pluginFolder);
            if (!Directory.Exists(pluginFolder))
            {
                Directory.CreateDirectory(pluginFolder);
            }

            HostApp.PlugMgr.LoadPlugins(AppDomain.CurrentDomain.BaseDirectory);
            HostApp.PlugMgr.LoadPlugins(pluginFolder);
            foreach (var item in Directory.EnumerateDirectories(pluginFolder))
            {
                HostApp.PlugMgr.LoadPlugins(item);
            }

            foreach (string key in HostApp.PlugMgr.Plugins.Keys)
            {
                Console.WriteLine("Loaded Plugin: {0}", key);
            }

            // register exit event
            HostApp.Exited += HostApp_Exited;

            //Host startup
            try
            {
                HostApp.StartUp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private static void HostApp_Exited(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        #region deal with exception
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show(ex.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            WriteLog(str);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception as Exception, e.ToString());
            MessageBox.Show(e.Exception.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            WriteLog(str);
        }

        static void WriteLog(string str)
        {
            if (!Directory.Exists("Log-Exception"))
            {
                Directory.CreateDirectory("Log-Exception");
            }

            using (StreamWriter sw = new StreamWriter(@"Log-Exception\Exception.txt", true))
            {
                sw.WriteLine(str);
                sw.WriteLine("---------------------------------------------------------");
                sw.Close();
            }
        }
        #endregion

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {                
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }

    }
}
