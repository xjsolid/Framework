using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.PluginInterface;
using MvvmFoundation.Wpf;

namespace TestBench
{
    class TestBenchPlugin : IFormPlugin
    {
        #region ctor
        public TestBenchPlugin(Messenger messenger)
        {
            this.messenger = messenger;
        }
        #endregion

        #region members
        static Form testBenchForm = new TestBench();
        Messenger messenger;
        #endregion
        public string Name
        {
            get
            {
                return "TestBenchForm";
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
            get { return messenger; }
            set { messenger = value; }
        }

        public Form Form
        {
            get
            {
                return testBenchForm;
            }
        }

        public void Initialize()
        {

        }

        public void Dispose()
        {
            testBenchForm.Dispose();
        }
    }
}
