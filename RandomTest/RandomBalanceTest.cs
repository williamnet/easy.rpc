
using System;
using NUnit.Framework;
using LoadBalance;
using System.Collections.Generic;
using System.Diagnostics;
namespace RandomTest
{
	[TestFixture]
	public class RandomBalanceTest
	{
		[Test]
		public void RandomBalanceSelectTest()
		{
			var node1 = new Node("http://www.baidu.com?node=1&weight=5", 5, true);
			var node2 = new Node("http://www.baidu.com?node=2&weight=10", 10, true);
			var node3 = new Node("http://www.baidu.com?node=3&weight=20", 20, true);
			
			var list = new List<Node>();
			list.Add(node1);
			list.Add(node2);
			list.Add(node3);
			
			int node1Count = 0;
			int node2Count = 0;
			int node3Count = 0;
			
			var randomBalance = new RandomBalance();
			
			for (var i = 0; i < 1000; i++) {
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
			
			Assert.IsTrue((node1Count * 1.0 / totalCount) <= (5 * 1.0 / 35));
		}
	}
}
