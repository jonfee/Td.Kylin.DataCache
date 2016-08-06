using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 商家商品系统分类缓存
    /// </summary>
    public sealed class MerchantProductSystemCategoryCache : CacheItem<MerchantProductSystemCategoryCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="MerchantProductSystemCategoryCache"/>实例
        /// </summary>
        public MerchantProductSystemCategoryCache() : base(CacheItemType.MerchantProductSystemCategory) { }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<MerchantProductSystemCategoryCacheModel> ReadDataFromDB()
        {
            return new MerchantProductSystemCategoryService().GetEnabledAll();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="categoryID">商品分类ID</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public MerchantProductSystemCategoryCacheModel Get(long categoryID, bool allScope = true)
        {
            var item = new MerchantProductSystemCategoryCacheModel { CategoryID = categoryID };

            return Get(item.HashField, allScope);
        }
    }
}
