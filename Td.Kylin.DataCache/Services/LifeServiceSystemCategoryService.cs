using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 生活服务系统分类
    /// </summary>
    internal sealed class LifeServiceSystemCategoryService : ILifeServiceSystemCategoryService
    {
        public List<LifeServiceSystemCategoryCacheModel> GetEnabledAll()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.Service_SystemCategory
                            where p.IsDelete == false && p.IsDisabled == false
                            orderby p.OrderNo descending
                            select new LifeServiceSystemCategoryCacheModel
                            {
                                CategoryID = p.CategoryID,
                                CategoryPath = p.CategoryPath,
                                Icon = p.Icon,
                                Name = p.Name,
                                OrderNo = p.OrderNo,
                                ParentCategoryID = p.ParentCategoryID
                            };

                return query.ToList();
            }
        }
    }
}
