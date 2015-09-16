using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.Cluster;
namespace RandomTest
{
	[TestFixture]
	public class ForkingClusterTest
	{
		[Test]
		public void TestMethod()
		{
			IList<Node> nodes = new List<Node>();
			
			Node node1 = new Node("a","http://a", 100, true);
			Node node2 = new Node("a","http://b", 100, true);
			
			nodes.Add(node1);
			nodes.Add(node2);
			
			var forkcluster = new ForkingCluster(2,3000);
			
			string result = forkcluster.Invoke(nodes, "a", new RandomBalance(), new TestInvoker(null, null));
			
			Assert.AreEqual("http://a",result);
		}
		
		class TestInvoker :Invoker<String>
		{
			public TestInvoker(object model, IDictionary<String,Object> attachment)
				: base(model, attachment)
			{
				
			}

			#region implemented abstract members of Invoker
			public override string JoinURL(Node node, string path)
			{
				return node.Url+ path;
			}
			#endregion			
		
			public override String DoInvoke(Node node,string path)
			{
				System.Diagnostics.Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
				
				if(node.Url == "http://a"){
					Thread.Sleep(50);
					return node.Url;
				}
				else {
					Thread.Sleep(2000);
					return node.Url;
				}
			}
		}
	}
}




















