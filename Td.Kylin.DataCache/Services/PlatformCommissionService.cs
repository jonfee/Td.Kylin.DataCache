using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 平台针对区域抽成服务
    /// </summary>
    /// <typeparam name="DbContext"></typeparam>
    internal sealed class PlatformCommissionService<DbContext> : IPlatformCommissionService where DbContext : DataContext, new()
    {
        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        public List<PlatformCommissionCacheModel> GetAll()
        {
            using (var db = new DbContext())
            {
                var query = from p in db.Area_PlatformCommission
                            select new PlatformCommissionCacheModel
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
