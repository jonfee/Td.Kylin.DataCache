using Td.Kylin.DataCache.RedisConfig;

namespace Td.Kylin.DataCache
{
    internal sealed class CacheStartup
    {
        static CacheStartup()
        {
            RedisConfiguration = InitRedisConfigration();
        }
        
        /// <summary>
        /// Redis缓存配置
        /// </summary>
        public static RedisConfigurationRoot RedisConfiguration { get;private set; }

        /// <summary>
        /// 数据库提供者类型
        /// </summary>
        public static SqlProviderType SqlType { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string SqlConnctionString { get; set; }

        #region 初始化Redis配置

        /// <summary>
        /// 创建并初始化Redis缓存配置
        /// </summary>
        /// <returns></returns>
        private static RedisConfigurationRoot InitRedisConfigration()
        {
            var config = new RedisConfigurationRoot();

            //全国区域
            config.Add(CacheItemType.SystemArea, 0, RedisSaveType.HashSet);
            //开通区域
            config.Add(CacheItemType.OpenArea, 0, RedisSaveType.HashSet);

            return config;
        }

        #endregion
    }
}
