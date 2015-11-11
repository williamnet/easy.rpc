using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.Test.Protocol
{
    public interface ITestService
    {
        String GetStringResult(Easy.Rpc.InvokerContext context = null);
        TestData GetData(TestData testData,string a, Easy.Rpc.InvokerContext context = null);
    }
}
