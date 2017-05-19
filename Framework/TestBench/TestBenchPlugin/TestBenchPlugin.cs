using Framework.PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestBenchPlugin
{
    public class TestBench : IPlugin
    {
        public TestBench()
        {

        }

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

        public void Dispose()
        {
        }

        public void Initialize()
        {
        }

        public void Start()
        {
            MessageBox.Show("Started!");
        }
    }
}
