using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 系统全局配置数据服务
    /// </summary>
    internal sealed class SystemGolbalConfigService : ISystemGolbalConfigService
    {
        public List<SystemGolbalConfigCacheModel> GetAll()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.System_GlobalResources
                            select new SystemGolbalConfigCacheModel
                            {
                                Name = p.Name,
                                ResourceKey = p.ResourceKey,
                                ResourceType = p.ResourceType,
                                Value = p.Value,
                                ValueUnit = p.ValueUnit
                            };

                return query.ToList();
            }
        }
    }
}
