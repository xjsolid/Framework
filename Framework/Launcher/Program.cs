using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginManager;
using System.Configuration;
using System.Windows.Forms;
using MvvmFoundation.Wpf;
using Framework.PluginInterface;
using System.IO;
using System.Runtime.InteropServices;

namespace Launcher
{
    class Program
    {
        static Messenger messenger = new Messenger();
        static PlugManager plugManager = new PlugManager(messenger);
        static void Main(string[] args)
        {
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常   
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Console.WriteLine("Starting...");
            plugManager.LoadPlugins(AppDomain.CurrentDomain.BaseDirectory);

            string pluginFolder = ConfigurationManager.AppSettings["PluginFolder"];
            pluginFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pluginFolder);
            if (!Directory.Exists(pluginFolder))
            {
                Directory.CreateDirectory(pluginFolder);
            }
            plugManager.LoadPlugins(pluginFolder);

            foreach (string key in plugManager.Plugins.Keys)
            {
                Console.WriteLine("Loaded Plugin: {0}", key);
            }

            // show MainUI
            string startupPlugin = ConfigurationManager.AppSettings["StartupPlugin"];
            try
            {
                plugManager.Show(startupPlugin);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception throw:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadKey();
            }
            messenger.Register(Messages.MainUIClose, OnMainUiClose);

            //WindowHide(Console.Title);

            Application.EnableVisualStyles();
            Application.Run();
            //             plugManager.UnloadPlugins();
        }

        static void OnMainUiClose()
        {
            WindowShow(Console.Title);
            Console.WriteLine("Unloading plugins");
            try
            {
                plugManager.UnloadPlugins();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception throw:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadKey();
            }
            Console.WriteLine("Application exit");
            Environment.Exit(0);
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            WriteLog(str);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception as Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region 隐藏窗口  
        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        public static void WindowHide(string consoleTitle)
        {
            IntPtr a = FindWindow("ConsoleWindowClass", consoleTitle);
            if (a != IntPtr.Zero)
                ShowWindow(a, 0);//隐藏窗口  
            else
                throw new Exception("can't hide console window");
        }

        public static void WindowShow(string consoleTitle)
        {
            IntPtr a = FindWindow("ConsoleWindowClass", consoleTitle);
            if (a != IntPtr.Zero)
                ShowWindow(a, 1);//隐藏窗口  
            else
                throw new Exception("can't hide console window");
        }
        #endregion
    }
}
