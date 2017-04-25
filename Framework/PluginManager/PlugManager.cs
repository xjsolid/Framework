using Framework.PluginInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PluginManager
{
    public class PlugManager
    {
        #region ctor
        #endregion

        #region members
        Dictionary<string, PluginInfo> plugins = new Dictionary<string, PluginInfo>();
        #endregion

        #region props
        public string PluginsDirectory { get; set; }

        #endregion

        #region methdos
        public void LoadPlugins()
        {
            if (string.IsNullOrEmpty(PluginsDirectory))
            {
                throw new Exception("PluginsDirectory is null/empty.");
            }
            plugins.Clear();
            foreach (string filePath in Directory.GetFiles(PluginsDirectory))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Extension.Equals(".dll"))
                {
                    AddPlugin(filePath);
                }
            }
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
                            PluginInfo pi = new PluginInfo();
                            pi.AssemblyPath = filePath;
                            pi.Instance = (IPlugin)Activator.CreateInstance(assembly.GetType(type.ToString()));
                            pi.Instance.Initialize();
                            plugins.Add(pi.Instance.Name, pi);
                        }
                    }
                    
                }
            }

        }
        #endregion
    }
}
