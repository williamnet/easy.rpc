using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.StatCollect
{
    class StatCollectModel
    {
        public string ServiceName { get; set; }
        public string Ip { get; set; }
        public string ApiUrl { get; set; }
        public string ApiPath { get; set; }
        public long ResponseTime { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
