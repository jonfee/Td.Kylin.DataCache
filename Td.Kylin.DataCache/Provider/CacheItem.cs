using StackExchange.Redis;
using System;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    internal abstract class CacheItem<T>
    {
        /// <summary>
        /// Redis缓存配置信息
        /// </summary>
        private CacheConfig _config;

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="itemType"></param>
        public CacheItem(CacheItemType itemType)
        {
            _config = CacheStartup.RedisConfiguration[itemType];
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="cacheKey"></param>
        public CacheItem(string cacheKey)
        {
            _config = CacheStartup.RedisConfiguration[cacheKey];
        }

        /// <summary>
        /// 缓存项类型
        /// </summary>
        public CacheItemType ItemType
        {
            get
            {
                return _config != null ? _config.ItemType : default(CacheItemType);
            }
        }

        /// <summary>
        /// 缓存Key
        /// </summary>
        public string CacheKey
        {
            get
            {
                return _config?.RedisKey;
            }
        }

        /// <summary>
        /// 缓存级别
        /// </summary>
        public CacheLevel Level
        {
            get
            {
                return _config != null ? _config.Level : CacheLevel.Permanent;
            }
        }

        /// <summary>
        /// 当前缓存操作的Redis数据库
        /// </summary>
        protected IDatabase RedisDB
        {
            get
            {
                return RedisManager.Redis.GetDatabase(_config.RedisDbIndex);
            }
        }

        private T _data;

        /// <summary>
        /// 缓存数据
        /// </summary>
        public T Data
        {
            get
            {
                if (Level == CacheLevel.Hight && LastUpdateTime.AddDays(1) < DateTime.Now)  //缓存级别高，一般24小时更新一次
                {
                    Update();
                }
                else if (Level == CacheLevel.Middel && LastUpdateTime.AddHours(6) < DateTime.Now)   //缓存级别中，一般6小时更新一次
                {
                    Update();
                }
                else if (Level == CacheLevel.Lower && LastUpdateTime.AddMinutes(30) < DateTime.Now) //缓存级别低，一般30分钟更新一次
                {
                    Update();
                }

                if (null == _data)
                {
                    _data = GetCache();
                }

                return _data;
            }
        }

        /// <summary>
        /// 最后一次更新缓存时间
        /// </summary>
        public DateTime LastUpdateTime { get; private set; }

        /// <summary>
        /// 更新缓存（从数据库中读取缓存元数据，并记录最后更新缓存的时间）
        /// </summary>
        public void Update()
        {
            this._data = ReadDataFromDB();

            this.LastUpdateTime = DateTime.Now;

            SetCache(this._data);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns></returns>
        protected abstract T GetCache();

        /// <summary>
        /// 设置缓存
        /// </summary>
        protected abstract void SetCache(T data);

        /// <summary>
        /// 从数据库中读取元数据
        /// </summary>
        /// <returns></returns>
        protected abstract T ReadDataFromDB();
    }
}
