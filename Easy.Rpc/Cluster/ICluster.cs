
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
		T Invoke<T>(IList<Node> nodes, string nodeGroupName, ILoadBalance loadbanlance,Invoker<T> invoker);
	}
}
