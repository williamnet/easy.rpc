
using System;

namespace Easy.Rpc
{
	public class InvokerContext
	{
		public InvokerContext(DirectoryContext directoryContext, ClusterContext clusterContext, LoadBalanceContext loadBalanceContext)
		{
			this.Directory = directoryContext;
			this.Cluster = clusterContext;
			this.LoadBalance = loadBalanceContext;
		}
		
		public DirectoryContext Directory {
			get;
			private set;
		}
		public ClusterContext Cluster {
			get;
			private set;
		}
		public LoadBalanceContext LoadBalance {
			get;
			private set;
		}
	}
	
}
