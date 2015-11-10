using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc
{
    /// <summary>
    /// RPC服务 AOP，需要拦截的方法需要是virtual的
    /// </summary>
    public class RPCServiceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var methodInfo = invocation.Method;
            Object[] attributes = methodInfo.GetCustomAttributes(true);

            var directoryAttri = attributes.SingleOrDefault(a => a is DirectoryAttribute) as DirectoryAttribute;

            var clusterAttri = attributes.SingleOrDefault(a => a is ClusterAttribute) as ClusterAttribute;

            var loadBalanceAttri = attributes.SingleOrDefault(a => a is LoadBalanceAttribute) as LoadBalanceAttribute;

            var protocolAttri = attributes.SingleOrDefault(a => a is ProtocolAttribute) as ProtocolAttribute;

            ConstructorInfo constr = protocolAttri.ContextType.GetConstructors()[0];

            ParameterInfo[] parameters = constr.GetParameters();

            object[] constrParamters = new Object[parameters.Length];
            constrParamters[0] = new DirectoryContext(directoryAttri.Path, directoryAttri.Directory);
            constrParamters[1] = new ClusterContext(clusterAttri.Name);
            constrParamters[2] = new LoadBalanceContext(loadBalanceAttri.Name);

            for (var i = 3; i < parameters.Length; i++)
            {
                Object value = PropertyHelper.GetPropertyValue(protocolAttri.GetType(), protocolAttri, parameters[i].Name);

                constrParamters[i] = value;
            }

            var invokeContext = Activator.CreateInstance(protocolAttri.ContextType, constrParamters) as InvokerContext;

            invocation.SetArgumentValue(invocation.Arguments.Length - 1, invokeContext);
            invocation.Proceed();
        }
    }
}
