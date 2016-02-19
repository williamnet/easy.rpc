using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Public.HttpRequestService;
using Newtonsoft.Json;

namespace Easy.Rpc.Monitor
{
    public class HttpSendCollectorData : ISendCollectorData
    {

        public HttpSendCollectorData(string postUrl)
        {
            this.PostUrl = postUrl;
        }

        public string PostUrl
        {
            get; private set;
        }

        public void Send(IList<CollectorData> collectorDataList)
        {
            if (collectorDataList == null || collectorDataList.Count == 0)
            {
                return;
            }
            try
            {
                var request = HttpRequestClient.Request(this.PostUrl, "POST");
                request.ContentType = "application/x-www-form-urlencoded";
                request.Send(new StringBuilder("data=" + JsonConvert.SerializeObject(collectorDataList))).GetBodyContent(true);
            }
            catch { }

        }
    }
}
