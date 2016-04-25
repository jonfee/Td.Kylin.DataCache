using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 区域行业推荐缓存
    /// </summary>
    public sealed class AreaRecommendIndustryCache : CacheItem<AreaRecommendIndustryCacheModel>
    {
        public AreaRecommendIndustryCache() : base(CacheItemType.AreaRecommendIndustry) { }

        protected override List<AreaRecommendIndustryCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.AreaRecommendIndustryService.GetAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <param name="industryID">行业ID</param>
        /// <returns></returns>
        public AreaRecommendIndustryCacheModel Get(int areaID, long industryID)
        {
            var item = new AreaRecommendIndustryCacheModel { AreaID = areaID, IndustryID = industryID };

            return Get(item.HashField);
        }
    }
}
