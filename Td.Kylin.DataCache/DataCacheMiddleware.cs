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
        /// 是否保持长连接
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

        /// <summary>
        /// 二级缓存的时间（单位：秒）
        /// </summary>
        private readonly int _level2CacheSeconds;

        #region RequestDelegate
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="next"></param>
        /// <param name="redisOptions">Redis Connections</param>
        /// <param name="keepAlive">是否保持长连接</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="initIfNull">缓存数据为空时是否初始化</param>
        /// <param name="level2CacheSeconds">二级缓存的时间（单位：秒）</param>
        public DataCacheMiddleware(RequestDelegate next, ConfigurationOptions redisOptions, bool keepAlive, SqlProviderType sqlType, string sqlConnection, CacheItemType[] cacheItems = null, bool initIfNull = false, int level2CacheSeconds = 30)
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
            _keepAlive = keepAlive;
            _next = next;
            _cacheItems = cacheItems;
            _initIfNull = initIfNull;
            _level2CacheSeconds = level2CacheSeconds;
        }

        public Task Invoke(HttpContext context)
        {
            Startup.Start(_options, _keepAlive, _initIfNull, _sqlProviderType, _sqlconnectionString, _cacheItems, _level2CacheSeconds);

            return _next(context);
        }

        #endregion

        #region 非Web程序中使用

        /// <summary>
        /// 实例化（非Web程序中使用）
        /// </summary>
        /// <param name="redisOptions"></param>
        /// <param name="keepAlive">是否保持长连接</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <param name="cacheItems">缓存类型</param>
        /// <param name="initIfNull">缓存数据为空时是否初始化</param>
        /// <param name="level2CacheSeconds">二级缓存的时间（单位：秒）</param>
        public DataCacheMiddleware(ConfigurationOptions redisOptions, bool keepAlive, SqlProviderType sqlType, string sqlConnection, CacheItemType[] cacheItems = null, bool initIfNull = false, int level2CacheSeconds = 30)
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
            _keepAlive = keepAlive;
            _cacheItems = cacheItems;
            _initIfNull = initIfNull;
            _level2CacheSeconds = level2CacheSeconds;
        }

        public void Invoke()
        {
            Startup.Start(_options, _keepAlive, _initIfNull, _sqlProviderType, _sqlconnectionString, _cacheItems, _level2CacheSeconds);
        }

        #endregion
    }
}
