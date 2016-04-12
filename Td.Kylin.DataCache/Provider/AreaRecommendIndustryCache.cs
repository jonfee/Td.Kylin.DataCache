using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 区域行业推荐缓存
    /// </summary>
    internal sealed class AreaRecommendIndustryCache : CacheItem<List<AreaRecommendIndustryCacheModel>>
    {
        public AreaRecommendIndustryCache() : base(CacheItemType.AreaRecommendIndustry) { }

        protected override List<AreaRecommendIndustryCacheModel> GetCache()
        {
            List<AreaRecommendIndustryCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<AreaRecommendIndustryCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<AreaRecommendIndustryCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.AreaRecommendIndustryService.GetAll();
        }

        protected override void SetCache(List<AreaRecommendIndustryCacheModel> data)
        {
            if (null != RedisDB)
            {
                //清除数据缓存
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {

                    var dic = data.ToDictionary(k => (RedisValue)k.AreaIndustryID, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }
    }
}
