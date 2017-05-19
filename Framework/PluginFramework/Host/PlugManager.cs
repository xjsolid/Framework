using Framework.PluginInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Host
{
    public class PlugManager
    {
        #region ctor
        public PlugManager()
        {
        }
        #endregion

        #region members
        Dictionary<string, PluginInfo> plugins = new Dictionary<string, PluginInfo>();
        #endregion

        #region props
        public Dictionary<string, PluginInfo> Plugins
        {
            get { return plugins; }
        }

        public PluginInfo this[string pluginName]
        {
            get { return FindPlugin(pluginName); }
        }
        #endregion

        #region methdos
        public void LoadPlugins(string plugDirectory)
        {
            if (string.IsNullOrEmpty(plugDirectory))
            {
                throw new ArgumentNullException("plugDirectory");
            }
            Console.WriteLine("Loading plugins in folder: {0}", plugDirectory);
            foreach (string filePath in Directory.GetFiles(plugDirectory))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Extension.Equals(".dll"))
                {
                    AddPlugin(filePath);
                }
            }
        }

        public void UnloadPlugins()
        {
            foreach (var item in plugins)
            {
                PluginInfo pi = item.Value;
                pi.Instance.Dispose();
                pi.Instance = null;
            }
            plugins.Clear();
        }

        void AddPlugin(string filePath)
        {
            Assembly assembly = Assembly.LoadFrom(filePath);
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsPublic)
                {
                    if (!type.IsAbstract)
                    {
                        Type typeInterface = type.GetInterface("Framework.PluginInterface.IPlugin", true);
                        if (typeInterface != null)
                        {
                            Console.WriteLine("Found Plugin: {0}", type.Name);
                            PluginInfo pi = FindPlugin(type.Name);
                            if (pi != null)
                            {
                                Console.WriteLine(string.Format("Plugin: {0} instance already exists", type.Name));
                                continue;
                            }
                            pi = new PluginInfo(type.Name);
                            pi.AssemblyPath = filePath;
                            Console.WriteLine("Create Instance of Plugin: {0}", type.Name);
                            object instance = Activator.CreateInstance(assembly.GetType(type.ToString()));
                            //object instance = assembly.CreateInstance(type.FullName);
                            pi.Instance = (IPlugin)instance;
                            Console.WriteLine("Success.");
                            Console.WriteLine("Initializing Plugin: {0}", type.Name);
                            pi.Instance.Initialize();
                            Console.WriteLine("Success.");
                            plugins.Add(type.Name, pi);
                        }
                    }
                }
            }

        }

        PluginInfo FindPlugin(string plugin)
        {
            foreach (string item in plugins.Keys)
            {
                if (item.Equals(plugin))
                {
                    return plugins[plugin];
                }
            }
            return null;
        }


        #endregion
    }
}
