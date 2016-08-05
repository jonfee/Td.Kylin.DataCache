using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 圈子分类数据服务
    /// </summary>
    internal sealed class ForumCategoryService : IForumCategoryService
    {
        public List<ForumCategoryCacheModel> GetEnabledAll()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.Circle_Category
                            where p.IsDelete == false && p.Disabled == false
                            orderby p.OrderNo descending
                            select new ForumCategoryCacheModel
                            {
                                CategoryID=p.CategoryID,
                                Name=p.Name,
                                OrderNo=p.OrderNo
                            };

                return query.ToList();
            }
        }
    }
}
