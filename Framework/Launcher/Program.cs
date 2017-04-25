using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginManager;

namespace Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            PlugManager plugManager = new PlugManager();
            plugManager.PluginsDirectory = AppDomain.CurrentDomain.BaseDirectory;
            plugManager.LoadPlugins();
        }

        void LoadTestBench()
        {

        }
    }
}
