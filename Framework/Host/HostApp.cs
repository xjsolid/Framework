using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFoundation.Wpf;
using System.Configuration;
using System.Reflection;
using Framework.PluginInterface;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Host
{
    public class HostApp
    {
        #region ctor
        public HostApp()
        {
        }
        #endregion

        #region members
        static Messenger messenger = new Messenger();
        static PlugManager plugMgr = new PlugManager(messenger);
        #endregion

        #region props
        public static PlugManager PlugMgr
        {
            get { return plugMgr; }
        }

        #endregion

        #region methods
        public static void StartUp()
        {
            if (plugMgr.Plugins.Count == 0)
            {
                throw new Exception("No Plugins loaded.");
            }

            var appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            string startupPlugin = appConfig.AppSettings.Settings["StartupPlugin"].Value;//ConfigurationManager.AppSettings["StartupPlugin"];
            plugMgr.Show(startupPlugin);
            messenger.Register(Messages.MainUIClose, OnMainUIClose);

            //WindowHide(Console.Title);

            Application.Run();

        }

        static void OnMainUIClose()
        {
            //TODO: clean up when exit
            plugMgr.UnloadPlugins();

            OnExit();
        }
        #endregion

        #region events
        public static event EventHandler Exited;
        protected static void OnExit()
        {
            if (Exited != null)
            {
                Exited(null, new EventArgs());
            }
        }
        #endregion

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
