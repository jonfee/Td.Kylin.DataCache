using StackExchange.Redis;
using System;

namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 数据缓存服务
    /// </summary>
    public class DataCacheServer : IDataCacheServer
    {
        /// <summary>
        /// 数据缓存服务实例
        /// </summary>
        /// <param name="options"><see cref="StackExchange.Redis.ConfigurationOptions"/></param>
        /// <param name="keepAlive"></param>
        /// <param name="cacheItems"></param>
        /// <param name="level2CacheSeconds"></param>
        public DataCacheServer(ConfigurationOptions options, bool keepAlive, CacheItemType[] cacheItems, int level2CacheSeconds) : this(new DataCacheServerOptions
        {
            CacheItems = cacheItems,
            InitIfNull = false,
            KeepAlive = keepAlive,
            Level2CacheSeconds = level2CacheSeconds,
            RedisOptions = options
        })
        { }

        /// <summary>
        /// 数据缓存服务实例
        /// </summary>
        /// <param name="redisOptions"><see cref="StackExchange.Redis.ConfigurationOptions"/>关联的配置字符串</param>
        /// <param name="keepAlive"></param>
        /// <param name="cacheItems"></param>
        /// <param name="level2CacheSeconds"></param>
        public DataCacheServer(string redisOptions, bool keepAlive, CacheItemType[] cacheItems, int level2CacheSeconds) : this(new DataCacheServerOptions
        {
            CacheItems = cacheItems,
            InitIfNull = false,
            KeepAlive = keepAlive,
            Level2CacheSeconds = level2CacheSeconds,
            RedisConnectionString = redisOptions,
        })
        { }

        /// <summary>
        /// 数据缓存服务实例
        /// </summary>
        /// <param name="options"></param>
        public DataCacheServer(DataCacheServerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.Level2CacheSeconds < 0) options.Level2CacheSeconds = 0;

            _options = options;
        }

        /// <summary>
        /// 数据缓存服务参数
        /// </summary>
        private readonly DataCacheServerOptions _options;

        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            ValidateOptions();

            try
            {
                Startup.Start(_options);

                CacheCollection.Reset();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 验证参数
        /// </summary>
        private void ValidateOptions()
        {
            if (_options.RedisOptions == null)
            {
                throw new InvalidOperationException("ConfigurationOptions (Options.RedisOptions) value is null.");
            }
        }
    }
}
