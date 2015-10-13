using System;

namespace Easy.Rpc
{
	[AttributeUsage(AttributeTargets.Class)]
	public class DirectoryAttribute:Attribute
	{
		public DirectoryAttribute(string directory, string path)
		{
			this.Directory = directory;
			this.Path = path;
		}
		/// <summary>
		/// 调用路径
		/// </summary>
		public string Path {
			get;
			private set;
		}
		/// <summary>
		/// 目录名称
		/// </summary>
		public string Directory {
			get;
			private set;
		}
	}
}
