using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    internal sealed class ForumCircleCache : CacheItem<List<ForumCircleCacheModel>>
    {
        public ForumCircleCache() : base(CacheItemType.ForumCircle) { }

        protected override List<ForumCircleCacheModel> GetCache()
        {
            List<ForumCircleCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<ForumCircleCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<ForumCircleCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.ForumCircleService.GetEnabledAll();
        }

        protected override void SetCache(List<ForumCircleCacheModel> data)
        {
            if (null != RedisDB)
            {
                //清除数据缓存
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {

                    var dic = data.ToDictionary(k => (RedisValue)k.ForumID, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }
    }
}
