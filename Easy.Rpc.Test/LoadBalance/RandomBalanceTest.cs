
using System;

using Easy.Rpc.LoadBalance;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Easy.Rpc.Test
{
    [TestClass]
	public class RandomBalanceTest
	{
        [TestMethod]
		public void RandomBalanceSelectTest()
		{
			var node1 = new Node("a","http://www.baidu.com?node=1&weight=5", 100, true,"192.168.1.10");
			var node2 = new Node("a","http://www.baidu.com?node=2&weight=10", 500, true, "192.168.1.10");
            var node3 = new Node("a", "http://www.baidu.com?node=3&weight=20", 1000, true, "192.168.1.10");
			
			var list = new List<Node>();
			list.Add(node1);
			list.Add(node2);
			list.Add(node3);
			
			int node1Count = 0;
			int node2Count = 0;
			int node3Count = 0;
			
			var randomBalance = new RandomLoadBalance();
			
			for (var i = 0; i < 100000; i++) {
				Node node = randomBalance.Select(list, string.Empty);
				
				if (node == node3) {
					node3Count++;
				}
				
				if (node == node2) {
					node2Count++;
				}
				
				if (node == node1) {
					node1Count++;
				}
			}
			
			int totalCount = node1Count + node2Count + node3Count;

            var result1 = (node1Count * 1.0 / totalCount);
            var result2 = (100 * 1.0 / 1600);

            Assert.IsTrue((result1 - result2) < 0.01, result1 + "," + result2);
		}
	}
}
