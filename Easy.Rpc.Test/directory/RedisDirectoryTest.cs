
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.directory;
using Newtonsoft.Json;
using StackExchange.Redis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RandomTest
{
	[TestClass]
	public class RedisDirectoryTest
	{
        //[TestMethod]
		public void TestMethod()
		{
			var newRedis = new RedisServer("172.18.11.83:6379", 3);
			var directory = new RedisDirectory(newRedis, "OrderService");
			
			var nodes = directory.GetNodes();

            Assert.IsTrue(nodes.Count > 0);
		}
	}
	
}
