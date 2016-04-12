using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 全局配置缓存
    /// </summary>
    internal sealed class SystemGolbalConfigCache : CacheItem<List<SystemGolbalConfigCacheModel>>
    {
        public SystemGolbalConfigCache() : base(CacheItemType.SystemGolbalConfig) { }

        protected override List<SystemGolbalConfigCacheModel> GetCache()
        {
            List<SystemGolbalConfigCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<SystemGolbalConfigCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<SystemGolbalConfigCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.SystemGolbalConfigService.GetAll();
        }

        protected override void SetCache(List<SystemGolbalConfigCacheModel> data)
        {
            if (null != RedisDB)
            {
                //清除数据缓存
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {

                    var dic = data.ToDictionary(k => (RedisValue)k.ConfigID, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }
    }
}
