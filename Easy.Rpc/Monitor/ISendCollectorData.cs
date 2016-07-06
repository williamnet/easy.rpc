using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.Monitor
{
    public interface ISendCollectorData
    {
        void Send(IList<CollectorData> collectorDataList);
    }
}
