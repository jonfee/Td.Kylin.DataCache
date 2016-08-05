using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 区域默认抽成数据服务
    /// </summary>
    internal sealed class AreaDefaultCommissionService : IAreaDefaultCommissionService
    {
        public List<AreaDefaultCommissionCacheModel> GetAll()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.Commission_OperatorDefault
                            select new AreaDefaultCommissionCacheModel
                            {
                                AreaID = p.AreaID,
                                CommissionItem = p.CommissionItem,
                                CommissionType = p.CommissionType,
                                Value = p.Value
                            };

                return query.ToList();
            }
        }
    }
}
