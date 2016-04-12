using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 开通区域数据缓存
    /// </summary>
    internal sealed class OpenAreaCache : CacheItem<List<OpenAreaCacheModel>>
    {
        public OpenAreaCache() : base(CacheItemType.OpenArea) { }

        protected override List<OpenAreaCacheModel> GetCache()
        {
            List<OpenAreaCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<OpenAreaCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<OpenAreaCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.OpenAreaService.GetAll();
        }

        protected override void SetCache(List<OpenAreaCacheModel> data)
        {

            if (null != RedisDB)
            {
                //清除数据缓存
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {

                    var dic = data.ToDictionary(k => (RedisValue)k.AreaID, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }
    }
}
