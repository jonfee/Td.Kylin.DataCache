using Microsoft.AspNetCore.Builder;
using StackExchange.Redis;
using System;
using Td.Kylin.EnumLibrary;

namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 缓存注入
    /// </summary>
    public static class DataCacheInjection
    {
        /// <summary>
        /// 以ConfigurationOptions形式注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="config"><seealso cref="CacheInjectionConfig"/>缓存注入配置类实例</param>
        /// <returns></returns>
        public static IApplicationBuilder UseDataCache(this IApplicationBuilder builder, CacheInjectionConfig config)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            return builder.Use(next => new DataCacheMiddleware(next, config.Options, config.KeepAlive, config.SqlType, config.SqlConnectionString, config.CacheItems, config.InitIfNull, config.Level2CacheSeconds).Invoke);
        }

        /// <summary>
        /// 以ConfigurationOptions形式注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options">Redis Connections</param>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="initIfNull">缓存数据为空时是否初始化</param>
        /// <param name="level2CacheSeconds">二级缓存的时间（单位：秒）</param>
        /// <returns></returns>
        public static IApplicationBuilder UseDataCache(this IApplicationBuilder builder, ConfigurationOptions options, bool keepAlive, SqlProviderType sqlType, string sqlConnection, CacheItemType[] cacheItems = null, bool initIfNull = false, int level2CacheSeconds = 0)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.Use(next => new DataCacheMiddleware(next, options, keepAlive, sqlType, sqlConnection, cacheItems: cacheItems, initIfNull: initIfNull, level2CacheSeconds: level2CacheSeconds).Invoke);
        }

        /// <summary>
        /// 以连接字符串形式注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="redisConnectionString">Redis的连接字符串</param>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="initIfNull">缓存数据为空时是否初始化</param>
        /// <param name="level2CacheSeconds">二级缓存的时间（单位：秒）</param>
        /// <returns></returns>
        public static IApplicationBuilder UseDataCache(this IApplicationBuilder builder, string redisConnectionString, bool keepAlive, SqlProviderType sqlType, string sqlConnection, CacheItemType[] cacheItems = null, bool initIfNull = false, int level2CacheSeconds = 0)
        {
            var options = ConfigurationOptions.Parse(redisConnectionString);

            return builder.UseDataCache(options, keepAlive, sqlType, sqlConnection, cacheItems, initIfNull, level2CacheSeconds);
        }

        /// <summary>
        /// 以CacheInjectionConfig形式注入
        /// </summary>
        /// <param name="config"><seealso cref="CacheInjectionConfig"/>类实例</param>
        /// <returns></returns>
        public static void UseDataCache(CacheInjectionConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            new DataCacheMiddleware(config.Options, config.KeepAlive, config.SqlType, config.SqlConnectionString, config.CacheItems, config.InitIfNull, config.Level2CacheSeconds).Invoke();
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
            new DataCacheMiddleware(options, keepAlive, sqlType, sqlConnection, cacheItems, initIfNull, level2CacheSeconds).Invoke();
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
    }
}
