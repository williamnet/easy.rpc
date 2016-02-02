using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.StatCollect;

namespace Easy.Rpc
{
    /// <summary>
    /// 带监控的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseInvoker<T> : IInvoker<T>
    {
        public T DoInvoke(Node node, string path)
        {
            string url = this.JoinURL(node, path);
            var statModel = BeginCollect(node.ProviderName, node.Ip, url, path, DateTime.Now);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            T result = ActualDoInvoke(node, path);

            stopwatch.Stop();
            this.EndCollect(statModel, stopwatch.ElapsedMilliseconds);

            return result;
        }

        private StatCollectModel BeginCollect(string serviceName,string ip,string apiUrl,string apiPath,DateTime requestTime)
        {
            var model = new StatCollectModel()
            {
                ServiceName = serviceName,
                Ip = ip,
                ApiUrl = apiUrl,
                ApiPath = apiPath,
                RequestTime = requestTime
            };
            return model;
        }
        private void EndCollect(StatCollectModel model,long responseTime)
        {
            model.ResponseTime = responseTime;
        } 

        protected abstract T ActualDoInvoke(Node node, string path);
        public abstract string JoinURL(Node node, string path);
    }
}
