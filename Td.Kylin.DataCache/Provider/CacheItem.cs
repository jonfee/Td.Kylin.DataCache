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

        /// <summary>
        /// 实始化方法
        /// </summary>
        /// <param name="config"></param>
        void Init(CacheConfig config)
        {
            _config = config;

            _level = _config != null ? _config.Level : CacheLevel.Permanent;

            #region 检测是否存在缓存，不存在则初始化更新
            if (CacheStartup.InitIfNull)
            {
                List<T> temp = GetCache();

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
        /// redis database
        /// </summary>
        private IDatabase _redisDB;

        /// <summary>
        /// 当前缓存操作的Redis数据库
        /// </summary>
        public IDatabase RedisDB
        {
            get
            {
                //redis database is null or multiplexer not is connected
                //create a new RedisContext and database
                if (null == _redisDB || !CacheStartup.RedisContext.IsConnected)
                {
                    if (null == CacheStartup.RedisContext)
                    {
                        CacheStartup.RedisContext = new RedisContext(CacheStartup.RedisOptions);
                    }

                    IDatabase tempDB = CacheStartup.RedisContext.GetDatabase(_config.RedisDbIndex);

                    if (CacheStartup.KeepAlive)
                    {
                        _redisDB = tempDB;
                    }
                    else
                    {
                        return tempDB;
                    }
                }

                return _redisDB;
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

                if (null == _data)
                {
                    _data = ReadDataFromDB();
                }
            }
            catch
            {
                //TODO 异常时处理
            }

            return _data;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns></returns>
        protected virtual List<T> GetCache()
        {
            if (null == RedisDB) return null;

            try
            {
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
                //如果RedisKey存在，则清除
                if (RedisDB.KeyExists(CacheKey))
                {
                    RedisDB.KeyDelete(CacheKey);
                }

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
            if (null == entity || RedisDB == null) return;

            RedisDB.HashSetAsync(CacheKey, entity.HashField, entity);
        }

        /// <summary>
        /// 添加缓存 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            if (null == entity || RedisDB == null) return;

            RedisDB.HashSetAsync(CacheKey, entity.HashField, entity);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            if (null == entity || RedisDB == null) return;

            RedisDB.HashDelete(CacheKey, entity.HashField);
        }

        /// <summary>
        /// 获取指定字段的数据项
        /// </summary>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public virtual T Get(string hashField)
        {
            if (null == RedisDB) return default(T);

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
        /// 获取指定字段集的数据项集合
        /// </summary>
        /// <param name="hashFields"></param>
        /// <param name="removeNullOrEmpty">是否移除null或empty的数据对象</param>
        /// <returns></returns>
        public virtual List<T> Get(string[] hashFields, bool removeNullOrEmpty)
        {
            if (null == RedisDB) return null;

            if (null == hashFields || hashFields.Length < 1) return null;

            try
            {
                var fields = hashFields.Select(p => (RedisValue)p).ToArray();

                return RedisDB.HashGet<T>(CacheKey, fields, removeNullOrEmpty);
            }
            catch
            {
                return null;
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
    }
}
