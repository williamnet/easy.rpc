
using System;

namespace Easy.Rpc
{
	[AttributeUsage(AttributeTargets.Class)]
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
