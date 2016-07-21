using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.EnumLibrary;

namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 数据缓存(Redis)注入配置类
    /// </summary>
    public class CacheInjectionConfig
    {

        private ConfigurationOptions _options;

        /// <summary>
        /// Redis-ConfigurationOptions
        /// </summary>
        public ConfigurationOptions Options
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
        /// 需要缓存的数据类型集合
        /// </summary>
        public CacheItemType[] CacheItems { get; set; }

        /// <summary>
        /// 缓存对象为null时是否实始化
        /// </summary>
        public bool InitIfNull { get; set; }

        /// <summary>
        /// 缓存数据来源数据库类型
        /// </summary>
        public SqlProviderType SqlType { get; set; }

        /// <summary>
        /// 缓存数据来源数据库连接字符串
        /// </summary>
        public string SqlConnectionString { get; set; }
    }
}
