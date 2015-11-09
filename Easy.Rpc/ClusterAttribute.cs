
using System;

namespace Easy.Rpc
{
	
	[AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
	public class ClusterAttribute:Attribute
	{
		public ClusterAttribute(string name)
		{
			this.Name = name;
		}
		/// <summary>
		/// Cluster的名称
		/// </summary>
		public String Name {
			get;
			private set;
		}
		/// <summary>
		/// 调用失败重试次数
		/// </summary>
		public Int32? Retries {
			get;
			set;
		}
		/// <summary>
		/// 并行调用的服务器数量
		/// </summary>
		public Int32? Forks {
			get;
			set;
		}
		/// <summary>
		/// 并行调用服务器超时的时间
		/// </summary>
		public Int32? Timeout {
			get;
			set;
		}
	}
}
