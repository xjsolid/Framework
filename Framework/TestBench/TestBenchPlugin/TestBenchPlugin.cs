using Framework.PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestBenchPlugin
{
    public class TestBenchPlugin : IPlugin
    {
        #region members
        TestBench tb;
        #endregion

        #region IPlugin implement
        public string Name
        {
            get
            {
                return "TestBench";
            }
        }

        public string Description
        {
            get
            {
                return "TestBench";
            }
        }

        public void Initialize()
        {
            tb = new TestBench();
            tb.FormClosing += Tb_FormClosing;
        }

        private void Tb_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Raven.BroadcastMessage(GolbalMessages.MainAppExit);
        }

        public void Start()
        {
            tb.Show();
        }

        public void Dispose()
        {
            tb.Close();
        }
        

        #endregion
    }
}
