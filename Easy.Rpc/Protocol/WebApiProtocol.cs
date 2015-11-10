using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.Protocol
{
    /// <summary>
    /// ASP.net WEBAPI协议
    /// </summary>
    public class WebApiProtocol : IProtocol
    {
        public T Invoke<T>(object model, IDictionary<string, object> queryMaps, InvokerContext context = null)
        {
            WebApiInvokerContext webApiContext = context as WebApiInvokerContext;

            var invoker = new WebApiInvoker<T>(model, queryMaps, webApiContext.Method, webApiContext.ContentType);
            return ClientInvoker.Invoke<T>(invoker, webApiContext);
        }

        public T Invoke<T>(IDictionary<string, object> queryMaps, InvokerContext context = null)
        {
            WebApiInvokerContext webApiContext = context as WebApiInvokerContext;

            var invoker = new WebApiInvoker<T>(null, queryMaps, webApiContext.Method, webApiContext.ContentType);
            return ClientInvoker.Invoke<T>(invoker, webApiContext);
        }
        

        public T Invoke<T>(object model, InvokerContext context = null)
        {
            WebApiInvokerContext webApiContext = context as WebApiInvokerContext;

            var invoker = new WebApiInvoker<T>(model, null, webApiContext.Method, webApiContext.ContentType);
            return ClientInvoker.Invoke<T>(invoker, webApiContext);
        }

        public T Invoke<T>(InvokerContext context = null)
        {
            WebApiInvokerContext webApiContext = context as WebApiInvokerContext;

            var invoker = new WebApiInvoker<T>(null, null, webApiContext.Method, webApiContext.ContentType);
            return ClientInvoker.Invoke<T>(invoker, webApiContext);
        }
    }
}
