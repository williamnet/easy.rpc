
using System;

namespace Easy.Rpc
{
	public class DirectoryContext
	{
		private readonly String path;
		private readonly String directory;
		
		public DirectoryContext(string path, string directory)
		{
			this.path = path;
			this.directory = directory;
		}
		
		public String Path {
			get {
				return path;
			}
		}
		public String Directory {
			get {
				return directory;
			}
		}
	}
}
