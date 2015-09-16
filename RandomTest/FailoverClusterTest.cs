
using System;
using System.Collections.Generic;
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
			IList<Node> nodes = new List<Node>();
			
			Node node1 = new Node("a","http://wwadfad.com", 100, true);
			Node node2 = new Node("a","http://sdfdslf.com", 100, true);
			Node node3 = new Node("a","http://sdfdaaaaaslf.com", 100, true);
			
			nodes.Add(node1);
			nodes.Add(node2);
			nodes.Add(node3);
			
			
			ClientFactory.Invoke(new TestInvoker(null,null));
			
			string result = new FailoverCluster().Invoke<String>(nodes, "a", new RandomBalance(), new TestInvoker(null, null));
			
			Assert.AreEqual("ok", result);
			
		}
		[Path("/oder/create","a")]
		class TestInvoker:Invoker<String>
		{
			
			private int i = 0;
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
			public override String DoInvoke(Node node,string path)
			{
				System.Diagnostics.Debug.WriteLine("调用次数："+ i);
				System.Diagnostics.Debug.WriteLine("调用的URL："+ node.Url);
				
				if (i == 0 || i == 1) {
					i++;
					throw new Exception();
				}
				
				if (i == 2) {
					return "ok";
				}
				
				
				return string.Empty;
			}
				
		}
	}
}
