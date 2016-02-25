using System;
using System.Diagnostics;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.Monitor;


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
            MonitorManager.RequestStat(DateTime.Now, this.BeginCollect(node.ProviderName, node.Ip, node.Url, url, path));
            Stopwatch sw = this.GetAndStart();
            bool hasError = false;
            try
            {
                T result = ActualDoInvoke(node, path);
                return result;
            }
            catch (System.Exception e)
            {
                hasError = true;
                throw e;
            }
            finally
            {
                MonitorManager.ResponseStat(DateTime.Now, this.BeginCollect(node.ProviderName, node.Ip, node.Url, url, path), this.GetResultAndStop(sw), hasError);
            }
        }

        private Stopwatch GetAndStart()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            return stopwatch;
        }

        public long GetResultAndStop(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private MonitorData BeginCollect(string serviceName, string ip,string baseApiUrl, string apiUrl, string apiPath)
        {
            var monitorData = new MonitorData()
            {
                ServiceName = serviceName,
                Ip = ip,
                ApiUrl = apiUrl,
                ApiPath = apiPath,
                BaseApiUrl = baseApiUrl
            };
            return monitorData;
        }
        protected abstract T ActualDoInvoke(Node node, string path);
        public abstract string JoinURL(Node node, string path);
    }
}
