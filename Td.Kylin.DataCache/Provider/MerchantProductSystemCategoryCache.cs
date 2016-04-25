using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 商家商品系统分类缓存
    /// </summary>
    public sealed class MerchantProductSystemCategoryCache : CacheItem<MerchantProductSystemCategoryCacheModel>
    {
        public MerchantProductSystemCategoryCache() : base(CacheItemType.MerchantProductSystemCategory) { }
        
        protected override List<MerchantProductSystemCategoryCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.MerchantProductSystemCategoryService.GetEnabledAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="categoryID">商品分类ID</param>
        /// <returns></returns>
        public MerchantProductSystemCategoryCacheModel Get(long categoryID)
        {
            var item = new MerchantProductSystemCategoryCacheModel { CategoryID = categoryID };

            return Get(item.HashField);
        }
    }
}
