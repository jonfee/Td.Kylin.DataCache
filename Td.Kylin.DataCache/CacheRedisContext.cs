using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 缓存Redis上下文对象
    /// </summary>
    public class CacheRedisContext
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        public static object _locker = new object();

        private static ConnectionMultiplexer _redis;

        /// <summary>
        /// Redis（ConnectionMultiplexer）对象，单例
        /// </summary>
        public static ConnectionMultiplexer Redis
        {
            get
            {
                if (_redis == null)
                {
                    lock (_redis)
                    {
                        if (_redis == null)
                        {
                            _redis = ConnectionMultiplexer.Connect(CacheStartup.RedisOptions);
                        }
                    }
                }

                return _redis;
            }
        }
    }
}
