using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Provider;

namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 缓存采集器
    /// </summary>
    public sealed class CacheCollection
    {
        private static CacheCollection _instance;

        private readonly static object mylock = new object();

        /// <summary>
        /// 缓存项实例集合
        /// </summary>
        private static Hashtable htCache = Hashtable.Synchronized(new Hashtable());

        public static CacheCollection Items
        {
            get
            {
                if (null == _instance)
                {
                    lock (mylock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CacheCollection();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 实例化CacheCollections
        /// </summary>
        private CacheCollection()
        {
            var configCollections = CacheStartup.RedisConfiguration.Collections;

            foreach (var config in configCollections)
            {
                if (null != config)
                {
                    var cacheItem = CacheItemFactory(config.ItemType);

                    if (null != cacheItem)
                    {
                        AddOrUpdate(config.RedisKey, cacheItem);
                    }
                }
            }
        }

        /// <summary>
        /// 所有缓存的Key集合
        /// </summary>
        public string[] Keys
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

        #region 索引器

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        private ICache this[CacheItemType itemType]
        {
            get
            {
                CacheConfig config = CacheStartup.RedisConfiguration[itemType];

                string cacheKey = config?.RedisKey;

                lock (htCache)
                {
                    if (Keys.Contains(cacheKey))
                    {
                        return (ICache)htCache[cacheKey];
                    }

                    return null;
                }
            }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        private ICache this[string cacheKey]
        {
            get
            {
                lock (htCache)
                {
                    if (Keys.Contains(cacheKey))
                    {
                        return (ICache)htCache[cacheKey];
                    }

                    return null;
                }
            }
        }

        #endregion

        #region 更新缓存

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="itemType"></param>
        public void Update(CacheItemType itemType)
        {
            var cache = this[itemType];

            Update(cache);
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        public void Update(string  cacheKey)
        {
            var cache = this[cacheKey];

            Update(cache);
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="cache"></param>
        private void Update(ICache cache)
        {
            if (null != cache)
            {
                cache.Update();
            }
        }

        #endregion

        #region 获取缓存数据结果

        /// <summary>
        /// 获取缓存数据结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public T GetCacheValue<T>(CacheItemType itemType) where T : class
        {
            var cache = this[itemType];

            T data = default(T);

            switch (itemType)
            {
                //系统区域
                case CacheItemType.SystemArea:
                    data = GetCacheValue<T, SystemAreaCache, List<SystemAreaCacheModel>>(cache);
                    break;
                //开通区域
                case CacheItemType.OpenArea:
                    data = GetCacheValue<T, OpenAreaCache, List<OpenAreaCacheModel>>(cache);
                    break;
                //商家行业
                case CacheItemType.MerchantIndustry:
                    data = GetCacheValue<T, MerchantIndustryCache, List<MerchantIndustryCacheModel>>(cache);
                    break;
                //区域行业推荐
                case CacheItemType.AreaRecommendIndustry:
                    data = GetCacheValue<T, AreaRecommendIndustryCache, List<AreaRecommendIndustryCacheModel>>(cache);
                    break;
                //上门预约业务
                case CacheItemType.BusinessServices:
                    data = GetCacheValue<T, BusinessServiceCache, List<BusinessServiceCacheModel>>(cache);
                    break;
                //B2C商品分类
                case CacheItemType.B2CProductCategory:
                    data = GetCacheValue<T, B2CProductCategoryCache, List<B2CProductCategoryCacheModel>>(cache);
                    break;
                //B2C商品分类标签
                case CacheItemType.B2CProductCategoryTags:
                    data = GetCacheValue<T, B2CProductCategoryTagCache, List<B2CProductCategoryTagCacheModel>>(cache);
                    break;
                //系统全局配置
                case CacheItemType.SystemGolbalConfig:
                    data = GetCacheValue<T, SystemGolbalConfigCache, List<SystemGolbalConfigCacheModel>>(cache);
                    break;
                //用户积分规则配置
                case CacheItemType.UserPointsConfig:
                    data = GetCacheValue<T, UserPointsConfigCache, List<UserPointsConfigCacheModel>>(cache);
                    break;
                //用户经验值规则配置
                case CacheItemType.UserEmpiricalConfig:
                    data = GetCacheValue<T, UserEmpiricalConfigCache, List<UserEmpiricalConfigCacheModel>>(cache);
                    break;
                //用户等级配置
                case CacheItemType.UserLevelConfig:
                    data = GetCacheValue<T, UserLevelConfigCache, List<UserLevelConfigCacheModel>>(cache);
                    break;
                //圈子分类
                case CacheItemType.ForumCategory:
                    data = GetCacheValue<T, ForumCategoryCache, List<ForumCategoryCacheModel>>(cache);
                    break;
                //系统圈子
                case CacheItemType.ForumCircle:
                    data = GetCacheValue<T, ForumCircleCache, List<ForumCircleCacheModel>>(cache);
                    break;
                //区域圈子
                case CacheItemType.AreaForum:
                    data = GetCacheValue<T, AreaForumCache, List<AreaForumCacheModel>>(cache);
                    break;
            }

            return data;
        }

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <typeparam name="CacheResult"></typeparam>
        /// <typeparam name="CacheProvider"></typeparam>
        /// <param name="cache"></param>
        /// <returns></returns>
        CacheResult GetCacheValue<CacheResult, CacheProvider, ProviderResult>(ICache cache) where CacheProvider : CacheItem<ProviderResult>
        {
            var data = default(CacheResult);

            try
            {
                var provider = cache as CacheProvider;

                if (null != provider && typeof(CacheResult).Equals(typeof(ProviderResult)))
                {
                    object temp = provider.Data;

                    data = (CacheResult)temp;
                }
            }
            catch
            {
                //Exception
            }

            return data;
        }

        #endregion

        #region  缓存项实例操作

        /// <summary>
        /// 添加/更新缓存项对象实例
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cacheItem"></param>
        void AddOrUpdate(string cacheKey, ICache cacheItem)
        {
            if (string.IsNullOrWhiteSpace(cacheKey) || null == cacheItem) return;

            lock (htCache)
            {
                if (Keys.Contains(cacheKey))
                {
                    htCache[cacheKey] = cacheItem;
                }
                else
                {
                    htCache.Add(cacheKey, cacheItem);
                }
            }
        }

        /// <summary>
        /// 缓存项生成
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        ICache CacheItemFactory(CacheItemType itemType)
        {
            ICache cacheItem = null;

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
                //上门预约业务缓存
                case CacheItemType.BusinessServices:
                    cacheItem = new BusinessServiceCache();
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
            }

            return cacheItem;
        }

        #endregion

        #region 缓存实例及值

        /// <summary>
        /// 系统区域缓存数据
        /// </summary>
        public List<SystemAreaCacheModel> SystemArea
        {
            get
            {
                return GetCacheValue<List<SystemAreaCacheModel>>(CacheItemType.SystemArea);
            }
        }

        /// <summary>
        /// 开通区域缓存数据
        /// </summary>
        public List<OpenAreaCacheModel> OpenArea
        {
            get
            {
                return GetCacheValue<List<OpenAreaCacheModel>>(CacheItemType.OpenArea);
            }
        }

        /// <summary>
        /// 商家行业缓存数据
        /// </summary>
        public List<MerchantIndustryCacheModel> MerchantIndustry
        {
            get
            {
                return GetCacheValue<List<MerchantIndustryCacheModel>>(CacheItemType.MerchantIndustry);
            }
        }

        /// <summary>
        /// 区域行业推荐
        /// </summary>
        public List<AreaRecommendIndustryCacheModel> AreaRecommendIndustry
        {
            get
            {
                return GetCacheValue<List<AreaRecommendIndustryCacheModel>>(CacheItemType.AreaRecommendIndustry);
            }
        }

        /// <summary>
        /// 上门预约服务缓存数据
        /// </summary>
        public List<BusinessServiceCacheModel> BusinessService
        {
            get
            {
                return GetCacheValue<List<BusinessServiceCacheModel>>(CacheItemType.BusinessServices);
            }
        }

        /// <summary>
        /// B2C商品分类
        /// </summary>
        public List<B2CProductCategoryCacheModel> B2CProductCategory
        {
            get
            {
                return GetCacheValue<List<B2CProductCategoryCacheModel>>(CacheItemType.B2CProductCategory);
            }
        }

        /// <summary>
        /// B2C商品分类标签 
        /// </summary>
        public List<B2CProductCategoryTagCacheModel> B2CProductCategoryTag
        {
            get
            {
                return GetCacheValue<List<B2CProductCategoryTagCacheModel>>(CacheItemType.B2CProductCategoryTags);
            }
        }

        /// <summary>
        /// 系统全局配置
        /// </summary>
        public List<SystemGolbalConfigCacheModel> SystemGolbalConfig
        {
            get
            {
                return GetCacheValue<List<SystemGolbalConfigCacheModel>>(CacheItemType.SystemGolbalConfig);
            }
        }

        /// <summary>
        /// 用户积分规则配置 
        /// </summary>
        public List<UserPointsConfigCacheModel> UserPointsConfig
        {
            get
            {
                return GetCacheValue<List<UserPointsConfigCacheModel>>(CacheItemType.UserPointsConfig);
            }
        }

        /// <summary>
        /// 用户经验值规则配置 
        /// </summary>
        public List<UserEmpiricalConfigCacheModel> UserEmpiricalConfig
        {
            get
            {
                return GetCacheValue<List<UserEmpiricalConfigCacheModel>>(CacheItemType.UserEmpiricalConfig);
            }
        }

        /// <summary>
        /// 用户等级配置 
        /// </summary>
        public List<UserLevelConfigCacheModel> UserLevelConfig
        {
            get
            {
                return GetCacheValue<List<UserLevelConfigCacheModel>>(CacheItemType.UserLevelConfig);
            }
        }

        /// <summary>
        /// 圈子分类
        /// </summary>
        public List<ForumCategoryCacheModel> ForumCategory
        {
            get
            {
                return GetCacheValue<List<ForumCategoryCacheModel>>(CacheItemType.ForumCategory);
            }
        }

        /// <summary>
        /// 系统圈子
        /// </summary>
        public List<ForumCircleCacheModel> ForumCircle
        {
            get
            {
                return GetCacheValue<List<ForumCircleCacheModel>>(CacheItemType.ForumCircle);
            }
        }

        /// <summary>
        /// 区域圈子
        /// </summary>
        public List<AreaForumCacheModel> AreaForum
        {
            get
            {
                return GetCacheValue<List<AreaForumCacheModel>>(CacheItemType.AreaForum);
            }
        }

        #endregion
    }
}
