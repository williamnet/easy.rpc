
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Xml.XPath;
using Easy.Rpc.LoadBalance;

namespace Easy.Rpc.directory
{
	/// <summary>
	/// 地址调用目录服务
	/// </summary>
	public class StaticDirectory:IDirectory
	{
		public const String NAME = "StaticDirectory";
		
		readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
		String file;
		readonly IList<Node> nodes = new List<Node>();
		
		public StaticDirectory(string file)
		{
			this.file = file;
			this.InitNodes();
		}
		
		void InitNodes()
		{
			nodes.Clear();
			
			var document = new XPathDocument(file);
			XPathNavigator navigator = document.CreateNavigator();
			XPathNodeIterator it = navigator.Select("Rpc/Providers/Provider");
			
			foreach (XPathNavigator navi in it) {
				String name = navi.GetAttribute("Name", "");
				Boolean available = Boolean.Parse(navi.GetAttribute("Available", ""));
				Int32 weight = Int32.Parse(navi.GetAttribute("Weight", ""));
				String url = navi.Value;
				
				var node = new Node(name, url, weight, available);
				
				nodes.Add(node);
			}
		}
		
		
		#region IDirectory implementation
		public string Name()
		{
			return NAME;
		}
		/// <summary>
		/// 获得调用节点
		/// </summary>
		/// <param name="providerName"></param>
		/// <returns></returns>
		public IList<Node> GetNodes(string providerName)
		{
			cacheLock.EnterReadLock();
			try {
				return nodes.Where(m => m.ProviderName == providerName && m.IsAvailable).ToList();
				
			} finally {
				cacheLock.ExitReadLock();
			}
		}
		/// <summary>
		/// 刷新节点信息
		/// </summary>
		/// <param name="file"></param>
		public void Refresh(string file = null)
		{
			cacheLock.EnterWriteLock();
			try {
				this.file = file ?? this.file;
				this.InitNodes();
			} finally {
				cacheLock.ExitWriteLock();
			}
		}
		#endregion
		
	}
}
