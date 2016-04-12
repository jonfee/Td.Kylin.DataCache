using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    internal sealed class ForumCategoryCache : CacheItem<List<ForumCategoryCacheModel>>
    {
        public ForumCategoryCache() : base(CacheItemType.ForumCategory) { }

        protected override List<ForumCategoryCacheModel> GetCache()
        {
            List<ForumCategoryCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<ForumCategoryCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<ForumCategoryCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.ForumCategoryService.GetEnabledAll();
        }

        protected override void SetCache(List<ForumCategoryCacheModel> data)
        {
            if (null != RedisDB)
            {
                //清除数据缓存
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {

                    var dic = data.ToDictionary(k => (RedisValue)k.CategoryID, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }
    }
}
