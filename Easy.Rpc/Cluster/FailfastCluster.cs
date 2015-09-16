using System;
using System.Collections.Generic;
using Easy.Rpc.LoadBalance;
namespace Easy.Rpc.Cluster
{
	/// <summary>
	/// 快速失败，只发起一次调用，失败立即报错。通常用于非幂等性的写操作，比如新增记录。
	/// </summary>
	public class FailfastCluster:ICluster
	{
		public const string NAME = "FailfastCluster";
		public String Name()
		{
			return NAME;
		}
		
		
		public T Invoke<T>(IList<Node> nodes, string path, ILoadBalance loadbanlance, IInvoker<T> invoker)
		{
			Node node = loadbanlance.Select(nodes, path);
			return invoker.DoInvoke(node,path);
		}
	}
}
