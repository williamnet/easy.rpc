
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Easy.Rpc.directory;
using NUnit.Framework;

namespace RandomTest
{
	[TestFixture]
	public class StaticDirectoryTest
	{
		[Test]
		public void StaticDirectoryLoadXMLTest()
		{
			string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rpc.config");
			var staticDirectory = new StaticDirectory(file);
			
			var nodes = staticDirectory.GetNodes("order");
			
			Assert.IsTrue(nodes.Count > 0);
		}
		[Test]
		public void StaticDirectoryRefreshTest()
		{
			string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rpc.config");
			var staticDirectory = new StaticDirectory(file);
			var random = new Random();
			for (var i = 0; i < 10; i++) {
				Task.Factory.StartNew(() => {
					//Thread.Sleep(random.Next(1, 20));
					System.Diagnostics.Debug.WriteLine("read");
					Assert.IsTrue(staticDirectory.GetNodes("order").Count > 0);
				});
			}
			
			Thread.Sleep(10);
			
			var rpc2 = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rpc2.config"));
			
			staticDirectory.Refresh(rpc2.FullName);
			
			Thread.Sleep(5);
			
			Assert.AreEqual(3, staticDirectory.GetNodes("order").Count);
			
			
		}
	}
}
