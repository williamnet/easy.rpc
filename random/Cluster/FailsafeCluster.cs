using System;
using System.Collections.Generic;
using LoadBalance;
namespace Cluster
{
	public class FailsafeCluster : ICluster
	{
		public T Invoke<T>(IList<Node> nodes, string nodeGroupName, ILoadBalance loadbanlance,Invoker<T> invoker)
		{
			Node node = loadbanlance.Select(nodes,nodeGroupName);
			
			try
			{
				return invoker.DoInvoke(node);	
			}
			catch
			{}
			
			return default(T);
		}
	}
}
