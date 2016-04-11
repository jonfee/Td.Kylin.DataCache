using Microsoft.AspNet.Builder;
using StackExchange.Redis;
using System;

namespace Td.Kylin.DataCache
{
    public static class DataCacheInjection
    {
        /// <summary>
        /// 以ConfigurationOptions形式注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options">Redis Connections</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <returns></returns>
        public static IApplicationBuilder UseDataCache(this IApplicationBuilder builder, ConfigurationOptions options, SqlProviderType sqlType, string sqlConnection)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.Use(next => new DataCacheMiddleware(next, options, sqlType, sqlConnection).Invoke);
        }

        /// <summary>
        /// 以连接字符串形式注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="redisConnectionString">Redis的连接字符串</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <returns></returns>
        public static IApplicationBuilder UseDataCache(this IApplicationBuilder builder, string redisConnectionString, SqlProviderType sqlType, string sqlConnection)
        {
            return builder.Use(next => new DataCacheMiddleware(next, redisConnectionString, sqlType, sqlConnection).Invoke);
        }

        /// <summary>
        /// 以ConfigurationOptions形式注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options">Redis Connections</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <returns></returns>
        public static void UseDataCache(ConfigurationOptions options, SqlProviderType sqlType, string sqlConnection)
        {
            new DataCacheMiddleware(options, sqlType, sqlConnection).Invoke();
        }

        /// <summary>
        /// 以连接字符串形式注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="redisConnectionString">Redis的连接字符串</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <returns></returns>
        public static void UseDataCache(string redisConnectionString, SqlProviderType sqlType, string sqlConnection)
        {
            new DataCacheMiddleware(redisConnectionString, sqlType, sqlConnection).Invoke();
        }
    }
}
