using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 商家自定义分类数据服务
    /// </summary>
    internal sealed class MerchantCustomCategoryService : IMerchantCustomCategoryService
    {
        public List<MerchantCustomCategoryCacheModel> GetEnabledAll()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.MerchGoods_Category
                            where p.IsDelete == false
                            orderby p.OrderNo descending
                            select new MerchantCustomCategoryCacheModel
                            {
                                CategoryID = p.CategoryID,
                                Name = p.Name,
                                OrderNo = p.OrderNo,
                                MerchantID = p.MerchantID
                            };

                return query.ToList();
            }
        }
    }
}
