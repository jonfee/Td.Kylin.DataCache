using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 缓存抽象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CacheItem<T> : ICache where T : BaseCacheModel, new()
    {
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
            var config = CacheStartup.RedisConfiguration[itemType];

            Init(config);
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="cacheKey"></param>
        protected CacheItem(string cacheKey)
        {
            var config = CacheStartup.RedisConfiguration[cacheKey];

            Init(config);
        }

        void Init(CacheConfig config)
        {
            _config = config;

            _level = _config != null ? _config.Level : CacheLevel.Permanent;

            #region 检测是否存在缓存，不存在则初始化更新
            List<T> temp = null;
            try
            {
                temp = GetCache();
            }
            catch
            {
                temp = null;
            }
            finally
            {
                if (temp == null) Update();
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
        protected IDatabase RedisDB
        {
            get
            {
                return RedisManager.Redis.GetDatabase(_config.RedisDbIndex);
            }
        }

        /// <summary>
        /// 更新锁
        /// </summary>
        private readonly static object _updateLock = new object();

        /// <summary>
        /// 是否正在更新
        /// </summary>
        private volatile bool _updating;

        /// <summary>
        /// 一般在更新时使用
        /// </summary>
        private List<T> _tempData;

        /// <summary>
        /// 获取缓存数据
        /// </summary>
        public List<T> Value()
        {
            List<T> _data = null;

            try
            {
                //如果正在更新，则使用更新前的临时数据
                if (_updating)
                {
                    _data = this._tempData;
                }
                else
                {
                    //从缓存中读取
                    _data = GetCache();
                }
            }
            catch
            {
                //TODO 异常时处理
            }
            finally
            {
                if (null == _data)
                {
                    _data = ReadDataFromDB();
                }
            }

            return _data;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns></returns>
        protected virtual List<T> GetCache()
        {
            try {
                return RedisDB.HashGetAll<T>(CacheKey).Select(p => p.Value).ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        protected virtual void SetCache(List<T> data)
        {
            if (null != RedisDB)
            {
                //清除数据缓存
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
        public virtual void Update()
        {
            var data = ReadDataFromDB();

            this._tempData = data;

            _updating = true;

            SetCache(data);

            _updating = false;

            this._tempData = null;
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
        public virtual void Update(T entity)
        {
            if (null == entity) return;

            RedisDB.HashSetAsync(CacheKey, entity.HashField, entity);
        }

        /// <summary>
        /// 添加缓存 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            if (null == entity) return;

            RedisDB.HashSetAsync(CacheKey, entity.HashField, entity);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            if (null == entity) return;

            RedisDB.HashDelete(CacheKey, entity.HashField);
        }

        /// <summary>
        /// 获取指定字段的数据项
        /// </summary>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public virtual T Get(string hashField)
        {
            try
            {
                return RedisDB.HashGet<T>(CacheKey, hashField);
            }
            catch
            {
                return default(T);
            }
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
        /// 获取缓存数据
        /// </summary>
        /// <returns></returns>
        public List<object> GetCacheData()
        {
            List<object> data = null;

            try
            {
                if (null != RedisDB)
                {
                    data = RedisDB.HashGetAll(CacheKey).Select(p => p.Value as object).ToList();
                }
            }
            catch
            {
                data = null;
            }

            return data;
        }
    }
}
