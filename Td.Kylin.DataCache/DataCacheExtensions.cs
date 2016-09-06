using StackExchange.Redis;
using Td.Kylin.EnumLibrary;

namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 缓存注入
    /// </summary>
    public static class DataCacheExtensions
    {
        #region 实例化到项目

        /// <summary>
        /// 以CacheInjectionConfig形式注入
        /// </summary>
        /// <param name="option"><seealso cref="DataCacheServerOptions"/>类实例</param>
        /// <returns></returns>
        public static void UseDataCache(DataCacheServerOptions option)
        {
            IDataCacheServer server = new DataCacheServer(option);
            server.Start();
        }

        /// <summary>
        /// 以ConfigurationOptions形式注入
        /// </summary>
        /// <param name="options">Redis Connections</param>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="initIfNull">缓存数据为空时是否初始化</param>
        /// <param name="level2CacheSeconds">二级缓存的时间（单位：秒）</param>
        /// <returns></returns>
        public static void UseDataCache(ConfigurationOptions options, bool keepAlive, SqlProviderType sqlType, string sqlConnection, CacheItemType[] cacheItems = null, bool initIfNull = false, int level2CacheSeconds = 30)
        {
            DataCacheServerOptions option = new DataCacheServerOptions
            {
                RedisOptions = options,
                CacheItems = cacheItems,
                InitIfNull = initIfNull,
                KeepAlive = keepAlive,
                Level2CacheSeconds = level2CacheSeconds,
                SqlConnection = sqlConnection,
                SqlType = sqlType
            };

            IDataCacheServer server = new DataCacheServer(option);

            server.Start();
        }

        /// <summary>
        /// 以连接字符串形式注入
        /// </summary>
        /// <param name="redisConnectionString">Redis的连接字符串</param>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="initIfNull">缓存数据为空时是否初始化</param>
        /// <param name="level2CacheSeconds">二级缓存的时间（单位：秒）</param>
        /// <returns></returns>
        public static void UseDataCache(string redisConnectionString, bool keepAlive, SqlProviderType sqlType, string sqlConnection, CacheItemType[] cacheItems = null, bool initIfNull = false, int level2CacheSeconds = 30)
        {
            var options = ConfigurationOptions.Parse(redisConnectionString);

            UseDataCache(options, keepAlive, sqlType, sqlConnection, cacheItems, initIfNull, level2CacheSeconds);
        }

        /// <summary>
        /// 以ConfigurationOptions形式注入
        /// <remarks>不需要对缓存进行维护</remarks>
        /// </summary>
        /// <param name="options">Redis Connections</param>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="level2CacheSeconds">二级缓存的时间（单位：秒）</param>
        /// <returns></returns>
        public static void UseDataCache(ConfigurationOptions options, bool keepAlive, CacheItemType[] cacheItems = null, int level2CacheSeconds = 30)
        {
            IDataCacheServer server = new DataCacheServer(options, keepAlive, cacheItems,  level2CacheSeconds);
            server.Start();
        }

        /// <summary>
        /// 以连接字符串形式注入
        /// <remarks>不需要对缓存进行维护</remarks>
        /// </summary>
        /// <param name="redisConnectionString">Redis的连接字符串</param>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="level2CacheSeconds">二级缓存的时间（单位：秒）</param>
        /// <returns></returns>
        public static void UseDataCache(string redisConnectionString, bool keepAlive, CacheItemType[] cacheItems = null, int level2CacheSeconds = 30)
        {
            IDataCacheServer server = new DataCacheServer(redisConnectionString, keepAlive, cacheItems, level2CacheSeconds);
            server.Start();
        }

        #endregion
    }
}
