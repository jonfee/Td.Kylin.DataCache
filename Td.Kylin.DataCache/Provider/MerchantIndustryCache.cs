using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 商家行业缓存
    /// </summary>
    public sealed class MerchantIndustryCache : CacheItem<MerchantIndustryCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="MerchantIndustryCache"/>实例
        /// </summary>
        public MerchantIndustryCache() : base(CacheItemType.MerchantIndustry) { }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<MerchantIndustryCacheModel> ReadDataFromDB()
        {
            return new MerchantIndustryService().GetEnabledAll();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="industryID">行业ID</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public MerchantIndustryCacheModel Get(long industryID, bool allScope = true)
        {
            var item = new MerchantIndustryCacheModel { IndustryID = industryID };

            return Get(item.HashField, allScope);
        }
    }
}
