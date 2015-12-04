
using System;
using System.IO;
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
		readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
		readonly String file;
		readonly String name;
		readonly IList<Node> nodes = new List<Node>();
		
		public StaticDirectory(string file, string name)
		{
			this.file = file;
			this.name = name;
			this.InitNodes();
		}
		
		void InitNodes()
		{
			nodes.Clear();
			var document = new XPathDocument(file);
			XPathNavigator navigator = document.CreateNavigator();
			XPathNodeIterator it = navigator.Select("Rpc/Providers/Provider");
			
			foreach (XPathNavigator navi in it) {
                string nodeName = navi.GetAttribute("Name", "");
				Boolean available = Boolean.Parse(navi.GetAttribute("Available", ""));
				Int32 weight = Int32.Parse(navi.GetAttribute("Weight", ""));
				String url = navi.Value;
				
				var node = new Node(nodeName, url, weight, available);
				
				nodes.Add(node);
			}
		}
		
		/// <summary>
		/// 服务名称
		/// </summary>
		/// <returns></returns>
		public String Name()
		{
			return this.name;
		}
		/// <summary>
		/// 获得调用节点
		/// </summary>
		/// <returns>返回可用的调用节点</returns>
		public IList<Node> GetNodes()
		{
			cacheLock.EnterReadLock();
			try {
				return nodes.Where(m => m.IsAvailable).ToList();
				
			} finally {
				cacheLock.ExitReadLock();
			}
		}
		/// <summary>
		/// 刷新节点信息
		/// </summary>
		public void Refresh()
		{
			cacheLock.EnterWriteLock();
			try {
				this.InitNodes();
			} finally {
				cacheLock.ExitWriteLock();
			}
		}
	}
}
