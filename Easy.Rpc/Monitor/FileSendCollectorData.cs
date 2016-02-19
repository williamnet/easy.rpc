using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Easy.Rpc.Monitor
{
    public class FileSendCollectorData : ISendCollectorData
    {
        public void Send(IList<CollectorData> collectorDataList)
        {
            if(collectorDataList == null || collectorDataList.Count == 0)
            {
                return;
            }
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,  collectorDataList[0].StatTime + ".txt");
                File.WriteAllText(path, JsonConvert.SerializeObject(collectorDataList), Encoding.UTF8);
            }
            catch { }
        }
    }
}
