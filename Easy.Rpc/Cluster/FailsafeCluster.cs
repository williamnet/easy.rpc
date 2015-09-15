using System;
using System.Collections.Generic;
using Easy.Rpc.LoadBalance;
namespace Easy.Rpc.Cluster
{
	/// <summary>
	/// 失败安全，出现异常时，直接忽略。通常用于写入审计日志等操作。
	/// </summary>
	public class FailsafeCluster : ICluster
	{
		public T Invoke<T>(IList<Node> nodes, string nodeGroupName, ILoadBalance loadbanlance, Invoker<T> invoker)
		{
			Node node = loadbanlance.Select(nodes, nodeGroupName);
			
			try {
				return invoker.DoInvoke(node);	
			} catch {
			}
			
			return default(T);
		}
	}
}
