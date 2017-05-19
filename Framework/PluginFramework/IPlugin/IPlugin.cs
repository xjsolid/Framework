using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFoundation.Wpf;

namespace Framework.PluginInterface
{
    public interface IPlugin : IDisposable
    {
        string Name { get; }
        string Description { get; }
        Messenger Messenger { get; set; }

        void Initialize();
    }
}
