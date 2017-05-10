using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.PluginInterface;
using MvvmFoundation.Wpf;
using System.Threading;

namespace Counter
{
    public class Counter : IPlugin
    {
        #region ctor
        public Counter()
        {

        }
        #endregion

        #region members
        Messenger msger;
        Thread counterThread;
        #endregion

        public string Description
        {
            get
            {
                return "Counter";
            }
        }

        public Messenger Messenger
        {
            get
            {
                return msger;
            }
            set
            {
                msger = value;
            }
        }

        public string Name
        {
            get
            {
                return "Counter";
            }
        }

        public void Dispose()
        {
            if (counterThread != null)
            {
                counterThread.Abort();
                counterThread.Join();
            }
            
        }

        public void Initialize()
        {
            counterThread = new Thread(new ThreadStart(StartCounter));
            counterThread.Start();
        }

        void StartCounter()
        {
            while(true)
            {
                Messenger.NotifyColleagues("Count", DateTime.Now.Second);
                Thread.Sleep(1000);
            }
        }
    }
}
