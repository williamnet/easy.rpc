
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Easy.Rpc.LoadBalance;

namespace Easy.Rpc.directory
{
    /// <summary>
    /// Redis实现目录服务
    /// </summary>
    public class RedisDirectory : IDirectory
    {
        readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        readonly IList<Node> nodes = new List<Node>();
        readonly IRedis redis;
        readonly string serviceName;

        public RedisDirectory(IRedis redis, string serviceName,IList<Node> initNodes)
        {

            this.redis = redis;
            this.serviceName = serviceName;

            this.nodes = initNodes;
            this.redis.Subscribe(Refresh, serviceName);
        }
        /// <summary>
        /// 目录名称
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return this.serviceName;
        }

        public IList<Node> GetNodes()
        {
            cacheLock.EnterReadLock();
            try
            {
                return nodes.Where(m => m.IsAvailable).ToList();

            }
            finally
            {
                cacheLock.ExitReadLock();
            }
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// redis订阅通知
        /// </summary>
        /// <param name="newNodes"></param>
        void Refresh(IList<Node> newNodes)
        {
            cacheLock.EnterWriteLock();
            try
            {
                nodes.Clear();
                foreach (var node in newNodes)
                {
                    nodes.Add(node);
                }
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }
    }
}
