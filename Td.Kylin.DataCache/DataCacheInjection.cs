using Microsoft.AspNet.Builder;
using StackExchange.Redis;
using System;
using Td.Kylin.EnumLibrary;

namespace Td.Kylin.DataCache
{
    public static class DataCacheInjection
    {
        /// <summary>
        /// 以ConfigurationOptions形式注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="options">Redis Connections</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="InitIfNull">缓存数据为空时是否初始化</param>
        /// <returns></returns>
        public static IApplicationBuilder UseDataCache(this IApplicationBuilder builder, bool keepAlive, ConfigurationOptions options, SqlProviderType sqlType, string sqlConnection, CacheItemType[] cacheItems = null, bool InitIfNull = false)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.Use(next => new DataCacheMiddleware(next, keepAlive, options, sqlType, sqlConnection, cacheItems).Invoke);
        }

        /// <summary>
        /// 以连接字符串形式注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="redisConnectionString">Redis的连接字符串</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="InitIfNull">缓存数据为空时是否初始化</param>
        /// <returns></returns>
        public static IApplicationBuilder UseDataCache(this IApplicationBuilder builder, bool keepAlive, string redisConnectionString, SqlProviderType sqlType, string sqlConnection, CacheItemType[] cacheItems = null, bool InitIfNull = false)
        {
            return builder.Use(next => new DataCacheMiddleware(next, keepAlive, redisConnectionString, sqlType, sqlConnection, cacheItems).Invoke);
        }

        /// <summary>
        /// 以ConfigurationOptions形式注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="options">Redis Connections</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="InitIfNull">缓存数据为空时是否初始化</param>
        /// <returns></returns>
        public static void UseDataCache(bool keepAlive, ConfigurationOptions options, SqlProviderType sqlType, string sqlConnection, CacheItemType[] cacheItems = null, bool InitIfNull = false)
        {
            new DataCacheMiddleware(keepAlive, options, sqlType, sqlConnection, cacheItems).Invoke();
        }

        /// <summary>
        /// 以连接字符串形式注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="redisConnectionString">Redis的连接字符串</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="InitIfNull">缓存数据为空时是否初始化</param>
        /// <returns></returns>
        public static void UseDataCache(bool keepAlive, string redisConnectionString, SqlProviderType sqlType, string sqlConnection, CacheItemType[] cacheItems = null, bool InitIfNull = false)
        {
            new DataCacheMiddleware(keepAlive, redisConnectionString, sqlType, sqlConnection, cacheItems).Invoke();
        }
    }
}
