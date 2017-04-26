using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginManager;
using System.Configuration;
using System.Windows.Forms;
using MvvmFoundation.Wpf;
using Framework.PluginInterface;

namespace Launcher
{
    class Program
    {
        static Messenger messenger = new Messenger();
        static PlugManager plugManager = new PlugManager(messenger);
        static void Main(string[] args)
        {
            messenger.Register(Messages.MainUIClose, OnMainUiClose);

            Console.WriteLine("Starting...");
            plugManager.LoadPlugins(AppDomain.CurrentDomain.BaseDirectory);
            foreach (string key in plugManager.Plugins.Keys)
            {
                Console.WriteLine("Loaded Plugin: {0}", key);
            }


            string startupPlugin = ConfigurationManager.AppSettings["StartupPlugin"];
            plugManager.Show(startupPlugin);

            Application.EnableVisualStyles();
            Application.Run();

            //             plugManager.UnloadPlugins();
        }

        static void OnMainUiClose()
        {
            plugManager.UnloadPlugins();
            Application.Exit();
        }

    }
}
