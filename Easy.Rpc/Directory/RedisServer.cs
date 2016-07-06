using Easy.Rpc.LoadBalance;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using Easy.Public.MyLog;

namespace Easy.Rpc.directory
{
    public class RedisServer : IRedis
    {
        readonly ConnectionMultiplexer redis;

        public RedisServer(string redisServer)
        {
            try
            {
                redis = ConnectionMultiplexer.Connect(redisServer);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Redis Connect Error", e.Message);
            }
        }
        public void Subscribe(Action<IList<Node>> action, string serviceName)
        {
            ISubscriber sub = redis.GetSubscriber();
            sub.Subscribe(serviceName, (c, m) =>
            {
                IList<Node> nodes = JsonConvert.DeserializeObject<IList<Node>>(m);
                action.Invoke(nodes);
            });
        }
    }
}
