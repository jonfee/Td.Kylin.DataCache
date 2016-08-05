using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 缓存抽象类
    /// </summary>
    /// <typeparam name="T"><seealso cref="BaseCacheModel"/></typeparam>
    public abstract class CacheItem<T> : ICache where T : BaseCacheModel, new()
    {
        private readonly object locker = new object();

        /// <summary>
        /// Redis缓存配置信息
        /// </summary>
        private CacheConfig _config;

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="itemType"></param>
        protected CacheItem(CacheItemType itemType)
        {
            var config = Startup.RedisConfiguration[itemType];

            Init(config);
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="cacheKey"></param>
        protected CacheItem(string cacheKey)
        {
            var config = Startup.RedisConfiguration[cacheKey];

            Init(config);
        }

        /// <summary>
        /// 实始化方法
        /// </summary>
        /// <param name="config"></param>
        void Init(CacheConfig config)
        {
            _config = config;

            _level = _config != null ? _config.Level : CacheLevel.Permanent;

            //二级缓存
            _secondLevelCacheData = GetCache();
            _secondLevelCacheLastUpdateTime = DateTime.Now;

            #region 检测是否存在缓存，不存在则初始化更新
            if (_secondLevelCacheData == null && Startup.InitIfNull)
            {
                Update().Wait();

                _secondLevelCacheData = GetCache();
            }
            #endregion
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
        /// level
        /// </summary>
        private CacheLevel _level;
        /// <summary>
        /// 缓存级别
        /// </summary>
        public CacheLevel Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }

        /// <summary>
        /// 当前缓存操作的Redis数据库
        /// </summary>
        public IDatabase RedisDB
        {
            get
            {
                return CacheRedisContext.Redis?.GetDatabase(_config.RedisDbIndex);
            }
        }

        /// <summary>
        /// 获取缓存数据
        /// </summary>
        public List<T> Value()
        {
            return GetSecondLevelCacheData();
        }

        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <returns></returns>
        protected virtual List<T> GetCache()
        {
            if (null == RedisDB) return null;

            var dic = RedisDB.HashGetAll<T>(CacheKey);

            if (null != dic)
            {
                return dic.Select(p => p.Value).ToList();
            }

            return null;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        protected virtual void SetCache(List<T> data)
        {
            if (null == RedisDB) return;

            lock (locker)
            {
                //如果RedisKey存在，则清除
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {
                    var dic = data.ToDictionary(k => (RedisValue)k.HashField, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }

        /// <summary>
        /// 更新缓存（从数据库中读取缓存元数据，并记录最后更新缓存的时间）
        /// </summary>
        public virtual Task<bool> Update()
        {
            return Task.Run(() =>
            {
                //更新缓存源（Redis数据）
                SetCache(null);

                //清空二级缓存信息以达到重新缓存的目的
                _secondLevelCacheData = null;
                _secondLevelCacheLastUpdateTime = DateTime.Now.AddYears(-10);

                return true;
            });
        }

        /// <summary>
        /// 从数据库中读取元数据
        /// </summary>
        /// <returns></returns>
        protected abstract List<T> ReadDataFromDB();

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="entity"></param>
        public virtual Task<bool> Update(T entity)
        {
            if (null == entity || RedisDB == null) return default(Task<bool>);

            return RedisDB.HashSetAsync(CacheKey, entity.HashField, entity);
        }

        /// <summary>
        /// 添加缓存 
        /// </summary>
        /// <param name="entity"></param>
        public virtual Task<bool> Add(T entity)
        {
            if (null == entity || RedisDB == null) return default(Task<bool>);

            return RedisDB.HashSetAsync(CacheKey, entity.HashField, entity);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="entity"></param>
        public virtual Task<bool> Delete(T entity)
        {
            return Task.Run(() =>
            {
                if (null == entity || RedisDB == null) return false;

                return RedisDB.HashDelete(CacheKey, entity.HashField);
            });
        }

        /// <summary>
        /// 获取指定字段的数据项
        /// </summary>
        /// <param name="hashField"></param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public virtual T Get(string hashField, bool allScope = true)
        {
            T item = default(T);

            if (null != _secondLevelCacheData)
            {
                item = _secondLevelCacheData.Where(p => p.HashField.Equals(hashField, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }

            if (null == item && allScope)
            {
                if (null != RedisDB)
                {
                    item = RedisDB.HashGet<T>(CacheKey, hashField);
                }
            }

            return item;
        }

        /// <summary>
        /// 获取指定字段集的数据项集合
        /// </summary>
        /// <param name="hashFields"></param>
        /// <param name="removeNullOrEmpty">是否移除null或empty的数据对象</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public virtual List<T> Get(string[] hashFields, bool removeNullOrEmpty, bool allScope = true)
        {
            if (null == hashFields || hashFields.Length < 1) return null;

            List<T> list = null;

            if (null != _secondLevelCacheData)
            {
                list = _secondLevelCacheData.Where(p => hashFields.Contains(p.HashField)).ToList();
            }

            if ((list == null || list.Count < 1) && allScope)
            {
                if (null != RedisDB)
                {
                    var fields = hashFields.Select(p => (RedisValue)p).ToArray();

                    list = RedisDB.HashGet<T>(CacheKey, fields, removeNullOrEmpty);
                }
            }

            return list;
        }

        /// <summary>
        /// 重置缓存级别
        /// </summary>
        /// <param name="level"></param>
        public void ResetLevel(CacheLevel level)
        {
            this._level = level;
        }

        /// <summary>
        /// 获取缓存数据（缓存源）
        /// </summary>
        /// <returns></returns>
        List<object> ICache.GetCacheData()
        {
            if (null == RedisDB) return null;

            List<object> data = null;

            try
            {
                data = RedisDB.HashGetAll(CacheKey).Select(p => p.Value as object).ToList();
            }
            catch
            {
                data = null;
            }

            return data;
        }

        /// <summary>
        /// 获取二级缓存数据
        /// </summary>
        /// <returns></returns>
        List<object> ICache.GetLevel2CacheData()
        {
            var data = GetSecondLevelCacheData();

            List<object> list = new List<object>();

            if (null != data)
            {
                foreach(var item in data)
                {
                    list.Add(Newtonsoft.Json.JsonConvert.SerializeObject(item));
                }
            }

            return list;
        }

        /// <summary>
        /// 二级缓存最后更新时间
        /// </summary>
        private DateTime _secondLevelCacheLastUpdateTime;

        /// <summary>
        /// 二级缓存数据
        /// </summary>
        private List<T> _secondLevelCacheData;
        /// <summary>
        /// 获取二级缓存数据
        /// </summary>
        private List<T> GetSecondLevelCacheData()
        {
            //二级缓存数据为null 且 超出了缓存时间，则重新缓存
            if (_secondLevelCacheLastUpdateTime.AddSeconds(Startup.Level2CacheSeconds) <= DateTime.Now)
            {
                _secondLevelCacheData = GetCache();

                _secondLevelCacheLastUpdateTime = DateTime.Now;
            }

            return _secondLevelCacheData ?? new List<T>();
        }


    }
}
