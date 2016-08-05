using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 职位类别数据服务
    /// </summary>
    internal sealed class JobCategoryService : IJobCategoryService
    {
        public List<JobCategoryCacheModel> GetAll()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.Job_Category
                            select new JobCategoryCacheModel
                            {
                                CategoryID = p.CategoryID,
                                Name = p.Name,
                                OrderNo = p.OrderNo,
                                ParentID = p.ParentID,
                                TagStatus = p.TagStatus
                            };

                return query.ToList();
            }
        }
    }
}
