using System.Threading;
using System.Threading.Tasks;
using Easy.Rpc.Monitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Easy.Rpc.Test
{
    [TestClass]
    public class StatDataTest
    {
        [TestMethod]
        public void FrequencyTest()
        {
            StatData data = new StatData();

            for(var i = 0; i < 10000; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    data.AddResponseFrequency();
                });
            }

            Thread.Sleep(5000);

            Assert.AreEqual(10000, data.ResponseFrequency);
        }
        [TestMethod]
        public void MaxResponseTimeTest()
        {
            StatData data = new StatData();
            for (var i = 0; i < 10000; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    data.MaxResponseTime = i;
                });
            }

            Thread.Sleep(5000);

            Assert.AreEqual(10000, data.MaxResponseTime);
        }
    }
}
