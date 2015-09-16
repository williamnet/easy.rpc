
using System;
using System.Collections.Generic;
using Easy.Rpc.LoadBalance;
namespace Easy.Rpc.Cluster
{
	/// <summary>
	/// Description of ICluster.
	/// </summary>
	public interface ICluster
	{
		String Name();
		
		T Invoke<T>(IList<Node> nodes, string path, ILoadBalance loadbanlance,IInvoker<T> invoker);
	}
}
