using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    internal sealed class AreaForumCache : CacheItem<List<AreaForumCacheModel>>
    {
        public AreaForumCache() : base(CacheItemType.AreaForum) { }

        protected override List<AreaForumCacheModel> GetCache()
        {
            List<AreaForumCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<AreaForumCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<AreaForumCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.AreaForumService.GetEnabledAll();
        }

        protected override void SetCache(List<AreaForumCacheModel> data)
        {
            if (null != RedisDB)
            {
                //清除数据缓存
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {

                    var dic = data.ToDictionary(k => (RedisValue)k.AreaForumID, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }
    }
}
