using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 区域针对商家抽成配置服务
    /// </summary>
    /// <typeparam name="DbContext"></typeparam>
    internal sealed class AreaForMerchantCommissionService<DbContext> : IAreaForMerchantCommissionService where DbContext : DataContext, new()
    {
        public List<AreaForMerchantCommissionCacheModel> GetAll()
        {
            using (var db = new DbContext())
            {
                var query = from p in db.Commission_OperatorFromMerchant
                            select new AreaForMerchantCommissionCacheModel
                            {
                                AreaID = p.AreaID,
                                CommissionItem = p.CommissionItem,
                                CommissionType = p.CommissionType,
                                MerchantID = p.MerchantID,
                                Value = p.Value
                            };

                return query.ToList();
            }
        }
    }
}
