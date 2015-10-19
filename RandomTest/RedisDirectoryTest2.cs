
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
			var newRedis = new NewRedis("172.18.11.83:6379", 3);
			
			var directory = new RedisDirectory(newRedis, "OrderService");
			
			var nodes = directory.GetNodes();
			
			Thread.Sleep(10000000);
			
		}
	}
	
	class NewRedis:Easy.Rpc.directory.IRedis
	{
		/// <summary>
		/// 记录node 和 provider set类型
		/// </summary>
		const string NodeInProvider = "nod_in_provider_{0}";
		/// <summary>
		/// Node实体数据，用于存储实体数据 string类型
		/// </summary>
		const string NodeEntity = "node_entity_{0}";
		const string ProviderEntityNames = "provider_entity_names";

        
		readonly ConnectionMultiplexer redis;
		int databaseid = 0;
		
		public NewRedis(string redisServer, int databaseId)
		{
			redis = ConnectionMultiplexer.Connect(redisServer);
			this.databaseid = databaseId;
		}
		public IList<Node> GetNodes(string serviceName)
		{
			double? value = redis.GetDatabase(databaseid).SortedSetScore(ProviderEntityNames, serviceName);
			
			if (value == null || !value.HasValue) {
				return new List<Node>();
			}
			
			int providerId = (int)value.Value;
			
			RedisValue[] values = redis.GetDatabase(databaseid).SetMembers(string.Format(NodeInProvider, providerId));

			string[] keys = values.Select(m => string.Format(NodeEntity, m)).ToArray();
			
			RedisKey[] redisKeys = keys.Select(k => (RedisKey)k).ToArray();

			RedisValue[] nodes = redis.GetDatabase(databaseid).StringGet(redisKeys);

			return nodes.Select(m => JsonConvert.DeserializeObject<Node>(m.ToString())).ToArray();
			
			
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
