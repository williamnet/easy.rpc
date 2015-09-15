using System;
using System.Collections.Generic;
using Easy.Rpc.LoadBalance;
namespace Easy.Rpc.Cluster
{
	public abstract class Invoker<T>
	{
		protected Invoker(object model, IDictionary<string, object> attachment)
		{
			this.Model = model;
			this.Attachment = attachment;
		}
		
		public Object Model {
			get;
			private set;
		}
		public IDictionary<String,Object> Attachment {
			get;
			private set;
		}
		
		public abstract T DoInvoke(Node node);
	}
}
