using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;


namespace Td.Kylin.DataCache.Services
{
    internal sealed class LegworkAreaConfigService<DbContext> : ILegworkAreaConfigService where DbContext : DataContext, new()
    {
        public List<LegworkAreaConfigCacheModel> GetAll()
        {
            using (var db = new DbContext())
            {
                return (from p in db.Legwork_AreaConfig
                        select new LegworkAreaConfigCacheModel
                        {
                            AreaID = p.AreaID,
                            Instructions = p.Instructions,
                            OpenAreas = p.OpenAreas
                        }).ToList();
            }
        }
    }
}
