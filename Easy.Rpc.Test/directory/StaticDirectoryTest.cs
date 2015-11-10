using System;
using System.IO;
using Easy.Rpc.directory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RandomTest
{
	[TestClass]
	public class StaticDirectoryTest
	{
		[TestMethod]
		public void StaticDirectoryLoadXMLTest()
		{
			string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rpc.config");
			var staticDirectory = new StaticDirectory(file,"order");

            //注册提供者目录
            //在实际代码中使用时，此方法调用应该放在应用启动时，如果时Web应用程序 ，需要放在Application_Start中调用
            DirectoryFactory.Register("order", new StaticDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rpc.config"), "order"));

           
           
		}
	}
}
