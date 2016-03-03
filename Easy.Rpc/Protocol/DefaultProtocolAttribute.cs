using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.Protocol
{
    /// <summary>
    /// 默认协议
    /// </summary>
    public class DefaultProtocolAttribute: ProtocolAttribute
    {
        public DefaultProtocolAttribute()
        {
            this.ContextType = typeof(InvokerContext);
        }
        public override Type ContextType
        {
            get; set;
        }
    }
}
