using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 职位类别缓存
    /// </summary>
    public sealed class JobCategoryCache : CacheItem<JobCategoryCacheModel>
    {
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
        
        protected override List<JobCategoryCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.JobCategoryService.GetAll();
        }
        
    }
}
