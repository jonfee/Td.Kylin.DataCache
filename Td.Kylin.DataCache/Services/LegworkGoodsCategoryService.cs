using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;


namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 跑腿物品类型数据服务
    /// </summary>
    internal class LegworkGoodsCategoryService : ILegworkGoodsCategoryService
    {
        public List<LegworkGoodsCategoryCacheModel> GetAll()
        {
            using (var db = new DataContext())
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
