using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Easy.Rpc.Monitor
{
    public static class MonitorManager
    {
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<MonitorData, StatData>> dataContainer = 
            new ConcurrentDictionary<string, ConcurrentDictionary<MonitorData, StatData>>();

        private static readonly Thread t;
        private static readonly IList<ISendCollectorData> senderList = new List<ISendCollectorData>();

        static MonitorManager()
        {
            t = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    int milliSeconds = (60 - DateTime.Now.Second) * 1000 + 2000;
                    Thread.Sleep(milliSeconds);

                    IList<CollectorData> collectDataList = DataCollect(DateTime.Now.AddMinutes(-1));

                    foreach (var item in senderList)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            item.Send(collectDataList);
                        });
                    }
                }
            }));

            t.Start();
        }

        public static void RegisterSend(ISendCollectorData sender)
        {
            senderList.Add(sender);
        }
        private static IList<CollectorData> DataCollect(DateTime time)
        {
            string requestTimeMinStr = time.ToString("yyyyMMddHHmm");

            ConcurrentDictionary<MonitorData, StatData> monitorData;

            if(dataContainer.TryRemove(requestTimeMinStr, out monitorData))
            {
                var collectDataList = new List<CollectorData>();
                foreach (var item in monitorData)
                {
                    var collectData = new CollectorData()
                    {
                        BaseApiUrl = item.Key.BaseApiUrl,
                        ApiPath = item.Key.ApiPath,
                        ApiUrl = item.Key.ApiUrl,
                        Ip = item.Key.Ip,
                        ServiceName = item.Key.ServiceName,
                        AverageResponseTime = item.Value.AverageResponseTime,
                        ResponseFrequency = item.Value.ResponseFrequency,
                        MaxResponseTime = item.Value.MaxResponseTime,
                        MinResponseTime = item.Value.MinResponseTime,
                        TotalResponseTime = item.Value.TotalResponseTime,
                        StatTime = requestTimeMinStr,
                        ErrorResponseFrquency = item.Value.ErrorResponseFrquency,
                        RequestFrequency = item.Value.RequestFrequency,
                        AverageRequestResponseTime = item.Value.AverageRequestResponseTime
                    };

                    collectDataList.Add(collectData);
                }
                return collectDataList;
            }
            return new CollectorData[0];
        }
        /// <summary>
        /// 响应统计
        /// </summary>
        /// <param name="requestTime"></param>
        /// <param name="data"></param>
        /// <param name="responseTime"></param>
        /// <param name="responseIsSuccess">请求是否成功</param>
        public static void ResponseStat(DateTime requestTime, MonitorData data, Int64 responseTime,bool responseIsSuccess)
        {
            if(t.ThreadState != ThreadState.Aborted && t.ThreadState != ThreadState.Stopped)
            {
                Task.Factory.StartNew(() =>
                {
                    ActualResponseStat(requestTime, data, responseTime, responseIsSuccess);
                });
            }
        }
        /// <summary>
        /// 请求统计
        /// </summary>
        /// <param name="requestTime"></param>
        /// <param name="data"></param>
        public static void RequestStat(DateTime requestTime, MonitorData data)
        {
            if (t.ThreadState != ThreadState.Aborted && t.ThreadState != ThreadState.Stopped)
            {
                Task.Factory.StartNew(() =>
                {
                    ActualRequestStat(requestTime, data);
                });
            }
        }
        /// <summary>
        /// 请求统计
        /// </summary>
        /// <param name="requestTime"></param>
        /// <param name="data"></param>
        private static void ActualRequestStat(DateTime requestTime,MonitorData data)
        {
            string requestTimeMinStr = requestTime.ToString("yyyyMMddHHmm");
            ConcurrentDictionary<MonitorData, StatData> monitorData;
            if (dataContainer.TryGetValue(requestTimeMinStr, out monitorData))
            {
                StatData statData;
                if (monitorData.TryGetValue(data, out statData))
                {
                    statData.AddRequestFrequency();
                }
                else
                {
                    statData = new StatData();
                    statData.AddRequestFrequency();
                    monitorData.TryAdd(data, statData);
                }
            }
            else
            {
                monitorData = new ConcurrentDictionary<MonitorData, StatData>();
                var statData = new StatData();
                statData.AddRequestFrequency();
                monitorData.TryAdd(data, statData);
                dataContainer.TryAdd(requestTimeMinStr, monitorData);
            }
        }
        /// <summary>
        /// 响应统计
        /// </summary>
        /// <param name="requestTime"></param>
        /// <param name="data"></param>
        /// <param name="responseTime"></param>
        /// <param name="hasError"></param>
        private static void ActualResponseStat(DateTime requestTime, MonitorData data, Int64 responseTime,bool hasError)
        {
            string requestTimeMinStr = requestTime.ToString("yyyyMMddHHmm");

            ConcurrentDictionary<MonitorData, StatData> monitorData;
            if (dataContainer.TryGetValue(requestTimeMinStr, out monitorData))
            {
                StatData statData;
                if (monitorData.TryGetValue(data, out statData))
                {
                    statData.AddResponseFrequency();
                    statData.UpdateResponseTime(responseTime);
                    if (hasError)
                    {
                        statData.AddErrorResponseFrequency();
                    }
                }
                else
                {
                    statData = new StatData();
                    statData.AddResponseFrequency();
                    statData.UpdateResponseTime(responseTime);
                    if (hasError)
                    {
                        statData.AddErrorResponseFrequency();
                    }

                    monitorData.TryAdd(data, statData);
                }
            }
            else
            {
                monitorData = new ConcurrentDictionary<MonitorData, StatData>();

                var statData = new StatData();
                statData.AddResponseFrequency();
                statData.UpdateResponseTime(responseTime);
                if (hasError)
                {
                    statData.AddErrorResponseFrequency();
                }
                monitorData.TryAdd(data, statData);
                dataContainer.TryAdd(requestTimeMinStr, monitorData);
            }
        }
    }
}
