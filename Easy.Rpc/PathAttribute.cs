using System;

namespace Easy.Rpc
{
	[AttributeUsage(AttributeTargets.Class)]
	public class PathAttribute:Attribute
	{
		public PathAttribute(string directory,string provider ,string path)
		{
			this.Directory = directory;
			this.Provider = provider;
			this.Path = path;
		}
		public string Provider {
			get;
			private set;
		}
		public string Path {
			get;
			private set;
		}
		public string Directory{
			get;
			private set;
		}
	}
}
