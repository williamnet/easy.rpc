
using System.Collections.Generic;
using Easy.Rpc.Monitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Easy.Rpc.Test
{
    [TestClass]
    public class MonitorDataTest
    {
        [TestMethod]
        public void DicForMonitorDataTest()
        {
            var m = new MonitorData()
            {
                ServiceName = "OrderDatabase",
                Ip = "database",
                BaseApiUrl = "database",
                ApiPath = "/FindBy",
                ApiUrl = "2"
            };
            var m2 = new MonitorData()
            {
                ServiceName = "OrderDatabase",
                Ip = "database",
                BaseApiUrl = "database",
                ApiPath = "/FindBy",
                ApiUrl = "1"
            };

            var dic = new Dictionary<MonitorData, string>();

            dic.Add(m, "a");
            dic.Add(m2, "b");


        }
    }
}
