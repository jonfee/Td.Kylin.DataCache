using StackExchange.Redis;

namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 缓存Redis上下文对象
    /// </summary>
    public static class CacheRedisContext
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
                //长连接
                if (Startup.KeepAlive)
                {
                    if (_redis == null)
                    {
                        lock (_locker)
                        {
                            if (_redis == null)
                            {
                                _redis = ConnectionMultiplexer.Connect(Startup.RedisOptions);
                            }
                        }
                    }

                    return _redis;
                }                
                else//非长连接
                {
                    return ConnectionMultiplexer.Connect(Startup.RedisOptions);
                }
            }
        }
    }
}
