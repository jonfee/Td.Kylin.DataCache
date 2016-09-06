using StackExchange.Redis;
using Td.Kylin.EnumLibrary;

namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 数据缓存服务参数
    /// </summary>
    public class DataCacheServerOptions
    {
        private ConfigurationOptions _options;

        /// <summary>
        /// Redis-ConfigurationOptions
        /// </summary>
        public ConfigurationOptions RedisOptions
        {
            get
            {
                if (_options == null && !string.IsNullOrWhiteSpace(RedisConnectionString))
                {
                    _options = ConfigurationOptions.Parse(RedisConnectionString);
                }

                return _options;
            }
            set
            {
                _options = value;
            }
        }

        /// <summary>
        /// Redis-连接字符串
        /// </summary>
        public string RedisConnectionString { get; set; }

        /// <summary>
        /// 是否保持长连接
        /// </summary>
        public bool KeepAlive { get; set; } = true;

        /// <summary>
        /// 数据库类型
        /// <remarks>如果不需求对缓存进行维护时，不需要此参数</remarks>
        /// </summary>
        public SqlProviderType SqlType { get; set; } = SqlProviderType.SqlServer;

        /// <summary>
        /// 数据库连接字符串
        /// <remarks>如果不需求对缓存进行维护时，不需要此参数</remarks>
        /// </summary>
        public string SqlConnection { get; set; }

        /// <summary>
        /// 缓存项类型
        /// </summary>
        public CacheItemType[] CacheItems { get; set; } = null;

        /// <summary>
        /// 缓存值为null时是否初始化
        /// </summary>
        public bool InitIfNull { get; set; } = false;

        /// <summary>
        /// 二级缓存缓存时间（单位：秒）
        /// </summary>
        public int Level2CacheSeconds { get; set; } = 10;
    }
}
