using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Easy.Public.HttpRequestService;
using Easy.Public.MyLog;
using Easy.Rpc.LoadBalance;
using Newtonsoft.Json;

namespace Easy.Rpc.directory
{
    public class RedisDirectoryBuilder
    {
        public const string PathAddRelation = "/Address/AddRelation";
        public const string PathPull = "/Address/Pull";
        public const string PathRegister = "/Address/Register";
        public readonly string RegisterUrl = "";
        public readonly string RedisUrl = "";
        public RedisDirectoryBuilder(string registerUrl, string redisUrl)
        {
            RegisterUrl = registerUrl;
            RedisUrl = redisUrl;
        }
        public void Build(MySelfInfo myself, string[] usedService = null, string[] apiList = null)
        {
            this.RegisterMyself(myself, apiList);
            this.RegisterRelation(myself.Directory, usedService);
            this.Pull(usedService);
        }
        private void RegisterMyself(MySelfInfo myself, string[] apilist = null)
        {
            string url = string.Concat(this.RegisterUrl, PathRegister);

            StringBuilder data = new StringBuilder();
            data.AppendFormat("directory={0}&", myself.Directory);
            data.AppendFormat("url={0}&", myself.Url);
            data.AppendFormat("ip={0}&", myself.Ip);
            data.AppendFormat("weight={0}&", myself.Weight);
            data.AppendFormat("status={0}&", myself.Status);
            data.AppendFormat("description={0}&", myself.Description);
            if (apilist != null && apilist.Length > 0)
            {
                data.AppendFormat("apiList={0}", string.Join(",", apilist));
            }
            try
            {
                var request = HttpRequestClient.Request(url, "POST", false);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Send(data).GetBodyContent(true);
            }
            catch(System.Exception e)
            {
                LogManager.Error("注册失败", e.Message);
            }
           
        }
        private void RegisterRelation(string directoryName, string[] providerDirectory)
        {
            string url = string.Concat(this.RegisterUrl, PathAddRelation);

            string providerDirectoryParams = string.Empty;
            if(providerDirectory != null && providerDirectory.Length != 0)
            {
                providerDirectoryParams = string.Join(",", providerDirectory);
            }

            StringBuilder data = new StringBuilder();
            data.AppendFormat("consumerDirectoryName={0}&", directoryName);
            data.AppendFormat("providerDirectoryNames={0}", providerDirectoryParams);

            try
            {
                var request = HttpRequestClient.Request(url, "POST", false);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Send(data).GetBodyContent(true);
            }
            catch(System.Exception e)
            {
                LogManager.Error("注册失败关系", e.Message);
            }
        }
        private void Pull(string[] providerDirectory)
        {
            if(providerDirectory == null || providerDirectory.Length == 0)
            {
                return;
            }
            var redis = new RedisServer(RedisUrl);
            string url = string.Concat(RegisterUrl, PathPull);

            foreach (var p in providerDirectory)
            {
                Task.Factory.StartNew(() =>
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("providerDirectory=" + p);

                    string result = string.Empty;
                    try
                    {
                        var request = HttpRequestClient.Request(url, "POST", false);
                        request.ContentType = "application/x-www-form-urlencoded";
                        result = request.Send(sb).GetBodyContent(true);
                    }
                    catch (System.Exception e)
                    {
                        LogManager.Info("拉取节点失败", e.Message);
                    }
                    IList<Node> nodes = new List<Node>(0);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        nodes = JsonConvert.DeserializeObject<IList<Node>>(result);
                    }
                    var redisDirectory = new RedisDirectory(redis, p, nodes);
                    DirectoryFactory.Register(p, redisDirectory);
                });
            }
        }
    }
}
