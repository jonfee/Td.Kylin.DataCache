using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 用户积分规则配置数据服务
    /// </summary>
    internal sealed class UserPointsConfigService : IUserPointsConfigService
    {
        public List<UserPointsConfigCacheModel> GetAll()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.System_PointsConfig
                            select new UserPointsConfigCacheModel
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
