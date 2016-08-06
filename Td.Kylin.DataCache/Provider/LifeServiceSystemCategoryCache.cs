using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 生活服务缓存
    /// </summary>
    public sealed class LifeServiceSystemCategoryCache : CacheItem<LifeServiceSystemCategoryCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="AreaForumCache"/>实例
        /// </summary>
        public LifeServiceSystemCategoryCache() : base(CacheItemType.LifeServiceSystemCategory) { }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public LifeServiceSystemCategoryCacheModel Get(long categoryID, bool allScope = true)
        {
            var item = new LifeServiceSystemCategoryCacheModel { CategoryID = categoryID };

            return Get(item.HashField,allScope);
        }
        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<LifeServiceSystemCategoryCacheModel> ReadDataFromDB()
        {
            return new LifeServiceSystemCategoryService().GetEnabledAll();
        }
    }
}
