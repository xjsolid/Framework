using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.PluginInterface
{
    enum MessageEnum
    {
        #region MainUI messages
        MainUIClose,
        #endregion
    }

    public class Messages
    {
        #region mainUI messages
        public static string MainUIClose
        {
            get { return MessageEnum.MainUIClose.ToString(); }
        }
        #endregion
    }
}
