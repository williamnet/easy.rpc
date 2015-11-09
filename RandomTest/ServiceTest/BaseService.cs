
using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using Easy.Domain.ServiceFramework;
using Easy.Rpc;
using Easy.Rpc.Cluster;
using Easy.Rpc.LoadBalance;

namespace RandomTest.ServiceTest
{
	/// <summary>
	/// Description of BaseService.
	/// </summary>
	public class BaseService:IBaseService,IService
	{
		#region IBaseService implementation
        [Directory("rpc", "/select")]
        [Cluster(FailoverCluster.NAME)]
        [LoadBalance(RoundRobinLoadBalance.NAME)]
		public virtual String Select(string a, string b, InvokerContext context)
		{
			var queryMap = new Dictionary<String,Object>();
			queryMap.Add("b", b);
			return this.Invoke<String>(a, queryMap, context);
		}
		
		public T Invoke<T>(object model, IDictionary<String,object> queryMaps, InvokerContext context)
		{
			var webapi = new WebApiInvoker<T>(model, queryMaps);
			return ClientInvoker.Invoke<T>(webapi, context);
		}
		
		#endregion
	}
	
	public class RPCServiceInterceptor:IInterceptor
	{
		#region IInterceptor implementation
		public void Intercept(IInvocation invocation)
		{
			var methodInfo = invocation.Method;
			Object[] attributes = methodInfo.GetCustomAttributes(true);
			
			var directoryAttri = attributes.SingleOrDefault(a => a is DirectoryAttribute) as DirectoryAttribute;
			
			var clusterAttri = attributes.SingleOrDefault(a => a is ClusterAttribute) as ClusterAttribute;
			
			var loadBalanceAttri = attributes.SingleOrDefault(a => a is LoadBalanceAttribute) as LoadBalanceAttribute;
			
			var invokeContext = new InvokerContext(new DirectoryContext(directoryAttri.Path, directoryAttri.Directory), new ClusterContext(clusterAttri.Name), new LoadBalanceContext(loadBalanceAttri.Name));
			
			
			invocation.SetArgumentValue(invocation.Arguments.Length - 1, invokeContext);
			
			invocation.Proceed();
		}
		#endregion
		
	}
}
