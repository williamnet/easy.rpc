
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Easy.Rpc.directory;
using NUnit.Framework;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.Cluster;
using Easy.Rpc;
namespace RandomTest
{
	[TestFixture]
	public class FailoverClusterTest
	{
		[Test]
		public void FailoverClusterRetryTest()
		{	
			
			String rpc = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rpc.config");
			StaticDirectory dir = new StaticDirectory(rpc, "order");
			
			DirectoryFactory.Register(dir.Name(), dir);
			
			Task.Factory.StartNew(() => { 
				for (int i = 0; i < 10000; i++) {
					String result = ClientInvoker.Invoke(new TestInvoker(null, null));
					System.Diagnostics.Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
					Assert.AreEqual("ok", result);
				}       
			});
			
			
			Thread.Sleep(20000);
		}
		[Directory("order", "/create")]
		[Cluster(FailoverCluster.NAME)]
		[LoadBalance(RoundRobinLoadBalance.NAME)]
		class TestInvoker:Invoker<String>
		{
			static readonly object lockobject = new object();
			
			public TestInvoker(object model, IDictionary<string, object> attachment)
				: base(model, attachment)
			{
				
			}

			#region implemented abstract members of Invoker
			public override string JoinURL(Node node, string path)
			{
				return node.Url + path;
			}
			#endregion
			public override String DoInvoke(Node node, string path)
			{
				lock (lockobject) {
					System.IO.File.AppendAllText(@"F:\a.txt", node.Url + "\r\n");
				}
				return "ok";
			}
		}
	}
}
