
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
		public void RedisNotifyTest()
		{
			Redis myRedis = new Redis("10.8.7.138:6379");
			
			string jsondata = this.GetNodes(true);
			myRedis.UpdateNode("order", "order",jsondata);
			
			
			
			RedisDirectory rd = new RedisDirectory(myRedis, "order", "order", "order");
			IList<Node> nodes = rd.GetNodes();
			Assert.AreEqual(3, nodes.Count(m => m.IsAvailable));
			
			jsondata = this.GetNodes(false);
			
			myRedis.UpdateNode("order", "order", jsondata);
			System.Threading.Thread.Sleep(2000);
			
			Assert.AreEqual(0, rd.GetNodes().Count);
		}
		[Test]
		public void GetDataTest()
		{
			String jsondata = this.GetNodes(true);
			new Redis("10.8.7.138:6379").UpdateNode("order", "order", jsondata);
		}
		
		private String GetNodes(bool enable)
		{
			IList<Node> nodes = new List<Node>();
			
			nodes.Add(new Node("order", "http://aaa.order1", 100, enable));
			nodes.Add(new Node("order", "http://aaa.order2", 100, enable));
			nodes.Add(new Node("order", "http://aaa.order3", 100, enable));
			string strNodes = JsonConvert.SerializeObject(nodes, Formatting.None);
			
			return strNodes;
		}
	}
	
	
	
	public class Redis:Easy.Rpc.directory.IRedis
	{
		private readonly ConnectionMultiplexer redis;
		
		public Redis(string redisServer)
		{
			redis = ConnectionMultiplexer.Connect(redisServer);
		}
		
		public void UpdateNode(string channel, string key, String node)
		{
			redis.GetDatabase().StringSet(key, node);
			redis.GetDatabase().Publish(channel, "");
			
		}
		
		public IList<Node> GetNodes(string key)
		{
			RedisValue value = redis.GetDatabase().StringGet(key);
			
			if (value.HasValue) {
				
				IList<Node> nodes = JsonConvert.DeserializeObject<IList<Node>>(value.ToString());
				return nodes;
			}
			return new List<Node>(0);
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
