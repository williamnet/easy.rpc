using System;
using System.Collections.Generic;
namespace Easy.Rpc.Cluster
{
	public class ClusterFactory
	{
		private static readonly IDictionary<String,Type> CLUSTERS = new Dictionary<String,Type>();
		private ClusterFactory()
		{
		}
		
		static ClusterFactory()
		{
			CLUSTERS.Add(FailfastCluster.NAME, typeof(FailfastCluster));
			CLUSTERS.Add(FailoverCluster.NAME, typeof(FailoverCluster));
			CLUSTERS.Add(FailsafeCluster.NAME, typeof(FailsafeCluster));
			CLUSTERS.Add(ForkingCluster.NAME, typeof(ForkingCluster));
		}
		
		public static ICluster GetCluser(String name)
		{
			if (CLUSTERS.ContainsKey(name)) {
				return Activator.CreateInstance(CLUSTERS[name]) as ICluster;
			}
			throw new KeyNotFoundException("cluser" + name + " == null");
		}

        public static void Register(string name, Type type)
        {
            CLUSTERS.Add(name, type);
        }
	}
}
