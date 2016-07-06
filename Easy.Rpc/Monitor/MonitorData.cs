using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Rpc.Monitor
{
    public class MonitorData
    {
        public MonitorData()
        {
            ServiceName = string.Empty;
            Ip = string.Empty;
            BaseApiUrl = string.Empty;
            ApiUrl = string.Empty;
            ApiPath = string.Empty;
        }
        public string ServiceName { get; set; }
        public string Ip { get; set; }
        public string BaseApiUrl { get; set; }
        public string ApiUrl { get; set; }
        public string ApiPath { get; set; }

        public override int GetHashCode()
        {
            string hashcode = this.ServiceName.ToUpper() + this.BaseApiUrl.ToUpper() + this.ApiPath.ToUpper();
            return hashcode.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }

            MonitorData d = (MonitorData)obj;
            if (
                this.ApiUrl.ToUpper() == d.ApiUrl.ToUpper()
                && this.ServiceName.ToUpper() == d.ServiceName.ToUpper()
                && this.Ip.ToUpper() == d.Ip.ToUpper()
                && this.BaseApiUrl.ToUpper() == d.BaseApiUrl.ToUpper()
                && this.ApiPath.ToUpper() == this.ApiPath.ToUpper()
                )
            {
                return true;
            }
            return false;
        }
    }
}
