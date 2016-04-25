using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 商家行业缓存
    /// </summary>
    public sealed class MerchantIndustryCache : CacheItem<MerchantIndustryCacheModel>
    {
        public MerchantIndustryCache() : base(CacheItemType.MerchantIndustry) { }

        protected override List<MerchantIndustryCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.MerchantIndustryService.GetEnabledAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="industryID">行业ID</param>
        /// <returns></returns>
        public MerchantIndustryCacheModel Get(long industryID)
        {
            var item = new MerchantIndustryCacheModel { IndustryID = industryID };

            return Get(item.HashField);
        }
    }
}
