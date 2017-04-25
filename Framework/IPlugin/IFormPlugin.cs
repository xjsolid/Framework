using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Framework.PluginInterface
{
    public interface IFormPlugin : IPlugin
    {
        Form Form { get; }
    }
}
