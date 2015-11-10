using Easy.Rpc.directory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.Test.directory
{
    [TestClass]
    public class DirectoryFactoryTest
    {
        [TestMethod]
        public void RegisterTest()
        {
            //注册提供者目录
            //在实际代码中使用时，此方法调用应该放在应用启动时，如果时Web应用程序 ，需要放在Application_Start中调用
            DirectoryFactory.Register("order", new StaticDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rpc.config"), "order"));

            IDirectory directory = DirectoryFactory.GetDirectory("order");
            Assert.IsNotNull(directory);
        }
    }
}
