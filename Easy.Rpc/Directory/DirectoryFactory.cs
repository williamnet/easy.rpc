using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Easy.Rpc.directory
{
	public class DirectoryFactory
	{
		static readonly IDictionary<String,IDirectory> Directory = new Dictionary<String,IDirectory>();
		DirectoryFactory()
		{
		}
		/// <summary>
		/// 清理目录
		/// </summary>
		public static void Clear()
		{
			Directory.Clear();
		}
		/// <summary>
		/// 注册新目录
		/// </summary>
		/// <param name="name">服务名称</param>
		/// <param name="directory">目录对象</param>
		public static void Register(string name, IDirectory directory)
		{
			if (!Directory.ContainsKey(name)) {
				Directory.Add(name, directory);
			}
		}
		/// <summary>
		/// 获得全部目录
		/// </summary>
		/// <returns></returns>
		public static IDirectory[] GetDirectories(){
			return Directory.Values.ToArray();
		}
		/// <summary>
		/// 根据名称获得目录
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static IDirectory GetDirectory(string name)
		{
			if (Directory.ContainsKey(name)) {
				return Directory[name];
			}
			throw new DirectoryNotFoundException("directory " + name + "is not found");
		}
		
	}
}
