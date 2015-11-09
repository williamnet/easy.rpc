using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easy.Public;
using Easy.Rpc.Cluster;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.Exception;
using Easy.Rpc.directory;
namespace Easy.Rpc
{
	/// <summary>
	/// 远程调用
	/// </summary>
	public class ClientInvoker
	{
		ClientInvoker()
		{
		}
		
		public static T Invoke<T>(IInvoker<T> invoker, InvokerContext invokerContext)
		{
			if (invokerContext.Directory == null) {
				throw new PathNotFoundException("directory attri error");
			}
			
			IList<Node> nodes = DirectoryFactory.GetDirectory(invokerContext.Directory.Directory).GetNodes();
			
			ICluster cluster = GetCluster(NullHelper.IfNull(invokerContext.Cluster, null).Name);
			ILoadBalance loadBalance = GetLoadBalance(NullHelper.IfNull(invokerContext.LoadBalance, null).Name);
			
			
			return cluster.Invoke<T>(nodes, invokerContext.Directory.Path, loadBalance, invoker);
			
		}
		
		public static T Invoke<T>(IInvoker<T> invoker)
		{
			Object[] attributes = invoker.GetType().GetCustomAttributes(true);
			
			var directoryAttri = attributes.SingleOrDefault(a => a is DirectoryAttribute) as DirectoryAttribute;
			
			var clusterAttri = attributes.SingleOrDefault(a => a is ClusterAttribute) as ClusterAttribute;
			
			var loadBalanceAttri = attributes.SingleOrDefault(a => a is LoadBalanceAttribute) as LoadBalanceAttribute;
			
			if (directoryAttri == null) {
				throw new PathNotFoundException("directory attri error");
			}
			
			IList<Node> nodes = DirectoryFactory.GetDirectory(directoryAttri.Directory).GetNodes();
			
			if (nodes.Count == 0) {
				throw new NodeNoFoundException("node length is 0");
			}
			
			ICluster cluster = GetCluster(NullHelper.IfNull(clusterAttri, null).Name);
			ILoadBalance loadBalance = GetLoadBalance(NullHelper.IfNull(loadBalanceAttri, null).Name);
			
			return cluster.Invoke<T>(nodes, directoryAttri.Path, loadBalance, invoker);
		}
		
		public static async Task<T> InvokeAsync<T>(IInvoker<T> invoker)
		{
			return await Task.Factory.StartNew(() => {
				return Invoke(invoker);
			});
		}
		
		static ILoadBalance GetLoadBalance(string loadBalanceName)
		{
			ILoadBalance loadBalance = null;
			if (string.IsNullOrEmpty(loadBalanceName)) {
				loadBalance = LoadBalanceFactory.GetLoadBalance(RandomLoadBalance.NAME);
			} else {
				loadBalance = LoadBalanceFactory.GetLoadBalance(loadBalanceName);
			}
			return loadBalance;
		}
		
		static ICluster GetCluster(string clusterName)
		{
			ICluster cluster = null;
			if (string.IsNullOrEmpty(clusterName)) {
				cluster = ClusterFactory.GetCluser(FailoverCluster.NAME);
			} else {
				cluster = ClusterFactory.GetCluser(clusterName);
			}
			return cluster;
		}
	}
}



















