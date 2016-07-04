using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using Td.Kylin.EnumLibrary;

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
        /// Redis是否为长连接
        /// </summary>
        private readonly bool _keepAlive;

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

        /// <summary>
        /// 缓存数据为null时是否初始化
        /// </summary>
        private readonly bool _initIfNull;

        #region RequestDelegate
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="next"></param>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="redisOptions">Redis Connections</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="initIfNull">缓存数据为空时是否初始化</param>
        public DataCacheMiddleware(RequestDelegate next, bool keepAlive, ConfigurationOptions redisOptions, SqlProviderType sqlType, string sqlConnection,  CacheItemType[] cacheItems = null, bool initIfNull = false)
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
            _keepAlive = keepAlive;
            _initIfNull = initIfNull;
        }

        public Task Invoke(HttpContext context)
        {
            CacheStartup.SqlType = _sqlProviderType;

            CacheStartup.SqlConnctionString = _sqlconnectionString;

            CacheStartup.InitIfNull = _initIfNull;

            CacheStartup.InitRedisConfigration(_options, _keepAlive, _cacheItems);

            return _next(context);
        }

        #endregion

        #region 非Web程序中使用

        /// <summary>
        /// 实例化（非Web程序中使用）
        /// </summary>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="redisOptions"></param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="initIfNull">缓存数据为空时是否初始化</param>
        public DataCacheMiddleware(bool keepAlive, ConfigurationOptions redisOptions, SqlProviderType sqlType, string sqlConnection,  CacheItemType[] cacheItems = null, bool initIfNull = false)
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
            _keepAlive = keepAlive;
            _initIfNull = initIfNull;
        }

        public void Invoke()
        {
            CacheStartup.SqlType = _sqlProviderType;

            CacheStartup.SqlConnctionString = _sqlconnectionString;

            CacheStartup.InitIfNull = _initIfNull;

            CacheStartup.InitRedisConfigration(_options, _keepAlive, _cacheItems);
        }

        #endregion
    }
}
