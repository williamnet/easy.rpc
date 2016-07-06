

namespace Easy.Rpc.directory
{
    public class MySelfInfo
    {
        /// <summary>
        /// 自己的目录名称
        /// </summary>
        public string Directory
        {
            get; set;
        }
        /// <summary>
        /// api地址
        /// </summary>
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip
        {
            get;
            set;
        }
        /// <summary>
        /// 权重
        /// </summary>
        public int Weight
        {
            get;
            set;
        }
        /// <summary>
        /// 状态 在线 = 1,下线 = 2
        /// </summary>
        public int Status
        {
            get;
            set;
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
