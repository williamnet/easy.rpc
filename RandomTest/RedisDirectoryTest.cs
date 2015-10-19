
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
			var myRedis = new Redis("172.18.11.83:6379", 3);
			
			string data = this.GetNodes("product", true);
			myRedis.UpdateNode("product", "product", data);
			
			var rd1 = new RedisDirectory(myRedis, "product");
			IList<Node> productNodes = rd1.GetNodes();
			Assert.AreEqual(3, productNodes.Count(m => m.IsAvailable));
			
			data = this.GetNodes("product", false);
			
			myRedis.UpdateNode("product", "product", data);
			System.Threading.Thread.Sleep(2000);
			
			Assert.AreEqual(0, rd1.GetNodes().Count);
			
			
			string jsondata = this.GetNodes("order", true);
			myRedis.UpdateNode("order", "order", jsondata);
			
			var rd = new RedisDirectory(myRedis, "order");
			IList<Node> nodes = rd.GetNodes();
			Assert.AreEqual(3, nodes.Count(m => m.IsAvailable));
			
			jsondata = this.GetNodes("order", false);
			
			myRedis.UpdateNode("order", "order", jsondata);
			System.Threading.Thread.Sleep(2000);
			
			Assert.AreEqual(0, rd.GetNodes().Count);
		}
		String GetNodes(string name, bool enable)
		{
			IList<Node> nodes = new List<Node>();
			
			nodes.Add(new Node(name, "http://aaa.order1", 100, enable));
			nodes.Add(new Node(name, "http://aaa.order2", 100, enable));
			nodes.Add(new Node(name, "http://aaa.order3", 100, enable));
			string strNodes = JsonConvert.SerializeObject(nodes, Formatting.None);
			
			return strNodes;
		}
	}
	
	
	
	public class Redis:Easy.Rpc.directory.IRedis
	{
		readonly ConnectionMultiplexer redis;
		int databaseid = 0;
		
		public Redis(string redisServer, int databaseId)
		{
			redis = ConnectionMultiplexer.Connect(redisServer);
			this.databaseid = databaseId;
		}
		
		public void UpdateNode(string channel, string key, String node)
		{
			redis.GetDatabase(databaseid).StringSet(key, node);
			redis.GetDatabase(databaseid).Publish(channel, "");
			
		}
		
		public IList<Node> GetNodes(string serviceName)
		{
			RedisValue value = redis.GetDatabase(databaseid).StringGet(serviceName);
			
			if (value.HasValue) {
				
				IList<Node> nodes = JsonConvert.DeserializeObject<IList<Node>>(value.ToString());
				return nodes;
			}
			return new List<Node>(0);
		}
		public void Subscribe(Action<IList<Node>> action, string serviceName)
		{
			ISubscriber sub = redis.GetSubscriber();
			
			sub.Subscribe(serviceName, (c, m) => {
				IList<Node> nodes = this.GetNodes(serviceName);
				action.Invoke(nodes);
			});
		}
	}
}
