using Microsoft.VisualStudio.TestTools.UnitTesting;
using Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Host.Tests
{
    [TestClass()]
    public class HostAppTests
    {
        [TestMethod()]
        public void StartUpTest()
        {
            HostApp.PlugMgr.LoadPlugins(Directory.GetCurrentDirectory());
            HostApp.StartUp();
            Assert.AreEqual(2, HostApp.PlugMgr.Plugins.Count);
        }
    }
}