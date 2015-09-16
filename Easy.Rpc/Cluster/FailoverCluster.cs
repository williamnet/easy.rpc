
using System;
using System.Linq;
using System.Collections.Generic;
using Easy.Rpc.LoadBalance;

namespace Easy.Rpc.Cluster
{
	/// <summary>
	///失败自动切换，当出现失败，重试其它服务器。
	///通常用于读操作，但重试会带来更长延迟。
	///可通过retries="2"来设置重试次数(不含第一次)。
	/// </summary>
	public class FailoverCluster:ICluster
	{
		public const string NAME ="FailoverCluster";
		public FailoverCluster(int retries)
		{
			this.Retries = retries;
		}
		
		public String Name()
		{
			return NAME;
		}
		
		public FailoverCluster()
		{
			this.Retries = 2;
		}
			
		/// <summary>
		/// 重试次数
		/// </summary>
		public int Retries {
			get;
			set;
		}
		
		public T Invoke<T>(IList<Node> nodes, string path, ILoadBalance loadbanlance, IInvoker<T> invoker)
		{
			IList<Node> invokedNodes = new List<Node>();

			int retries = -1;
			for (var i = 0; i <= this.Retries; i++) {
				retries++;
				try {
					IList<Node> thisInvokeNodes = nodes.Except(invokedNodes).ToList();
					if (thisInvokeNodes.Count == 0) {
						thisInvokeNodes = nodes;
					}
					Node node = loadbanlance.Select(thisInvokeNodes, path);
					
					invokedNodes.Add(node);
					
					return invoker.DoInvoke(node,path);
				} catch (System.Exception) {
					if (retries >= this.Retries) {
						throw;
					}
				}
			}
			return default(T);
		}
	}
}

















