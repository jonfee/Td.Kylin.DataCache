﻿using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using Td.Kylin.DataCache.RedisConfig;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    internal sealed class DataCacheMiddleware
    {
        /// <summary>
        /// Http Request
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The options relevant to a set of redis connections
        /// </summary>
        private readonly ConfigurationOptions _options;

        /// <summary>
        /// 数据库类型
        /// </summary>
        private readonly SqlProviderType _sqlProviderType;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private readonly string _sqlconnectionString;

        /// <summary>
        /// 需要注入的缓存类型
        /// </summary>
        private readonly CacheItemType[] _cacheItems;

        #region RequestDelegate
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="next"></param>
        /// <param name="redisOptions">Redis Connections</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        public DataCacheMiddleware(RequestDelegate next, ConfigurationOptions redisOptions, SqlProviderType sqlType, string sqlConnection, params CacheItemType[] cacheItems)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (redisOptions == null)
            {
                throw new ArgumentNullException(nameof(redisOptions));
            }
            if (string.IsNullOrWhiteSpace(sqlConnection))
            {
                throw new ArgumentNullException(nameof(sqlConnection));
            }
            _sqlProviderType = sqlType;
            _sqlconnectionString = sqlConnection;
            _options = redisOptions;
            _next = next;
            _cacheItems = cacheItems;
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="next"></param>
        /// <param name="redisConnection">Redis 连接字符串</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        public DataCacheMiddleware(RequestDelegate next, string redisConnection, SqlProviderType sqlType, string sqlConnection, params CacheItemType[] cacheItems)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            var tempOptions = ConfigurationOptions.Parse(redisConnection);

            if (tempOptions == null)
            {
                throw new ArgumentNullException(nameof(redisConnection));
            }
            if (string.IsNullOrWhiteSpace(sqlConnection))
            {
                throw new ArgumentNullException(nameof(sqlConnection));
            }
            _sqlProviderType = sqlType;
            _sqlconnectionString = sqlConnection;
            _options = tempOptions;
            _next = next;
            _cacheItems = cacheItems;
        }

        public Task Invoke(HttpContext context)
        {
            RedisInjection.UseRedis(_options);

            CacheStartup.SqlType = _sqlProviderType;

            CacheStartup.SqlConnctionString = _sqlconnectionString;

            CacheStartup.InitRedisConfigration(_cacheItems);
            
            CacheCollection.Reset();

            return _next(context);
        }

        #endregion

        #region 非Web程序中使用

        /// <summary>
        /// 实例化（非Web程序中使用）
        /// </summary>
        /// <param name="redisOptions"></param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        public DataCacheMiddleware(ConfigurationOptions redisOptions, SqlProviderType sqlType, string sqlConnection, params CacheItemType[] cacheItems)
        {
            if (redisOptions == null)
            {
                throw new ArgumentNullException(nameof(redisOptions));
            }
            if (string.IsNullOrWhiteSpace(sqlConnection))
            {
                throw new ArgumentNullException(nameof(sqlConnection));
            }
            _sqlProviderType = sqlType;
            _sqlconnectionString = sqlConnection;
            _options = redisOptions;
            _cacheItems = cacheItems;
        }

        /// <summary>
        /// 实例化（非Web程序中使用）
        /// </summary>
        /// <param name="next"></param>
        /// <param name="redisConnection">Redis 连接字符串</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        public DataCacheMiddleware(string redisConnection, SqlProviderType sqlType, string sqlConnection, params CacheItemType[] cacheItems)
        {
            var tempOptions = ConfigurationOptions.Parse(redisConnection);

            if (tempOptions == null)
            {
                throw new ArgumentNullException(nameof(redisConnection));
            }
            if (string.IsNullOrWhiteSpace(sqlConnection))
            {
                throw new ArgumentNullException(nameof(sqlConnection));
            }
            _sqlProviderType = sqlType;
            _sqlconnectionString = sqlConnection;
            _options = tempOptions;
            _cacheItems = cacheItems;
        }

        public void Invoke()
        {
            RedisInjection.UseRedis(_options);

            CacheStartup.SqlType = _sqlProviderType;

            CacheStartup.SqlConnctionString = _sqlconnectionString;

            CacheStartup.InitRedisConfigration(_cacheItems);

            CacheCollection.Reset();
        }

        #endregion
    }
}
