
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.directory;
using NUnit.Framework;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace RandomTest
{
	[TestFixture]
	public class RedisDirectoryTest2
	{
		[Test]
		public void TestMethod()
		{
			var newRedis = new RedisServer("172.18.11.83:6379", 3);
			
			var directory = new RedisDirectory(newRedis, "OrderService");
			
			var nodes = directory.GetNodes();
			
			Thread.Sleep(10000000);
			
		}
	}
	
}
