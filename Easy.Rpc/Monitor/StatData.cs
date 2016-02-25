using System;
using System.Threading;

namespace Easy.Rpc.Monitor
{
    public class StatData
    {
        private long responseFrequency;
        private long maxResponseTime;
        private long minResponseTime;
        private long totalResponseTime;
        private long requestFrequency;
        private long errorResponseFrequency;

        /// <summary>
        /// 获得错误次数
        /// </summary>
        public long ErrorResponseFrquency
        {
            get
            {
                return Interlocked.Read(ref errorResponseFrequency);
            }
        }
        /// <summary>
        /// 请求次数
        /// </summary>
        public long RequestFrequency
        {
            get
            {
                return Interlocked.Read(ref requestFrequency);
            }
        }
        /// <summary>
        /// 请求次数
        /// </summary>
        public long ResponseFrequency
        {
            get
            {
                return Interlocked.Read(ref responseFrequency);
            }
        }

        public void AddErrorResponseFrequency()
        {
            Interlocked.Increment(ref errorResponseFrequency);
        }

        /// <summary>
        /// 增加请求次数
        /// </summary>
        public void AddRequestFrequency()
        {
            Interlocked.Increment(ref requestFrequency);
        }
        /// <summary>
        /// 增加响应次数
        /// </summary>
        public void AddResponseFrequency()
        {
            Interlocked.Increment(ref responseFrequency);
        }
        /// <summary>
        /// 更新响应时间
        /// </summary>
        /// <param name="responseTime"></param>
        public void UpdateResponseTime(long responseTime)
        {
            MaxResponseTime = responseTime;
            MinResponseTime = responseTime;
            TotalResponseTime = responseTime;
        }
        /// <summary>
        /// 平均响应时间
        /// </summary>
        public double AverageResponseTime
        {
            get
            {
                return totalResponseTime / responseFrequency;
            }
        }
        /// <summary>
        /// 请求平均响应时间
        /// </summary>
        public double AverageRequestResponseTime
        {
            get
            {
                return totalResponseTime / requestFrequency;
            }
        }
        /// <summary>
        /// 总响应时间
        /// </summary>
        public long TotalResponseTime
        {
            get
            {
                return Interlocked.Read(ref totalResponseTime);
            }
            set
            {
                Interlocked.Add(ref totalResponseTime, value);
            }
        }
        /// <summary>
        /// 最大响应时间
        /// </summary>
        public long MaxResponseTime
        {
            get
            {
                return Interlocked.Read(ref maxResponseTime);
            }
            set
            {
                Int64 initValue = 0;
                do
                {
                    initValue = maxResponseTime;
                    if (value <= initValue)
                    {
                        value = maxResponseTime;
                    }
                } while (initValue != Interlocked.CompareExchange(ref maxResponseTime, value, initValue));
            }
        }
        /// <summary>
        /// 最小响应时间
        /// </summary>
        public long MinResponseTime
        {
            get
            {
                return Interlocked.Read(ref minResponseTime);
            }
            set
            {
                Int64 initValue = 0;
                do
                {
                    initValue = minResponseTime;
                    if (initValue != 0 && value >= initValue)
                    {
                        value = minResponseTime;
                    }

                } while (initValue != Interlocked.CompareExchange(ref minResponseTime, value, initValue));
            }
        }
    }
}
