using System;

namespace Easy.Rpc
{
	[AttributeUsage(AttributeTargets.Class)]
	public class PathAttribute:Attribute
	{
		public PathAttribute(String path, string provider)
		{
			this.Path = path;
			this.Provider = provider;
		}
		public string Provider {
			get;
			private set;
		}
		public string Path {
			get;
			private set;
		}
	}
}
