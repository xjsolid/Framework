using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.PluginInterface;
using MvvmFoundation.Wpf;

namespace TestBench
{
    public class TestBenchPlugin : IFormPlugin
    {
        #region ctor
        public TestBenchPlugin()
        {
        }
        #endregion

        #region members
        static Form testBenchForm = new TestBench();
        Messenger messenger;
        #endregion

        #region props
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
            testBenchForm.FormClosing += TestBenchForm_FormClosing;
        }

        private void TestBenchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            messenger.NotifyColleagues(Messages.MainUIClose);
        }

        public void Show()
        {
            Form.Show();
        }

        public void Dispose()
        {
            testBenchForm.Dispose();
        }
        #endregion
    }
}
