using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Easy.Rpc.Monitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Easy.Rpc.Test
{
    [TestClass]
    public class MonitorManagerTest
    {
        [TestMethod]
        public void Test()
        {
            MonitorManager.RegisterSend(new FileSendCollectorData());

            var moti = new MonitorData();
            moti.ApiPath = "/test";
            moti.ApiUrl = "http://192.167.1.1:3000/test/api";
            moti.Ip = "192.167.1.1:3000";
            moti.ServiceName = "TestSerivce";

            var moti1 = new MonitorData();
            moti1.ApiPath = "/test";
            moti1.ApiUrl = "http://192.167.1.1:3000/test/api1";
            moti1.Ip = "192.167.1.1:3000";
            moti1.ServiceName = "TestSerivce";

            var moti2 = new MonitorData();
            moti2.ApiPath = "/test";
            moti2.ApiUrl = "http://192.167.1.1:3000/test/api2";
            moti2.Ip = "192.167.1.1:3000";
            moti2.ServiceName = "TestSerivce";

            var moti3 = new MonitorData();
            moti3.ApiPath = "/test";
            moti3.ApiUrl = "http://192.167.1.1:3000/test/api3";
            moti3.Ip = "192.167.1.1:3000";
            moti3.ServiceName = "TestSerivce";

            var motiList = new MonitorData[4] { moti, moti1, moti2, moti3 };

            DateTime delaydatetime = DateTime.Now.AddMinutes(3);

            while (DateTime.Now <= delaydatetime)
            {
                Thread.Sleep(new Random(Guid.NewGuid().GetHashCode()).Next(100, 1000));

                for (var i = 0; i < 50; i++)
                {
                    Task.Factory.StartNew(() =>
                    {
                        int m = new Random(Guid.NewGuid().GetHashCode()).Next(0, 4);

                        var requesttime = DateTime.Now;
                        int responseTime = new Random(Guid.NewGuid().GetHashCode()).Next(1, 1000);
                        MonitorManager.ResponseStat(requesttime, motiList[m], responseTime, true);
                    });
                }
            }

            Thread.Sleep(60 * 4 * 1000);
        }
    }
}
