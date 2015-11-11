using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Easy.Rpc.Test.Protocol
{
    public class TestController : ApiController
    {
        [HttpPost]
        public string GetString()
        {
            return "Ok";
        }
        [HttpPost]
        public dynamic GetData(TestData testData,string a)
        {
            testData.Name = testData.Name + a;
            return testData;
        }

    }
}
