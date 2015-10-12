
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.directory;
using NUnit.Framework;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Linq;

namespace RandomTest
{
	[TestFixture]
	public class RedisDirectoryTest
	{
		[Test]
		public void TestMethod()
		{
			Redis myRedis = new Redis("10.8.7.138:6379");
			
			RedisDirectory rd = new RedisDirectory(myRedis, "order", "order", "order");
			
			IList<Node> nodes = rd.GetNodes(string.Empty);
			
			
			
			Assert.AreEqual(3, nodes.Count(m => m.IsAvailable));
			
			System.Threading.Thread.Sleep(90000);
			
		}
		[Test]
		public void GetDataTest()
		{
			
			IList<Node> nodes = new List<Node>();
			
			nodes.Add(new Node("order", "http://aaa.order1", 100, true));
			nodes.Add(new Node("order", "http://aaa.order2", 100, true));
			nodes.Add(new Node("order", "http://aaa.order3", 100, true));
			
			
			string strNodes = JsonConvert.SerializeObject(nodes, Formatting.None);
			
			
		}
	}
	
	
	
	public class Redis:Easy.Rpc.directory.IRedis
	{
		private readonly ConnectionMultiplexer redis;
		
		public Redis(string redisServer)
		{
			redis = ConnectionMultiplexer.Connect(redisServer);
		}
		
		public void UpdateNode(String node){
			
		}
		
		public void Publish(String channel)
		{
			redis.GetDatabase().Publish(channel, "");
		}
		
		public IList<Node> GetNodes(string key)
		{
			RedisValue[] values = redis.GetDatabase().ListRange(key);
			
			IList<Node> nodes = new List<Node>();
			foreach (var redisValue in values) {
				if (redisValue.HasValue) {
					
					Node node = JsonConvert.DeserializeObject<Node>(redisValue.ToString());
					nodes.Add(node);
				}
			}
			
			return nodes;
		}
		public void Subscribe(Action<IList<Node>> action, string channel, string key)
		{
			ISubscriber sub = redis.GetSubscriber();
			
			sub.Subscribe(channel, (c, m) => {
				IList<Node> nodes = this.GetNodes(key);
				action.Invoke(nodes);
			});
		}
	}
}
