using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.Monitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Easy.Rpc.Test
{
    [TestClass]
    public class BaseInvokerTest
    {
        public BaseInvokerTest()
        {
            MonitorManager.RegisterSend(new FileSendCollectorData());
        }
        [TestMethod]
        public void Test()
        {
            var node1 = new Node("testService", "http://127.0.0.1:3000", 100, true, "127.0.0.1:3000");
            var node2 = new Node("testService", "http://127.0.0.1:6000", 100, true, "127.0.0.1:6000");

            var nodes = new Node[2] { node1, node2 };

            var testInvoker = new TestInvoker((b, node, path) =>
            {

                var r = new Random(Guid.NewGuid().GetHashCode());

                Thread.Sleep(r.Next(100, 1000));

                return string.Empty;
            });

            for (var i = 0; i < 10000; i++)
            {
                Task.Factory.StartNew(() => {
                    var r = new Random(Guid.NewGuid().GetHashCode());
                    testInvoker.DoInvoke(nodes[r.Next(0, 2)], "/test");
                });
            }

            Thread.Sleep(60 * 2 * 1000);
        }
    }

    public class TestInvoker : BaseInvoker<string>
    {
        public TestInvoker(Func<BaseInvoker<string>, Node, string, string> func) : base(func)
        {

        }

        public override string JoinURL(Node node, string path)
        {
            return node.Url + path;
        }
    }
}
