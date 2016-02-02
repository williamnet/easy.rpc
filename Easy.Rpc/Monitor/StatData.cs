using System;
using System.Threading;

namespace Easy.Rpc.Monitor
{
    public class StatData
    {
        private Int64 frequency;
        private Int64 maxResponseTime;
        private Int64 minResponseTime;
        private Int64 totalResponseTime;
        /// <summary>
        /// 请求次数
        /// </summary>
        public Int64 Frequency
        {
            get
            {
                return Interlocked.Read(ref frequency);
            }
        }
        public void AddFrequency()
        {
            Interlocked.Increment(ref frequency);
        }
        /// <summary>
        /// 更新响应时间
        /// </summary>
        /// <param name="responseTime"></param>
        public void UpdateResponseTime(Int64 responseTime)
        {
            this.MaxResponseTime = responseTime;
            this.MinResponseTime = responseTime;
            this.TotalResponseTime = responseTime;
        }
        /// <summary>
        /// 平均响应时间
        /// </summary>
        public double AverageResponseTime
        {
            get
            {
                return this.totalResponseTime / this.frequency;
            }
        }
        /// <summary>
        /// 总响应时间
        /// </summary>
        public Int64 TotalResponseTime
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
        public Int64 MaxResponseTime
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
        public Int64 MinResponseTime
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
