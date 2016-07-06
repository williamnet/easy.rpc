using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Easy.Rpc.Test
{
    [TestClass]
    public class ThreadTest
    {
        private static readonly Thread t;
        [TestMethod]
        public  void Test()
        {
            int stop = (60-DateTime.Now.Second) * 1000 + 2000;

            Thread.Sleep(stop);
            Assert.IsTrue(DateTime.Now.Second - 2 <= 1);

            Thread.Sleep(5000);

            stop = (60 - DateTime.Now.Second) * 1000 + 2000;
            Thread.Sleep(stop);
            Assert.IsTrue(DateTime.Now.Second - 2 <= 1);

        }
    }
}
