
using System;

namespace Easy.Rpc
{
	public class ClusterContext
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">集群名称</param>
		/// <param name="retries">重试次数</param>
		/// <param name="forks">并行调用数量</param>
		/// <param name="timeout">超时间</param>
		public ClusterContext(string name, int? retries = null, int? forks = null, int? timeout = null)
		{
			this.Name = name;
			this.Retries = retries;
			this.Forks = forks;
			this.Timeout = timeout;
			
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
