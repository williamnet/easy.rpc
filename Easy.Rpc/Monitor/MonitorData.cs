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
        public string ServiceName { get; set; }
        public string Ip { get; set; }
        public string BaseApiUrl { get; set; }
        public string ApiUrl { get; set; }
        public string ApiPath { get; set; }

        public override int GetHashCode()
        {
            return this.ApiUrl.ToUpper().GetHashCode();
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
            if (this.ApiUrl.ToUpper() == d.ApiUrl.ToUpper())
            {
                return true;
            }
            return false;
        }
    }
}
