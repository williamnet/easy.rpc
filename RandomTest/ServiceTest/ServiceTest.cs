using Easy.Domain.ServiceFramework;
using Easy.Rpc.directory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomTest.ServiceTest
{
    public class ServiceTest
    {
        static ServiceFactory factory;

        static ServiceTest()
        {
            ServiceFactoryBuilder builder = new ServiceFactoryBuilder();

            factory = builder.Build(new System.IO.FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "servcies.xml")));

            DirectoryFactory.Register("rpc", new StaticDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rpc.config"), "prc"));
        }
        [Test]
        public void LoadTest()
        {
            factory.Get<IBaseService>().Select("a", "b");
        }
    }
}
