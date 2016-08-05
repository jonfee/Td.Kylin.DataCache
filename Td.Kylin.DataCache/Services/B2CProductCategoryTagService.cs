using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 精品汇商品分类标签数据服务
    /// </summary>
    internal sealed class B2CProductCategoryTagService : IB2CProductCategoryTagService
    {
        public List<B2CProductCategoryTagCacheModel> GetAll()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.Mall_CategoryTag
                            select new B2CProductCategoryTagCacheModel
                            {
                                CategoryID = p.CategoryID,
                                OrderNo = p.OrderNo,
                                TagID = p.TagID,
                                TagName = p.TagName
                            };

                return query.ToList();
            }
        }
    }
}
