
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Easy.Rpc.LoadBalance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Easy.Rpc.Test
{
    [TestClass]
	public class RoundRobinLoadBalanceTest
	{
        [TestMethod]
        public void RoundRobinLoadBalanceSelectTest()
		{
			var node1 = new Node("a","http://www.baidu.com?node=1&weight=100", 5, true,"192.168.1.1");
			var node2 = new Node("a","http://www.baidu.com?node=2&weight=100", 10, true, "192.168.1.1");
			var node3 = new Node("a","http://www.baidu.com?node=3&weight=200", 5, true, "192.168.1.1");
			var node4 = new Node("a","http://www.baidu.com?node=4&weight=200", 30, true, "192.168.1.1");
			var node5 = new Node("a","http://www.baidu.com?node=5&weight=200", 100, true, "192.168.1.1");
			
			var list = new List<Node>();
			list.Add(node1);
			list.Add(node2);
			list.Add(node3);
			list.Add(node4);
			list.Add(node5);
			
			var roundRobinBalance = new RoundRobinLoadBalance();
			
			ConcurrentDictionary<String,AtomicPositiveInteger> conDic = new ConcurrentDictionary<String,AtomicPositiveInteger>();
			
			conDic.TryAdd(node1.Url, new AtomicPositiveInteger());
			conDic.TryAdd(node2.Url, new AtomicPositiveInteger());
			conDic.TryAdd(node3.Url, new AtomicPositiveInteger());
			conDic.TryAdd(node4.Url, new AtomicPositiveInteger());
			conDic.TryAdd(node5.Url, new AtomicPositiveInteger());
			
			for (var i = 0; i < 100000; i++) 
			{
				Node node = roundRobinBalance.Select(list, "test1");
				conDic[node.Url].GetAndIncrement();
			}

            int totalCount = conDic[node1.Url].Value() + conDic[node2.Url].Value() + conDic[node3.Url].Value() + conDic[node4.Url].Value() + conDic[node4.Url].Value();

            var result1 = (conDic[node4.Url].Value() * 1.0 / totalCount);
            var result2 = (100 * 1.0 / 150);

            Assert.IsTrue((result1 - result2) < 0.01, result1 + "," + result2);
		}
	}
}





















