using Framework.PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmFoundation.Wpf;

namespace Logger
{
    public class LoggerPlugin : IPlugin
    {
        #region ctor
        public LoggerPlugin()
        {

        }
        #endregion

        #region members
        Messenger messenger;
        #endregion

        public string Name
        {
            get
            {
                return "Logger";
            }
        }
        public string Description
        {
            get
            {
                return "";
            }
        }
        public Messenger Messenger
        {
            get
            {
                return messenger;
            }

            set
            {
                messenger = value;
            }
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }


    }
}
