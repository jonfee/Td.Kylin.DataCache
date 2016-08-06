using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 区域行业推荐缓存
    /// </summary>
    public sealed class AreaRecommendIndustryCache : CacheItem<AreaRecommendIndustryCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="AreaRecommendIndustryCache"/>实例
        /// </summary>
        public AreaRecommendIndustryCache() : base(CacheItemType.AreaRecommendIndustry) { }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<AreaRecommendIndustryCacheModel> ReadDataFromDB()
        {
            return new AreaRecommendIndustryService().GetAll();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <param name="industryID">行业ID</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public AreaRecommendIndustryCacheModel Get(int areaID, long industryID, bool allScope = true)
        {
            var item = new AreaRecommendIndustryCacheModel { AreaID = areaID, IndustryID = industryID };

            return Get(item.HashField,allScope);
        }
    }
}
