using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// B2C商品分类标签缓存
    /// </summary>
    internal sealed class B2CProductCategoryTagCache : CacheItem<List<B2CProductCategoryTagCacheModel>>
    {
        public B2CProductCategoryTagCache() : base(CacheItemType.B2CProductCategoryTags) { }

        protected override List<B2CProductCategoryTagCacheModel> GetCache()
        {
            List<B2CProductCategoryTagCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<B2CProductCategoryTagCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<B2CProductCategoryTagCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.B2CProductCategoryTagService.GetAll();
        }

        protected override void SetCache(List<B2CProductCategoryTagCacheModel> data)
        {
            if (null != RedisDB)
            {
                //清除数据缓存
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {

                    var dic = data.ToDictionary(k => (RedisValue)k.TagID, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }
    }
}
