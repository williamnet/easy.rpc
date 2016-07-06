using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Rpc.LoadBalance;
using Newtonsoft.Json;

namespace Easy.Rpc.directory
{
    class NodeCacheHelper
    {
        private static readonly string CachedFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache_nodes");

        static NodeCacheHelper()
        {

            if (!System.IO.Directory.Exists(CachedFolder))
            {
                System.IO.Directory.CreateDirectory(CachedFolder);
            }
        }

        public void Save(IList<Node> nodes,string serviceName)
        {
            if(nodes == null || nodes.Count == 0)
            {
                return;
            }

            string path = Path.Combine(CachedFolder, serviceName + ".config");
            string json = JsonConvert.SerializeObject(nodes);
            System.IO.File.WriteAllText(path, json, Encoding.UTF8);
        }

        public IList<Node> LoadLocal(string serviceName)
        {
            string path = Path.Combine(CachedFolder, serviceName + ".config");
            if (!System.IO.File.Exists(path))
            {
                return new List<Node>();
            }
            string content = System.IO.File.ReadAllText(path, Encoding.UTF8);
            if (string.IsNullOrWhiteSpace(content))
            {
                return new List<Node>();
            }
            return JsonConvert.DeserializeObject<IList<Node>>(content);
        }
    }
}
