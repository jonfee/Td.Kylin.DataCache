using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// B2C商品分类标签缓存
    /// </summary>
    public sealed class B2CProductCategoryTagCache : CacheItem<B2CProductCategoryTagCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="B2CProductCategoryTagCache"/>实例 
        /// </summary>
        public B2CProductCategoryTagCache() : base(CacheItemType.B2CProductCategoryTags) { }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<B2CProductCategoryTagCacheModel> ReadDataFromDB()
        {
            return new B2CProductCategoryTagService().GetAll();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="tagID">标签ID</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public B2CProductCategoryTagCacheModel Get(long tagID, bool allScope = true)
        {
            var item = new B2CProductCategoryTagCacheModel { TagID = tagID };

            return Get(item.HashField,allScope);
        }
    }
}
