using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.DataCache.Provider;
using Td.Kylin.DataCache.RedisConfig;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache
{
    public sealed class CacheCollections
    {
        private static CacheCollections _instance;

        private readonly static object mylock = new object();

        /// <summary>
        /// 缓存项实例集合
        /// </summary>
        private static Hashtable htCache = Hashtable.Synchronized(new Hashtable());

        public static CacheCollections Items
        {
            get
            {
                if (null == _instance)
                {
                    lock (mylock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CacheCollections();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 实例化CacheCollections
        /// </summary>
        private CacheCollections()
        {
            var configCollections = CacheStartup.RedisConfiguration.Collections;

            foreach (var config in configCollections)
            {
                if (null != config)
                {
                    var cacheItem = CacheItemFactory(config);

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
        public object this[CacheItemType itemType]
        {
            get
            {
                CacheConfig config = CacheStartup.RedisConfiguration[itemType];

                string cacheKey = config?.RedisKey;

                lock (htCache)
                {
                    if (Keys.Contains(cacheKey))
                    {
                        return htCache[cacheKey];
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
        public object this[string cacheKey]
        {
            get
            {
                lock (htCache)
                {
                    if (Keys.Contains(cacheKey))
                    {
                        return htCache[cacheKey];
                    }

                    return null;
                }
            }
        }

        #endregion

        /// <summary>
        /// 获取缓存数据结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public T GetCacheValue<T>(CacheItemType itemType) where T : class
        {
            object cache = this[itemType];

            T data = default(T);

            Type type = typeof(T);

            switch (itemType)
            {
                //系统区域
                case CacheItemType.SystemArea:
                    var sysArea = cache as SystemAreaCache;
                    object value = sysArea?.Data;
                    if (type.Equals(typeof(List<SystemAreaCacheModel>)))
                    {
                        data = (T)value;
                    }
                    break;
                //开通区域
                case CacheItemType.OpenArea:

                    break;
            }

            return data;
        }


        #region  缓存项实例操作

        /// <summary>
        /// 添加/更新缓存项对象实例
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cacheItem"></param>
        void AddOrUpdate(string cacheKey, object cacheItem)
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
        /// <param name="config"></param>
        /// <returns></returns>
        object CacheItemFactory(CacheConfig config)
        {
            if (null == config) return null;

            object cacheItem = null;

            switch (config.ItemType)
            {
                case CacheItemType.SystemArea:
                    cacheItem = new SystemAreaCache();
                    break;
            }

            return cacheItem;
        }

        #endregion
    }
}
