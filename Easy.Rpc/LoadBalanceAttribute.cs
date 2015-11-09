
using System;

namespace Easy.Rpc
{
	[AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
	public class LoadBalanceAttribute:Attribute
	{
		public LoadBalanceAttribute(String name)
		{
			this.Name=name;
		}
		
		public String Name {
			get;
			private set;
		}
	}
}
