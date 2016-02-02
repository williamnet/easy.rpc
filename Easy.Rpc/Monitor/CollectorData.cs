using System;

namespace Easy.Rpc.Monitor
{
    public class CollectorData
    {
        public string ServiceName;
        public string Ip;
        public string ApiUrl;
        public string ApiPath;

        public Int64 Frequency;
        public Int64 MaxResponseTime;
        public Int64 MinResponseTime;
        public Int64 TotalResponseTime;
        public double AverageResponseTime;
        /// <summary>
        /// 统计时间
        /// </summary>
        public string StatTime;
    }
}
