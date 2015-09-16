
using System;

namespace Easy.Rpc
{
	
	[AttributeUsage(AttributeTargets.Class)]
	public class ClusterAttribute:Attribute
	{
		public ClusterAttribute(string name)
		{
			this.Name = name;
		}
		
		public String Name {
			get;
			private set;
		}
		
		public Int32? Retries {
			get;
			set;
		}
		public Int32? Forks {
			get;
			set;
		}
		public Int32? Timeout {
			get;
			set;
		}
	}
}
