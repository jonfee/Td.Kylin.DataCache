using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 商家自定义分类缓存
    /// </summary>
    public sealed class MerchantCustomCategoryCache : CacheItem<MerchantCustomCategoryCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="MerchantCustomCategoryCache"/>实例
        /// </summary>
        public MerchantCustomCategoryCache() : base(CacheItemType.MerchantCustomCategory) { }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<MerchantCustomCategoryCacheModel> ReadDataFromDB()
        {
            return new MerchantCustomCategoryService().GetEnabledAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="categoryID">商品分类ID</param>
        /// <returns></returns>
        public MerchantCustomCategoryCacheModel Get(long categoryID)
        {
            var item = new MerchantCustomCategoryCacheModel { CategoryID = categoryID };

            return Get(item.HashField);
        }
    }
}
