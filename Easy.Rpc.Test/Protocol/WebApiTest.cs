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
        [Description("测试POST没有提交任务参数")]
        public void PostWithoutDataTest()
        {
            string result = factory.Get<ITestService>().GetStringResult();
            Assert.AreEqual("Ok", result);
        }
        [TestMethod]
        public void PostDataTest()
        {
            var testData = new TestData()
            {
                Age = 100,
                CreatDate = DateTime.Now,
                IsOk = true,
                Money = 23.93m,
                Name = "X李"
            };

            var resultTestData = factory.Get<ITestService>().GetData(testData, "xiao");

            Assert.AreEqual(testData.Age, resultTestData.Age);
            Assert.AreEqual(testData.CreatDate.Hour, resultTestData.CreatDate.Hour);
            Assert.AreEqual(testData.IsOk, resultTestData.IsOk);
            Assert.AreEqual(testData.Money, resultTestData.Money);
            Assert.AreEqual(testData.Name + "xiao", resultTestData.Name);
        }

        [ClassCleanup]
        public static void Clear()
        {
            disposable.Dispose();
        }
    }

}
