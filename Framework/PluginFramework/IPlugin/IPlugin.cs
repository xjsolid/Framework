using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.PluginInterface
{
    public interface IPlugin : IDisposable
    {
        string Name { get; }
        string Description { get; }

        void Initialize();
        void Start();
    }
}
