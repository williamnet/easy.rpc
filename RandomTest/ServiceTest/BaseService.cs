
using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using Easy.Domain.ServiceFramework;
using Easy.Rpc;
using Easy.Rpc.Cluster;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.Protocol;

namespace RandomTest.ServiceTest
{
    /// <summary>
    /// Description of BaseService.
    /// </summary>
    public class BaseService : WebApiProtocol, IBaseService, IService
    {
        [WebApiProtocol]
        [Directory("rpc", "/select")]
        [Cluster(FailoverCluster.NAME)]
        [LoadBalance(RoundRobinLoadBalance.NAME)]
        public virtual String Select(string a, string b, InvokerContext context)
        {
            var queryMap = new Dictionary<String, Object>();
            queryMap.Add("b", b);
            return this.Invoke<String>(a, queryMap, context);
        }
    }
}
