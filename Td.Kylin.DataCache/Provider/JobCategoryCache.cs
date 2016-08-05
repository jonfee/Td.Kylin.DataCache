using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 职位类别缓存
    /// </summary>
    public sealed class JobCategoryCache : CacheItem<JobCategoryCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="JobCategoryCache"/>实例
        /// </summary>
        public JobCategoryCache() : base(CacheItemType.JobCategory) { }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        /// <returns></returns>
        public JobCategoryCacheModel Get(long categoryID)
        {
            var item = new JobCategoryCacheModel { CategoryID = categoryID };

            return Get(item.HashField);
        }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<JobCategoryCacheModel> ReadDataFromDB()
        {
            return new JobCategoryService().GetAll();
        }
        
    }
}
