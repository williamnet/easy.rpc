using Easy.Domain.ServiceFramework;
using Easy.Rpc.directory;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.Test.Protocol
{
    [TestClass]
    public class WebApiTest
    {
        static ServiceFactory factory;
        static IDisposable disposable;
        [ClassInitialize]
        public static void SetUp(TestContext test)
        {
            string baseUrl = "http://localhost:8787";
            disposable = WebApp.Start<Startup>(new StartOptions(baseUrl));

            DirectoryFactory.Register("testServiceDirectory", new StaticDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testServiceDirectory.config"), "testServiceDirectory"));

            Easy.Domain.ServiceFramework.ServiceFactoryBuilder builder = new Domain.ServiceFramework.ServiceFactoryBuilder();

            factory = builder.Build(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "servcies.xml")));

        }
        [TestMethod]
        public void Test()
        {
            string result = factory.Get<ITestService>().GetStringResult();
            Assert.AreEqual("ok", result);
        }

        [ClassCleanup]
        public static void Clear()
        {
            disposable.Dispose();
        }
    }

}
