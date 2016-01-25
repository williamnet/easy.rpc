using Easy.Rpc.LoadBalance;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.directory
{
    public class RedisServer : IRedis
    {
        readonly ConnectionMultiplexer redis;
        int databaseid = 0;

        public RedisServer(string redisServer, int databaseId)
        {
            redis = ConnectionMultiplexer.Connect(redisServer);
            this.databaseid = databaseId;
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
