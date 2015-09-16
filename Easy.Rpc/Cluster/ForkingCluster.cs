
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Easy.Rpc.LoadBalance;
namespace Easy.Rpc.Cluster
{
	/// <summary>
	/// 并行调用多个服务器，只要一个成功即返回。通常用于实时性要求较高的读操作，但需要浪费更多服务资源。可通过forks="2"来设置最大并行数。
	/// </summary>
	public class ForkingCluster:ICluster
	{
		public const string NAME ="ForkingCluster";
		
		public String Name()
		{
			return NAME;
		}
		
		public ForkingCluster(int forks, int timeout = 1000)
		{
			this.Forks = forks;
			this.Timeout = timeout;
		}
		public ForkingCluster()
		{
			this.Forks = 2;
			this.Timeout = 10000;
		}
		public int Timeout {
			get;
			set;
		}
		
		public int Forks {
			get;
			set;
		}
		
		public T Invoke<T>(IList<Node> nodes, string path, ILoadBalance loadbanlance, IInvoker<T> invoker)
		{
			IList<Node> selected;
			if (this.Forks <= 0 || this.Forks >= nodes.Count) {
				selected = nodes;
			} else {
				selected = new List<Node>();
				
				for (var i = 0; i < this.Forks; i++) {
					Node node = loadbanlance.Select(nodes, path);
					
					if (!selected.Contains(node)) {
						selected.Add(node);
					}
				}
			}
			var tasks = new List<Task>();
			foreach (Node node in selected) {
				
				var task = Task.Factory.StartNew<T>(() => invoker.DoInvoke(node,path));
				
				tasks.Add(task);
			}
			
			int taskId = Task.WaitAny(tasks.ToArray(), new TimeSpan(0, 0, 0, 0, this.Timeout));
			
			if (taskId == -1) {
				throw new TimeoutException();
			}
			var completeTask = tasks[taskId] as Task<T>;
			return completeTask.Result;
		}
	}
}












