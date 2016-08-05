﻿using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 精品汇商品分类数据服务
    /// </summary>
    internal sealed class B2CProductCategoryService : IB2CProductCategoryService
    {
        public List<B2CProductCategoryCacheModel> GetEnabledAll()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.Mall_Category
                            where p.IsDelete == false && p.Disabled == false
                            select new B2CProductCategoryCacheModel
                            {
                                AreaID = p.AreaID,
                                CategoryID = p.CategoryID,
                                Ico = p.Ico,
                                Name = p.Name,
                                OrderNo = p.OrderNo
                            };

                return query.ToList();
            }
        }
    }
}
