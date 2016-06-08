using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 区域行业推荐数据服务
    /// </summary>
    /// <typeparam name="DbContext"></typeparam>
    internal sealed class AreaRecommendIndustryService<DbContext> : IAreaRecommendIndustryService where DbContext : DataContext, new()
    {
        /// <summary>
        /// 获取所有区域推荐行业集合
        /// </summary>
        /// <returns></returns>
        public List<AreaRecommendIndustryCacheModel> GetAll()
        {
            using (var db = new DbContext())
            {
                var query = from p in db.Area_RecommendIndustry
                            join i in db.Merchant_Industry
                            on p.IndustryID equals i.IndustryID
                            select new AreaRecommendIndustryCacheModel
                            {
                                AreaID = p.AreaID,
                                IndustryID = p.IndustryID,
                                OrderNo = p.OrderNo,
                                ParentID = p.ParentID,
                                RecommendType = p.RecommendType,
                                Name = i.Name,
                                Icon = i.Icon
                            };

                return query.ToList();
            }
        }
    }
}
