using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    internal sealed class ModuleAuthorizeService<DbContext> : IModuleAuthorizeService where DbContext : DataContext, new()
    {
        public List<ApiModuleAuthorizeCacheModel> GetAll()
        {
            using (var db = new DbContext())
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
