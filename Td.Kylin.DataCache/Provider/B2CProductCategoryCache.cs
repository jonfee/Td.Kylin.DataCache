using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// B2C商品分类缓存
    /// </summary>
    public sealed class B2CProductCategoryCache : CacheItem<B2CProductCategoryCacheModel>
    {
        public B2CProductCategoryCache() : base(CacheItemType.B2CProductCategory) { }
        
        protected override List<B2CProductCategoryCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.B2CProductCategoryService.GetEnabledAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="categoryID">商品分类ID</param>
        /// <returns></returns>
        public B2CProductCategoryCacheModel Get(long categoryID)
        {
            var item = new B2CProductCategoryCacheModel { CategoryID = categoryID };

            return Get(item.HashField);
        }
    }
}
