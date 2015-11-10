using Easy.Domain.ServiceFramework;
using Easy.Rpc.Cluster;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Easy.Rpc.Test.Protocol
{
    public class TestService : WebApiProtocol, ITestService, IService
    {
        [WebApiProtocol]
        [Directory("testServiceDirectory","")]
        [Cluster(FailoverCluster.NAME)]
        [LoadBalance(RandomLoadBalance.NAME)]
        public virtual string GetStringResult(InvokerContext context = null)
        {
            return this.Invoke<string>(context);
        }
    }
}
