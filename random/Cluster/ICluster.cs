
using System;
using System.Collections.Generic;
using LoadBalance;
namespace Cluster
{
	/// <summary>
	/// Description of ICluster.
	/// </summary>
	public interface ICluster
	{
		T Invoke<T>(IList<Node> nodes, string nodeGroupName, ILoadBalance loadbanlance,Invoker<T> invoker);
	}
}
