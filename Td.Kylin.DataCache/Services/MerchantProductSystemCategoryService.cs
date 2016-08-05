﻿using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 商家商品系统分类数据服务s
    /// </summary>
    internal sealed class MerchantProductSystemCategoryService : IMerchantProductSystemCategoryService
    {
        public List<MerchantProductSystemCategoryCacheModel> GetEnabledAll()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.MerchantGoods_SystemCategory
                            where p.IsDelete == false && p.IsDisabled == false
                            orderby p.OrderNo descending
                            select new MerchantProductSystemCategoryCacheModel
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
