using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// B2C商品分类标签缓存
    /// </summary>
    public sealed class B2CProductCategoryTagCache : CacheItem<B2CProductCategoryTagCacheModel>
    {
        public B2CProductCategoryTagCache() : base(CacheItemType.B2CProductCategoryTags) { }

        protected override List<B2CProductCategoryTagCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.B2CProductCategoryTagService.GetAll();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="tagID">标签ID</param>
        /// <returns></returns>
        public B2CProductCategoryTagCacheModel Get(long tagID)
        {
            var item = new B2CProductCategoryTagCacheModel { TagID = tagID };

            return Get(item.HashField);
        }
    }
}
