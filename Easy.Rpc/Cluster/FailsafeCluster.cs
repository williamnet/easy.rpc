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
		public const string NAME ="FailsafeCluster";
		
		public String Name()
		{
			return NAME;
		}
		
		public T Invoke<T>(IList<Node> nodes, string path, ILoadBalance loadbanlance, IInvoker<T> invoker)
		{
			Node node = loadbanlance.Select(nodes, path);
			
			try {
				return invoker.DoInvoke(node,path);	
			} catch {
			}
			
			return default(T);
		}
	}
}
