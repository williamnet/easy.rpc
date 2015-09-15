
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.Cluster;
namespace RandomTest
{
	[TestFixture]
	public class FailoverClusterTest
	{
		[Test]
		public void FailoverClusterRetryTest()
		{
			IList<Node> nodes = new List<Node>();
			
			Node node1 = new Node("http://wwadfad.com", 100, true);
			Node node2 = new Node("http://sdfdslf.com", 100, true);
			Node node3 = new Node("http://sdfdaaaaaslf.com", 100, true);
			
			nodes.Add(node1);
			nodes.Add(node2);
			nodes.Add(node3);
			
			
			string result = new FailoverCluster().Invoke<String>(nodes, "a", new RandomBalance(), new TestInvoker(null, null));
			
			Assert.AreEqual("ok", result);
			
		}
		
		class TestInvoker:Invoker<String>
		{
			
			private int i = 0;
			public TestInvoker(object model, IDictionary<string, object> attachment)
				: base(model, attachment)
			{
				
			}
			
			public override String DoInvoke(Node node)
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
