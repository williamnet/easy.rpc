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
                        Ip=item.Key.Ip,
                        ServiceName= item.Key.ServiceName,
                        AverageResponseTime = item.Value.AverageResponseTime,
                        Frequency = item.Value.Frequency,
                        MaxResponseTime = item.Value.MaxResponseTime,
                        MinResponseTime= item.Value.MinResponseTime,
                        TotalResponseTime = item.Value.TotalResponseTime,
                        StatTime = requestTimeMinStr
                    };

                    collectDataList.Add(collectData);
                }
                return collectDataList;
            }
            return new CollectorData[0];
        }

        public static void Write(DateTime requestTime, MonitorData data, Int64 responseTime)
        {
            if(t.ThreadState != ThreadState.Aborted && t.ThreadState != ThreadState.Stopped)
            {
                Task.Factory.StartNew(() =>
                {
                    ActualWrite(requestTime, data, responseTime);
                });
            }
        }

        private static void ActualWrite(DateTime requestTime, MonitorData data, Int64 responseTime)
        {
            string requestTimeMinStr = requestTime.ToString("yyyy-MM-dd HH:mm");

            ConcurrentDictionary<MonitorData, StatData> monitorData;
            if (dataContainer.TryGetValue(requestTimeMinStr, out monitorData))
            {
                StatData statData;
                if (monitorData.TryGetValue(data, out statData))
                {
                    statData.AddFrequency();
                    statData.UpdateResponseTime(responseTime);
                }
                else
                {
                    statData = new StatData();
                    statData.AddFrequency();
                    statData.UpdateResponseTime(responseTime);

                    monitorData.TryAdd(data, statData);
                }
            }
            else
            {
                monitorData = new ConcurrentDictionary<MonitorData, StatData>();

                var statData = new StatData();
                statData.AddFrequency();
                statData.UpdateResponseTime(responseTime);
                monitorData.TryAdd(data, statData);

                dataContainer.TryAdd(requestTimeMinStr, monitorData);
            }
        }
    }
}
