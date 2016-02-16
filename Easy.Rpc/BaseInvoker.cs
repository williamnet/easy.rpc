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
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var requestTime = DateTime.Now;
            var hasError = false;
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
                if (!hasError)
                {
                    stopwatch.Stop();
                    MonitorManager.Write(requestTime, this.BeginCollect(node.ProviderName, node.Ip, node.Url,url, path), stopwatch.ElapsedMilliseconds);
                }
            }
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
