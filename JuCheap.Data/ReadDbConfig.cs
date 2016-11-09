
namespace JuCheap.Data
{
    /// <summary>
    /// 只读数据库配置
    /// </summary>
    public class ReadDbConfig
    {
        public ReadDbConfig()
        {
            Port = 1433;
            UserId = "sa";
        }
        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
