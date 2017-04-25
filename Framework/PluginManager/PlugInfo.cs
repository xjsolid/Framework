using Framework.PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginManager
{
    public class PluginInfo
    {
        public IPlugin Instance { get; set; }
        public string AssemblyPath { get; set; }
    }
}
