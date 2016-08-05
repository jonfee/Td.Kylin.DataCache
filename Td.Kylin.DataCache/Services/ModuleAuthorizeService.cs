using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 模块服务授权数据服务
    /// </summary>
    internal sealed class ModuleAuthorizeService : IModuleAuthorizeService
    {
        public List<ApiModuleAuthorizeCacheModel> GetAll()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.System_ModuleAuthorize
                            select new ApiModuleAuthorizeCacheModel
                            {
                                AppSecret = p.AppSecret,
                                ModuleID = p.ModuleID,
                                Role = p.Role,
                                ServerID = p.ServerID
                            };

                return query.ToList();
            }
        }
    }
}
