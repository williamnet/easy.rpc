using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.Protocol
{
    public interface IProtocol
    {
        T Invoke<T>(object model, IDictionary<String, object> queryMaps, InvokerContext context = null);
        T Invoke<T>(IDictionary<String, object> queryMaps,InvokerContext context = null);
        T Invoke<T>(object model,  InvokerContext context = null);
        T Invoke<T>(InvokerContext context = null);
    }
}
