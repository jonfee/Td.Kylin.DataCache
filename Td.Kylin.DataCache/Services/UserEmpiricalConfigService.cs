using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 用户经验值规则配置数据服务
    /// </summary>
    internal sealed class UserEmpiricalConfigService : IUserEmpiricalConfigService
    {
        public List<UserEmpiricalConfigCacheModel> GetAll()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.System_EmpiricalConfig
                            select new UserEmpiricalConfigCacheModel
                            {
                                ActivityType = p.ActivityType,
                                MaxLimit = p.MaxLimit,
                                MaxUnit = p.MaxUnit,
                                Repeatable = p.Repeatable,
                                Score = p.Score
                            };

                return query.ToList();
            }
        }
    }
}
