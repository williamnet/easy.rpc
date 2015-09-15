/*
 * 由SharpDevelop创建。
 * 用户： 晓静
 * 日期: 2015-09-13
 * 时间: 13:57
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using NUnit.Framework;
using LoadBalance;
namespace RandomTest
{
	[TestFixture]
	public class RoundRobinLoadBalanceTest
	{
		[Test]
		public void TestMethod()
		{
			var node1 = new Node("http://www.baidu.com?node=1&weight=100", 5, true);
			var node2 = new Node("http://www.baidu.com?node=2&weight=100", 10, true);
			var node3 = new Node("http://www.baidu.com?node=3&weight=200", 5, true);
			var node4 = new Node("http://www.baidu.com?node=4&weight=200", 30, true);
			var node5 = new Node("http://www.baidu.com?node=5&weight=200", 100, true);
			
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
			
			for (var i = 1; i < 10001; i++) 
			{
				Node node = roundRobinBalance.Select(list, "test1");
				conDic[node.Url].GetAndIncrement();
				//System.Diagnostics.Debug.WriteLine(node.Url);
			}
			System.Diagnostics.Debug.WriteLine(conDic[node1.Url].Value());
			System.Diagnostics.Debug.WriteLine(conDic[node2.Url].Value());
			System.Diagnostics.Debug.WriteLine(conDic[node3.Url].Value());
			System.Diagnostics.Debug.WriteLine(conDic[node4.Url].Value());
			System.Diagnostics.Debug.WriteLine(conDic[node5.Url].Value());
			//Assert.AreEqual(6, conDic[node1.Url].Value());
			//Assert.AreEqual(15, conDic[node2.Url].Value());
			//Assert.AreEqual(57, conDic[node3.Url].Value());
		}
	}
}





















