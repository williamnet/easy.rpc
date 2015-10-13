
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
	public class RedisDirectory:IDirectory
	{
		readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
		readonly IList<Node> nodes = new List<Node>();
		readonly IRedis redis;
		readonly string name;
		readonly string channel;
		readonly string key;
		
		public RedisDirectory(IRedis redis, string name, string channel, string key)
		{
			
			this.redis = redis;
			this.name = name;
			this.channel = channel;
			this.key = key;
			
			this.nodes = this.redis.GetNodes(key);
			this.redis.Subscribe(Refresh, this.channel, this.key);
		}
		/// <summary>
		/// 目录名称
		/// </summary>
		/// <returns></returns>
		public string Name()
		{
			return this.name;
		}

		public IList<Node> GetNodes()
		{
			cacheLock.EnterReadLock();
			try {
				return nodes.Where(m => m.IsAvailable).ToList();
				
			} finally {
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
			try {
				nodes.Clear();
				foreach (var node in newNodes) {
					nodes.Add(node);
				}
			} finally {
				cacheLock.ExitWriteLock();
			}
		}
	}
	
	public interface IRedis
	{
		/// <summary>
		/// 根据redis的key查询Nodes列表
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		IList<Node> GetNodes(string key);
		/// <summary>
		/// Reids Nodes变化通知订阅
		/// </summary>
		/// <param name="action"></param>
		/// <param name="channel"></param>
		/// <param name="key"></param>
		void Subscribe(Action<IList<Node>> action, string channel, string key);
	}
}
