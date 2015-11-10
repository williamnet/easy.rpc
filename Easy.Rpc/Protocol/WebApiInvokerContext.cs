using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.Protocol
{
    public class WebApiInvokerContext : InvokerContext
    {
        public WebApiInvokerContext(DirectoryContext directoryContext, ClusterContext clusterContext, LoadBalanceContext loadBalanceContext, string method = "POST", string contentType = "application/json")
            : base(directoryContext, clusterContext, loadBalanceContext)
        {
            this.Method = method;
            this.ContentType = contentType;
        }
        
        public String Method
        {
            get;
            private set;
        }
        public String ContentType
        {
            get;
            private set;
        }
    }
}
