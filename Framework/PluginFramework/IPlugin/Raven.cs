using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.PluginInterface
{
    public class Raven
    {
        #region members
        static Messenger messenger = new Messenger();
        #endregion

        #region methods
        public static void ExcepectingMessage(string message, Action callback)
        {
            messenger.Register(message, callback);
        }
        public static void ExcepectingMessage<T>(string message, Action<T> callback)
        {
            messenger.Register(message, callback);
        }

        public static void BroadcastMessage(string message)
        {
            messenger.NotifyColleagues(message);
        }

        public static void BroadcastMessage(string message, object parameter)
        {
            messenger.NotifyColleagues(message, parameter);
        }
        #endregion
    }
}
