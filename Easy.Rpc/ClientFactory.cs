using System;
using System.Collections.Generic;
using System.Linq;
using Easy.Rpc.Cluster;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.Exception;
using Easy.Rpc.directory;
namespace Easy.Rpc
{
	public class ClientFactory
	{
		ClientFactory()
		{
		}
		public static T Invoke<T>(IInvoker<T> invoker)
		{
			Object[] attributes = invoker.GetType().GetCustomAttributes(true);
			
			var pathAttri = attributes.SingleOrDefault(a => a is PathAttribute) as PathAttribute;
			
			var clusterAttri = attributes.SingleOrDefault(a => a is ClusterAttribute) as ClusterAttribute;
			
			var loadBalanceAttri = attributes.SingleOrDefault(a => a is LoadBalanceAttribute) as LoadBalanceAttribute;
			
			if (pathAttri == null) {
				throw new PathNotFoundException("path attri error");
			}
			
			IList<Node> nodes = DirectoryFactory.GetDirectory(pathAttri.Directory).GetNodes(pathAttri.Provider);
			
			if (nodes.Count == 0) {
				throw new NodeNoFoundException("node length is 0");
			}
			
			ICluster cluster = GetCluster(clusterAttri);
			ILoadBalance loadBalance = GetLoadBalance(loadBalanceAttri);
			
			return cluster.Invoke<T>(nodes, pathAttri.Path, loadBalance, invoker);
		}
		static ILoadBalance GetLoadBalance(LoadBalanceAttribute attri)
		{
			ILoadBalance loadBalance = null;
			if (attri == null) {
				loadBalance = LoadBalanceFactory.GetLoadBalance(RandomLoadBalance.NAME);
			} else {
				loadBalance = LoadBalanceFactory.GetLoadBalance(attri.Name);
			}
			return loadBalance;
		}
		
		static ICluster GetCluster(ClusterAttribute attri)
		{
			ICluster cluster = null;
			if (attri == null) {
				cluster = ClusterFactory.GetCluser(FailoverCluster.NAME);
			} else {
				cluster = ClusterFactory.GetCluser(attri.Name);
			}
			return cluster;
		}
	}
}



















