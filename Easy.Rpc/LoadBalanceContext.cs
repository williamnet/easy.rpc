
using System;

namespace Easy.Rpc
{
	public class LoadBalanceContext
	{
		public LoadBalanceContext(String name)
		{
			this.Name=name;
		}
		
		public String Name {
			get;
			private set;
		}
	}
}
