using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.Test.Protocol
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use((context, task) =>
            {
                return context.Response.WriteAsync("ok");
            });

        }
    }
}
