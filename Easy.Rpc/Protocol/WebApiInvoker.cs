
using System;
using System.Collections.Generic;
using System.Net;
using Easy.Public.HttpRequestService;
using Easy.Rpc.LoadBalance;

namespace Easy.Rpc.Protocol
{
    /// <summary>
    /// Description of WebApiInvoker.
    /// </summary>
    public class WebApiInvoker<T> : IInvoker<T>
    {
        String method = "";
        String contentType = "";
        object @bodyObject;
        IDictionary<String, Object> queryMap = new Dictionary<String, Object>();


        public WebApiInvoker(object bodyObject, IDictionary<string, object> queryMap, string method = "post", string contentType = "application/json")
        {

            this.bodyObject = bodyObject;
            this.queryMap = queryMap;
            this.method = method;
            this.contentType = contentType;
        }


        public virtual string JoinURL(Node node, string path)
        {
            string url = node.Url + path;

            String queryString = string.Empty;
            if (this.queryMap != null)
            {
                foreach (KeyValuePair<String, Object> element in this.queryMap)
                {
                    queryString += string.Format("{0}={1}&", element.Key, element.Value);
                }
                queryString = queryString.TrimEnd('&');

                if (url.Contains("?"))
                {
                    url = string.Concat(url, "&", queryString);
                }
                else
                {
                    url = string.Concat(url, "?", queryString);
                }
            }

            return url;
        }

        public virtual T DoInvoke(Node node, string path)
        {
            string url = this.JoinURL(node, path);

            HttpWebRequest request = HttpRequestClient.Request(url, this.method, this.contentType);
            HttpWebResponse response = null;
            if (this.bodyObject == null)
            {
                response = request.Send();
            }
            else
            {
                response = request.Send(this.bodyObject);
            }
            return response.GetBodyContent<T>();
        }
    }
}
