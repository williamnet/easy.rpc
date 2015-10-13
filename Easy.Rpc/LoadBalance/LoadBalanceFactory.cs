
using System;
using System.Collections.Generic;
using System.Linq;

namespace Easy.Rpc.LoadBalance
{
	/// <summary>
	/// 负载均衡工厂类
	/// </summary>
	public class LoadBalanceFactory
	{
		readonly static IDictionary<String,ILoadBalance> LoadBalance = new Dictionary<String,ILoadBalance>();
		
		LoadBalanceFactory()
		{
		}
		static LoadBalanceFactory()
		{
			LoadBalance.Add(RandomLoadBalance.NAME, new RandomLoadBalance());
			LoadBalance.Add(RoundRobinLoadBalance.NAME, new RoundRobinLoadBalance());
		}
		
		public static void Register(ILoadBalance loadBalance)
		{
			if (!LoadBalance.ContainsKey(loadBalance.Name())) {
				LoadBalance.Add(loadBalance.Name(),loadBalance);
			}
		}
		
		public IList<ILoadBalance> LoadBalances(){
			return LoadBalance.Values.ToList();
		}
		
		public static void Clear(){
			LoadBalance.Clear();
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
