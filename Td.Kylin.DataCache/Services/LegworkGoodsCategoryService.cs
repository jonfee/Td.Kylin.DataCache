using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;


namespace Td.Kylin.DataCache.Services
{
    internal class LegworkGoodsCategoryService<DbContext> : ILegworkGoodsCategoryService where DbContext : DataContext, new()
    {
        public List<LegworkGoodsCategoryCacheModel> GetAll()
        {
            using (var db = new DbContext())
            {
                return (from p in db.Legwork_GoodsCategory
                        where p.IsDelete == false
                        select new LegworkGoodsCategoryCacheModel
                        {
                            CategoryID = p.CategoryID,
                            Name = p.Name,
                            SortOrder = p.SortOrder
                        }).ToList();
            }
        }
    }
}
