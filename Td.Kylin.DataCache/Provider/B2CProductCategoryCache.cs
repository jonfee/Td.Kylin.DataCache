using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// B2C商品分类缓存
    /// </summary>
    internal sealed class B2CProductCategoryCache : CacheItem<List<B2CProductCategoryCacheModel>>
    {
        public B2CProductCategoryCache() : base(CacheItemType.B2CProductCategory) { }

        protected override List<B2CProductCategoryCacheModel> GetCache()
        {
            List<B2CProductCategoryCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<B2CProductCategoryCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<B2CProductCategoryCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.B2CProductCategoryService.GetEnabledAll();
        }

        protected override void SetCache(List<B2CProductCategoryCacheModel> data)
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
