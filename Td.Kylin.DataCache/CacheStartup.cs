using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.RedisConfig;
using Td.Kylin.EnumLibrary;

namespace Td.Kylin.DataCache
{
    internal sealed class Startup
    {
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="options">缓存Redis配置信息</param>
        /// <param name="keepAlive">是否长连接</param>
        /// <param name="initIfNull">缓存为null时是否初始化</param>
        /// <param name="sqlType">缓存的源数据库类型</param>
        /// <param name="sqlConnection">缓存的源数据库连接</param>
        /// <param name="types">需要缓存的对象类型集合</param>
        /// <param name="level2CacheSeconds">二级缓存的时间（单位：秒）</param>
        public static void Start(ConfigurationOptions options, bool keepAlive, bool initIfNull, SqlProviderType sqlType, string sqlConnection, IEnumerable<CacheItemType> types, int level2CacheSeconds)
        {
            //未设置二级缓存时间时，默认二级缓存时间10秒
            if (level2CacheSeconds <= 0) level2CacheSeconds = 10;

            RedisOptions = options;
            KeepAlive = keepAlive;
            InitIfNull = initIfNull;
            SqlType = sqlType;
            SqlConnctionString = sqlConnection;
            Level2CacheSeconds = level2CacheSeconds;

            //注入缓存对象
            InjectCacheItems(types);
        }

        /// <summary>
        /// 创建并初始化Redis缓存配置
        /// </summary>
        /// <returns></returns>
        public static void InjectCacheItems(IEnumerable<CacheItemType> types)
        {
            var config = new RedisConfigurationRoot();

            List<CacheItemType> cacheItems = null == types ? null : types.ToList();

            bool isAll = cacheItems == null || cacheItems.Count() < 1;

            if (isAll || cacheItems.Contains(CacheItemType.SystemArea))
            {
                //全国区域
                config.Add(CacheItemType.SystemArea, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.OpenArea))
            {
                //开通区域
                config.Add(CacheItemType.OpenArea, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.AreaForum))
            {
                //区域圈子
                config.Add(CacheItemType.AreaForum, 0, RedisSaveType.HashSet, CacheLevel.Hight);
            }
            if (isAll || cacheItems.Contains(CacheItemType.AreaRecommendIndustry))
            {
                //区域行业推荐
                config.Add(CacheItemType.AreaRecommendIndustry, 0, RedisSaveType.HashSet, CacheLevel.Hight);
            }
            if (isAll || cacheItems.Contains(CacheItemType.B2CProductCategory))
            {
                //B2C商品分类
                config.Add(CacheItemType.B2CProductCategory, 0, RedisSaveType.HashSet, CacheLevel.Hight);
            }
            if (isAll || cacheItems.Contains(CacheItemType.B2CProductCategoryTags))
            {
                //B2C商品分类下标签
                config.Add(CacheItemType.B2CProductCategoryTags, 0, RedisSaveType.HashSet, CacheLevel.Hight);
            }
            if (isAll || cacheItems.Contains(CacheItemType.ForumCategory))
            {
                //圈子分类
                config.Add(CacheItemType.ForumCategory, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.ForumCircle))
            {
                //系统圈子
                config.Add(CacheItemType.ForumCircle, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.MerchantIndustry))
            {
                //商家行业
                config.Add(CacheItemType.MerchantIndustry, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.MerchantProductSystemCategory))
            {
                //商家商品系统分类
                config.Add(CacheItemType.MerchantProductSystemCategory, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.SystemGolbalConfig))
            {
                //全局配置
                config.Add(CacheItemType.SystemGolbalConfig, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.UserEmpiricalConfig))
            {
                //用户经验值规则配置
                config.Add(CacheItemType.UserEmpiricalConfig, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.UserLevelConfig))
            {
                //用户等级规则配置
                config.Add(CacheItemType.UserLevelConfig, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.UserPointsConfig))
            {
                //用户积分规则配置
                config.Add(CacheItemType.UserPointsConfig, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.JobCategory))
            {
                //职位类别
                config.Add(CacheItemType.JobCategory, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.PlatformCommission))
            {
                //平台针对区域抽成
                config.Add(CacheItemType.PlatformCommission, 0, RedisSaveType.HashSet, CacheLevel.Hight);
            }
            if (isAll || cacheItems.Contains(CacheItemType.AreaForMerchantCommission))
            {
                //区域针对商家抽成
                config.Add(CacheItemType.AreaForMerchantCommission, 0, RedisSaveType.HashSet, CacheLevel.Hight);
            }
            if (isAll || cacheItems.Contains(CacheItemType.AreaForPersonalWorkerCommission))
            {
                //区域针对个人服务人员抽成
                config.Add(CacheItemType.AreaForPersonalWorkerCommission, 0, RedisSaveType.HashSet, CacheLevel.Hight);
            }
            if (isAll || cacheItems.Contains(CacheItemType.AreaDefaultCommission))
            {
                //区域默认抽成
                config.Add(CacheItemType.AreaDefaultCommission, 0, RedisSaveType.HashSet, CacheLevel.Middel);
            }
            if (isAll || cacheItems.Contains(CacheItemType.ApiModuleAuthorize))
            {
                //接口对模块授权
                config.Add(CacheItemType.ApiModuleAuthorize, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.LegworkAreaConfig))
            {
                //跑腿业务区域配置
                config.Add(CacheItemType.LegworkAreaConfig, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.LegworkGlobalConfig))
            {
                //跑腿业务全局配置
                config.Add(CacheItemType.LegworkGlobalConfig, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.LegworkGoodsCategory))
            {
                //跑腿业务物品类型
                config.Add(CacheItemType.LegworkGoodsCategory, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }
            if (isAll || cacheItems.Contains(CacheItemType.LifeServiceSystemCategory))
            {
                //生活服务分类
                config.Add(CacheItemType.LifeServiceSystemCategory, 0, RedisSaveType.HashSet, CacheLevel.Permanent);
            }

            RedisConfiguration = config;
        }

        /// <summary>
        /// Redis连接信息
        /// </summary>
        public static ConfigurationOptions RedisOptions { get; private set; }

        /// <summary>
        /// 是否为长连接
        /// </summary>
        public static bool KeepAlive { get; private set; }

        /// <summary>
        /// Redis缓存配置
        /// </summary>
        public static RedisConfigurationRoot RedisConfiguration { get; private set; }

        /// <summary>
        /// 缓存数据为null时是否初始化
        /// </summary>
        public static bool InitIfNull { get; private set; }

        /// <summary>
        /// 数据库提供者类型
        /// </summary>
        public static SqlProviderType SqlType { get; private set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string SqlConnctionString { get; private set; }

        /// <summary>
        /// 二级缓存时间（秒）
        /// </summary>
        public static int Level2CacheSeconds { get; private set; }
    }
}
