using Framework.PluginInterface;
using MvvmFoundation.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestBench
{
    public partial class TestForm : Form, IFormPlugin
    {
        public TestForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        #region members
        Messenger messenger;
        LogContext logContext;
        #endregion

        public string Description
        {
            get
            {
                return "";
            }
        }

        public Form Form
        {
            get
            {
                return this;
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
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log-TestBench");
            string logPath = string.Format("{0}\\{1}", logDir, DateTime.Now.ToString("yyyyMMdd-hhmmss"));
            logContext = new LogContext("Initializing TestBench", logPath, LogLevel.Info, true);
            Messenger.Register<int>("Count", OnCount);
        }

        void OnCount(int i)
        {
            this.textBox_Counter.Text = i.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Caution!");
        }

        private void TestBench_FormClosing(object sender, FormClosingEventArgs e)
        {
            messenger.NotifyColleagues(Messages.LogDispose, logContext);
            messenger.NotifyColleagues(Messages.MainUIClose);
        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        static int logCount = 0;
        private void button_LogTest_Click(object sender, EventArgs e)
        {
            Log(string.Format("Log test: {0}", logCount), LogLevel.Debug);
            Log("Log test Error", LogLevel.Error);
            logCount++;
        }

        void Log(string message, LogLevel logLevel)
        {
            logContext.LogContent = message;
            logContext.LogLevel = logLevel;
            messenger.NotifyColleagues(Messages.Log, logContext);
        }

        private void button_Exception_Click(object sender, EventArgs e)
        {
            throw new Exception("exception test");
        }


    }
}
