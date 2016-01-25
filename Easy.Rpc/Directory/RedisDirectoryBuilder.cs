using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Public.HttpRequestService;
using Easy.Rpc.directory;
using Easy.Rpc.LoadBalance;
using Newtonsoft.Json;

namespace Easy.Rpc.Directory
{
    public class RedisDirectoryBuilder
    {
        public const string PathAddRelation = "/Address/AddRelation";
        public const string PathPull = "/Address/Pull";
        public const string PathRegister = "/Address/Register";
        public readonly string RegisterUrl = "";
        public readonly string RedisUrl = "";
        public readonly int DatabaseIndex = 0;
        public RedisDirectoryBuilder(string registerUrl, string redisUrl, int databaseIndex)
        {
            RegisterUrl = registerUrl;
            RedisUrl = redisUrl;
            DatabaseIndex = databaseIndex;
        }
        public void Build(MySelfInfo myself, string[] usedService)
        {
            this.RegisterMyself(myself);
            this.RegisterRelation(myself.Directory, usedService);
            this.Pull(usedService);
        }

        private void RegisterMyself(MySelfInfo myself)
        {
            string url = string.Concat(this.RegisterUrl, PathRegister);

            StringBuilder data = new StringBuilder();
            data.AppendFormat("directory={0}&", myself.Directory);
            data.AppendFormat("url={0}&", myself.Url);
            data.AppendFormat("ip={0}&", myself.Ip);
            data.AppendFormat("weight={0}&", myself.Weight);
            data.AppendFormat("status={0}&", myself.Status);
            data.AppendFormat("description={0}", myself.Description);

            var request = HttpRequestClient.Request(url, "POST", false);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Send(data).GetBodyContent(true);
        }

        private void RegisterRelation(string directoryName, string[] providerDirectory)
        {
            string url = string.Concat(this.RegisterUrl, PathAddRelation);

            StringBuilder data = new StringBuilder();
            data.AppendFormat("consumerDirectoryName={0}&", directoryName);
            data.AppendFormat("providerDirectory={1}", string.Join(",", providerDirectory));

            var request = HttpRequestClient.Request(url, "POST", false);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Send(data).GetBodyContent(true);
        }

        private void Pull(string[] providerDirectory)
        {
            var redis = new RedisServer(RedisUrl, DatabaseIndex);
            string url = string.Concat(RegisterUrl, PathPull);

            IList<Task> tasklist = new List<Task>();
            foreach (var p in providerDirectory)
            {
                Task t = new Task(() =>
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("providerDirectory=" + p);

                    var request = HttpRequestClient.Request(url, "POST", false);
                    request.ContentType = "application/x-www-form-urlencoded";
                    string result = request.Send(sb).GetBodyContent(true);

                    IList<Node> nodes = JsonConvert.DeserializeObject<IList<Node>>(result);
                    var redisDirectory = new RedisDirectory(redis, p, nodes);
                    DirectoryFactory.Register(p, redisDirectory);

                });
                tasklist.Add(t);
            }
            Task.WhenAll(tasklist);
        }
    }
}
