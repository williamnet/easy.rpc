using System;
using System.Collections.Generic;

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
