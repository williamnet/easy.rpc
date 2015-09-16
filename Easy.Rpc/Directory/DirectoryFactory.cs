using System;
using System.Collections.Generic;
using System.IO;
namespace Easy.Rpc.directory
{
	public class DirectoryFactory
	{
		static readonly IDictionary<String,IDirectory> Directory = new Dictionary<String,IDirectory>();
		DirectoryFactory()
		{
		}
		
		static DirectoryFactory()
		{
			Directory.Add(StaticDirectory.NAME, new StaticDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rpc.config")));
		}
		
		public static IDirectory GetDirectory(string name)
		{
			if (Directory.ContainsKey(name)) {
				return Directory[name];
			}
			throw new DirectoryNotFoundException("directory " + name + "is not found");
		}
		
	}
}
