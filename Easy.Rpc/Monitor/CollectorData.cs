using System;

namespace Easy.Rpc.Monitor
{
    public class CollectorData
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName;
        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip;
        /// <summary>
        /// API地址公共部分
        /// </summary>
        public string BaseApiUrl;
        /// <summary>
        /// API地址
        /// </summary>
        public string ApiUrl;
        /// <summary>
        /// API路径
        /// </summary>
        public string ApiPath;
        /// <summary>
        /// 每分钟并发数
        /// </summary>
        public Int64 ResponseFrequency;
        /// <summary>
        /// 最大响应时间
        /// </summary>
        public Int64 MaxResponseTime;
        /// <summary>
        /// 最小响应时间
        /// </summary>
        public Int64 MinResponseTime;
        /// <summary>
        /// 每分钟总响应时间
        /// </summary>
        public Int64 TotalResponseTime;
        /// <summary>
        /// 每分钟平均响应时间
        /// </summary>
        public double AverageResponseTime;
        /// <summary>
        /// 每分钟请求平均响应时间
        /// </summary>
        public double AverageRequestResponseTime;
        /// <summary>
        /// 统计时间
        /// </summary>
        public string StatTime;
        /// <summary>
        /// 错误请求次数
        /// </summary>
        public long ErrorResponseFrquency;
        /// <summary>
        /// 请求次数
        /// </summary>
        public long RequestFrequency;

        
    }
}
