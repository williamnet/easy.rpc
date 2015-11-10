using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.Protocol
{
    public class WebApiProtocolAttribute : ProtocolAttribute
    {
        public WebApiProtocolAttribute()
        {
            this.Method = "POST";
            this.ContentType = "application/json";
            this.ContextType = typeof(WebApiInvokerContext);
        }

        /// <summary>
        /// 属性名称与WebApiInvokerContext构造函数method参数致，不区分大小写
        /// </summary>
        public String Method
        {
            get;
            set;
        }
        /// <summary>
        /// 属性名称与WebApiInvokerContext构造函数contentType参数致，不区分大小写
        /// </summary>
        public String ContentType
        {
            get;
            set;
        }
    }
}
