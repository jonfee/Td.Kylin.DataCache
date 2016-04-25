using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 区域针对个人工作人员抽成配置服务
    /// </summary>
    /// <typeparam name="DbContext"></typeparam>
    internal sealed class AreaForPersonalWorkerCommissionService<DbContext> : IAreaForPersonalWorkerCommissionService where DbContext : DataContext, new()
    {
        public List<AreaForPersonalWorkerCommissionCacheModel> GetAll()
        {
            using (var db = new DbContext())
            {
                var query = from p in db.Commission_OperatorFromWorker
                            select new AreaForPersonalWorkerCommissionCacheModel
                            {
                                AreaID = p.AreaID,
                                CommissionItem = p.CommissionItem,
                                CommissionType = p.CommissionType,
                                UserID = p.UserID,
                                Value = p.Value
                            };

                return query.ToList();
            }
        }
    }
}
