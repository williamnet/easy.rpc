using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Rpc.directory;
using Easy.Rpc.LoadBalance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Easy.Rpc.Test
{
    [TestClass]
    public class NodeLoadTest
    {
        [TestMethod]
        public void LoadTest()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WechatService.config");

            string content = File.ReadAllText(path);

            var list = JsonConvert.DeserializeObject<IList<Node>>(content);

            Assert.IsTrue(list.Count > 0);
            var list2 = JsonConvert.DeserializeObject<IList<Node>>(content);

            var rd = new RedisDirectory(null, "null", list2);

        
            


        }
    }
}
