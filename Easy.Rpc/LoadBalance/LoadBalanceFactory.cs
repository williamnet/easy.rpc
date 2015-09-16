
using System;
using System.Collections.Generic;
namespace Easy.Rpc.LoadBalance
{

	public class LoadBalanceFactory
	{
		private readonly static IDictionary<String,Type> LoadBalance =new Dictionary<String,Type>();
		
		private LoadBalanceFactory()
		{
		}
		static LoadBalanceFactory()
		{
			LoadBalance.Add(RandomBalance.NAME,typeof(RandomBalance));
			LoadBalance.Add(RoundRobinLoadBalance.NAME,typeof(RoundRobinLoadBalance));
		}

		public static ILoadBalance GetLoadBalance(String name){
			if(LoadBalance.ContainsKey(name)){
				return Activator.CreateInstance( LoadBalance[name]) as ILoadBalance;
			}
			throw new KeyNotFoundException("loadbalance"+name +" == null");
		}
	}
}
