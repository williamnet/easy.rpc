
using System;
using System.Collections.Generic;
namespace Easy.Rpc.LoadBalance
{

	public class LoadBalanceFactory
	{
		private readonly static IDictionary<String,ILoadBalance> LoadBalance = new Dictionary<String,ILoadBalance>();
		
		private LoadBalanceFactory()
		{
		}
		static LoadBalanceFactory()
		{
			LoadBalance.Add(RandomLoadBalance.NAME, new RandomLoadBalance());
			LoadBalance.Add(RoundRobinLoadBalance.NAME, new RoundRobinLoadBalance());
		}

		public static ILoadBalance GetLoadBalance(String name)
		{
			if (LoadBalance.ContainsKey(name)) {
				return LoadBalance[name];
			}
			throw new KeyNotFoundException("loadbalance" + name + " == null");
		}
	}
}
