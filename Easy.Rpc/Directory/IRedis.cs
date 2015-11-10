using Easy.Rpc.LoadBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc.directory
{
    public interface IRedis
    {
        /// <summary>
        /// 根据redis的key查询Nodes列表
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        IList<Node> GetNodes(string serviceName);
        /// <summary>
        /// Reids Nodes变化通知订阅
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        void Subscribe(Action<IList<Node>> action, string serviceName);
    }
}
