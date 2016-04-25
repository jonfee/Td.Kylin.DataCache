using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 圈子分类缓存
    /// </summary>
    public sealed class ForumCategoryCache : CacheItem<ForumCategoryCacheModel>
    {
        public ForumCategoryCache() : base(CacheItemType.ForumCategory) { }
        
        protected override List<ForumCategoryCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.ForumCategoryService.GetEnabledAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="categoryID">圈子分类ID</param>
        /// <returns></returns>
        public ForumCategoryCacheModel Get(long categoryID)
        {
            var item = new ForumCategoryCacheModel { CategoryID = categoryID };

            return Get(item.HashField);
        }
    }
}
