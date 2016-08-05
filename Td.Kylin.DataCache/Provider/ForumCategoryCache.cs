using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 圈子分类缓存
    /// </summary>
    public sealed class ForumCategoryCache : CacheItem<ForumCategoryCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="ForumCategoryCache"/>实例
        /// </summary>
        public ForumCategoryCache() : base(CacheItemType.ForumCategory) { }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<ForumCategoryCacheModel> ReadDataFromDB()
        {
            return new ForumCategoryService().GetEnabledAll();
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
