using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public abstract class ProtocolAttribute : Attribute
    {
        public virtual Type ContextType
        {
            get;
            set;
        }
    }
}
