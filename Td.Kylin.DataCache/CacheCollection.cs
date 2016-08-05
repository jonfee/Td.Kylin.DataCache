using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.DataCache.Provider;

namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 缓存采集器
    /// </summary>
    public sealed class CacheCollection
    {
        /// <summary>
        /// 缓存项实例集合
        /// </summary>
        private volatile static Hashtable htCache = null;

        private readonly static object mylock = new object();

        static CacheCollection()
        {
            Reset();
        }

        /// <summary>
        /// 重置集合器
        /// </summary>
        public static void Reset()
        {
            lock (mylock)
            {
                htCache = Hashtable.Synchronized(new Hashtable());

                var configCollections = Startup.RedisConfiguration.Collections;

                foreach (var config in configCollections)
                {
                    if (null != config)
                    {
                        var cacheItem = CacheItemFactory(config.ItemType);

                        if (null != cacheItem)
                        {
                            if (htCache.ContainsKey(config.RedisKey))
                            {
                                htCache[config.RedisKey] = cacheItem;
                            }
                            else
                            {
                                htCache.Add(config.RedisKey, cacheItem);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 缓存数量
        /// </summary>
        public static int Count
        {
            get
            {
                return null != htCache ? htCache.Count : 0;
            }
        }

        /// <summary>
        /// 缓存的Key集合
        /// </summary>
        public static string[] Keys
        {
            get
            {
                var arr = new ArrayList(htCache.Keys);

                List<string> keys = new List<string>();

                foreach (var key in arr)
                {
                    var str = key as string;

                    if (null != str)
                    {
                        keys.Add(str);
                    }
                }

                keys.TrimExcess();

                return keys.ToArray();
            }
        }

        /// <summary>
        /// 缓存项生成
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        static dynamic CacheItemFactory(CacheItemType itemType)
        {
            dynamic cacheItem = null;

            switch (itemType)
            {
                //系统区域缓存
                case CacheItemType.SystemArea:
                    cacheItem = new SystemAreaCache();
                    break;
                //开通区域缓存
                case CacheItemType.OpenArea:
                    cacheItem = new OpenAreaCache();
                    break;
                //商家行业缓存
                case CacheItemType.MerchantIndustry:
                    cacheItem = new MerchantIndustryCache();
                    break;
                //区域行业推荐缓存
                case CacheItemType.AreaRecommendIndustry:
                    cacheItem = new AreaRecommendIndustryCache();
                    break;
                //B2C商品分类
                case CacheItemType.B2CProductCategory:
                    cacheItem = new B2CProductCategoryCache();
                    break;
                //B2C商品分类标签
                case CacheItemType.B2CProductCategoryTags:
                    cacheItem = new B2CProductCategoryTagCache();
                    break;
                //全局配置
                case CacheItemType.SystemGolbalConfig:
                    cacheItem = new SystemGolbalConfigCache();
                    break;
                //用户积分规则配置 
                case CacheItemType.UserPointsConfig:
                    cacheItem = new UserPointsConfigCache();
                    break;
                //用户经验值规则配置 
                case CacheItemType.UserEmpiricalConfig:
                    cacheItem = new UserEmpiricalConfigCache();
                    break;
                //用户等级配置 
                case CacheItemType.UserLevelConfig:
                    cacheItem = new UserLevelConfigCache();
                    break;
                //圈子分类
                case CacheItemType.ForumCategory:
                    cacheItem = new ForumCategoryCache();
                    break;
                //系统圈子
                case CacheItemType.ForumCircle:
                    cacheItem = new ForumCircleCache();
                    break;
                //区域圈子
                case CacheItemType.AreaForum:
                    cacheItem = new AreaForumCache();
                    break;
                //商家商品系统分类
                case CacheItemType.MerchantProductSystemCategory:
                    cacheItem = new MerchantProductSystemCategoryCache();
                    break;
                //职位类别
                case CacheItemType.JobCategory:
                    cacheItem = new JobCategoryCache();
                    break;
                //区域默认抽成
                case CacheItemType.AreaDefaultCommission:
                    cacheItem = new AreaDefaultCommissionCache();
                    break;
                //区域针对商家抽成
                case CacheItemType.AreaForMerchantCommission:
                    cacheItem = new AreaForMerchantCommissionCache();
                    break;
                //区域针对个人服务人员抽成
                case CacheItemType.AreaForPersonalWorkerCommission:
                    cacheItem = new AreaForPersonalWorkerCommissionCache();
                    break;
                //平台针对区域抽成
                case CacheItemType.PlatformCommission:
                    cacheItem = new PlatformCommissionCache();
                    break;
                //接口模块授权配置
                case CacheItemType.ApiModuleAuthorize:
                    cacheItem = new ApiModuleAuthorizeCache();
                    break;
                //跑腿业务区域配置
                case CacheItemType.LegworkAreaConfig:
                    cacheItem = new LegworkAreaConfigCache();
                    break;
                //跑腿业务全局配置
                case CacheItemType.LegworkGlobalConfig:
                    cacheItem = new LegworkGlobalConfigCache();
                    break;
                //跑腿业务物品类型
                case CacheItemType.LegworkGoodsCategory:
                    cacheItem = new LegworkGoodsCategoryCache();
                    break;
                //生活服务分类
                case CacheItemType.LifeServiceSystemCategory:
                    cacheItem = new LifeServiceSystemCategoryCache();
                    break;
            }

            return cacheItem;
        }

        #region 缓存实例及值

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        private static T GetCacheObject<T>(CacheItemType itemType) where T : ICache
        {
            CacheConfig config = Startup.RedisConfiguration[itemType];

            string cacheKey = config?.RedisKey;

            if (!string.IsNullOrWhiteSpace(cacheKey) && Keys.Contains(cacheKey))
            {
                object cache = htCache[cacheKey];

                if (cache is T)
                {
                    return (T)cache;
                }
            }

            return default(T);
        }

        /// <summary>
        /// 系统区域缓存
        /// </summary>
        public static SystemAreaCache SystemAreaCache { get { return GetCacheObject<SystemAreaCache>(CacheItemType.SystemArea); } }

        /// <summary>
        /// 开通区域缓存
        /// </summary>
        public static OpenAreaCache OpenAreaCache { get { return GetCacheObject<OpenAreaCache>(CacheItemType.OpenArea); } }

        /// <summary>
        /// 商家行业缓存
        /// </summary>
        public static MerchantIndustryCache MerchantIndustryCache { get { return GetCacheObject<MerchantIndustryCache>(CacheItemType.MerchantIndustry); } }

        /// <summary>
        /// 区域行业推荐缓存
        /// </summary>
        public static AreaRecommendIndustryCache AreaRecommendIndustryCache { get { return GetCacheObject<AreaRecommendIndustryCache>(CacheItemType.AreaRecommendIndustry); } }

        /// <summary>
        /// 商家商品系统分类缓存
        /// </summary>
        public static MerchantProductSystemCategoryCache MerchantProductSystemCategoryCache { get { return GetCacheObject<MerchantProductSystemCategoryCache>(CacheItemType.MerchantProductSystemCategory); } }

        /// <summary>
        /// B2C商品分类缓存
        /// </summary>
        public static B2CProductCategoryCache B2CProductCategoryCache { get { return GetCacheObject<B2CProductCategoryCache>(CacheItemType.B2CProductCategory); } }

        /// <summary>
        /// B2C商品分类标签缓存
        /// </summary>
        public static B2CProductCategoryTagCache B2CProductCategoryTagCache { get { return GetCacheObject<B2CProductCategoryTagCache>(CacheItemType.B2CProductCategoryTags); } }

        /// <summary>
        /// 系统全局配置缓存
        /// </summary>
        public static SystemGolbalConfigCache SystemGolbalConfigCache { get { return GetCacheObject<SystemGolbalConfigCache>(CacheItemType.SystemGolbalConfig); } }

        /// <summary>
        /// 用户积分规则配置缓存 
        /// </summary>
        public static UserPointsConfigCache UserPointsConfigCache { get { return GetCacheObject<UserPointsConfigCache>(CacheItemType.UserPointsConfig); } }

        /// <summary>
        /// 用户经验值规则配置缓存
        /// </summary>
        public static UserEmpiricalConfigCache UserEmpiricalConfigCache { get { return GetCacheObject<UserEmpiricalConfigCache>(CacheItemType.UserEmpiricalConfig); } }

        /// <summary>
        /// 用户等级配置缓存
        /// </summary>
        public static UserLevelConfigCache UserLevelConfigCache { get { return GetCacheObject<UserLevelConfigCache>(CacheItemType.UserLevelConfig); } }

        /// <summary>
        /// 圈子分类缓存
        /// </summary>
        public static ForumCategoryCache ForumCategoryCache { get { return GetCacheObject<ForumCategoryCache>(CacheItemType.ForumCategory); } }

        /// <summary>
        /// 系统圈子缓存
        /// </summary>
        public static ForumCircleCache ForumCircleCache { get { return GetCacheObject<ForumCircleCache>(CacheItemType.ForumCircle); } }

        /// <summary>
        /// 区域圈子缓存
        /// </summary>
        public static AreaForumCache AreaForumCache { get { return GetCacheObject<AreaForumCache>(CacheItemType.AreaForum); } }

        /// <summary>
        /// 职位类别缓存
        /// </summary>
        public static JobCategoryCache JobCategoryCache { get { return GetCacheObject<JobCategoryCache>(CacheItemType.JobCategory); } }

        /// <summary>
        /// 平台针对区域抽成缓存
        /// </summary>
        public static PlatformCommissionCache PlatformCommissionCache { get { return GetCacheObject<PlatformCommissionCache>(CacheItemType.PlatformCommission); } }

        /// <summary>
        /// 区域默认抽成缓存
        /// </summary>
        public static AreaDefaultCommissionCache AreaDefaultCommissionCache { get { return GetCacheObject<AreaDefaultCommissionCache>(CacheItemType.AreaDefaultCommission); } }

        /// <summary>
        /// 区域针对商家抽成缓存
        /// </summary>
        public static AreaForMerchantCommissionCache AreaForMerchantCommissionCache { get { return GetCacheObject<AreaForMerchantCommissionCache>(CacheItemType.AreaForMerchantCommission); } }

        /// <summary>
        /// 区域针对个人服务人员抽成缓存
        /// </summary>
        public static AreaForPersonalWorkerCommissionCache AreaForPersonalWorkerCommissionCache { get { return GetCacheObject<AreaForPersonalWorkerCommissionCache>(CacheItemType.AreaForPersonalWorkerCommission); } }

        /// <summary>
        /// 接口对模块授权缓存
        /// </summary>
        public static ApiModuleAuthorizeCache ApiModuleAuthorizeCache { get { return GetCacheObject<ApiModuleAuthorizeCache>(CacheItemType.ApiModuleAuthorize); } }

        /// <summary>
        /// 跑腿业务区域配置缓存
        /// </summary>
        public static LegworkAreaConfigCache LegworkAreaConfigCache { get { return GetCacheObject<LegworkAreaConfigCache>(CacheItemType.LegworkAreaConfig); } }

        /// <summary>
        /// 跑腿业务全局配置缓存
        /// </summary>
        public static LegworkGlobalConfigCache LegworkGlobalConfigCache { get { return GetCacheObject<LegworkGlobalConfigCache>(CacheItemType.LegworkGlobalConfig); } }

        /// <summary>
        /// 跑腿业务物品类型缓存
        /// </summary>
        public static LegworkGoodsCategoryCache LegworkGoodsCategoryCache { get { return GetCacheObject<LegworkGoodsCategoryCache>(CacheItemType.LegworkGoodsCategory); } }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="itemType"><seealso cref="CacheItemType"/>枚举成员</param>
        /// <returns></returns>
        public static ICache GetCache(CacheItemType itemType)
        {
            ICache cache = null;

            try
            {
                var vals = htCache.Values;

                if (null != vals)
                {
                    foreach (var item in vals)
                    {
                        var temp = (ICache)item;

                        if (temp.ItemType == itemType)
                        {
                            cache = temp;
                        }
                    }
                }
            }
            catch
            {
                //TODO 
                cache = null;
            }

            return cache;
        }

        /// <summary>
        /// 获取所有缓存项
        /// </summary>
        /// <returns></returns>
        public static List<ICache> GetAllCache()
        {
            try
            {
                var vals = htCache.Values;

                List<ICache> list = null;

                if (null != vals)
                {
                    list = new List<ICache>();
                    foreach (var item in vals)
                    {
                        list.Add((ICache)item);
                    }
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定缓存级别的缓存集合列表
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static List<ICache> GetCacheList(CacheLevel level)
        {
            var list = GetAllCache();

            if (null != list)
            {
                return list.Where(p => p.Level == level).ToList();
            }

            return null;
        }

        /// <summary>
        /// 更新指定级别的缓存
        /// </summary>
        /// <param name="level"></param>
        public static Task<bool> Update(CacheLevel level)
        {
            return new Task<bool>(() =>
            {
                var list = GetCacheList(level);

                if (null != list)
                {
                    foreach (var cache in list)
                    {
                        cache.Update();
                    }
                }

                return true;
            });
        }

        /// <summary>
        /// 更新缓存级别
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="level"></param>
        public static void ResetLevel(CacheItemType itemType, CacheLevel level)
        {
            var cache = GetCache(itemType);

            if (null != cache)
            {
                cache.ResetLevel(level);
            }
        }

        #endregion
    }
}
