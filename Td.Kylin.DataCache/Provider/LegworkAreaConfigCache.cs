using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    public class LegworkAreaConfigCache : CacheItem<LegworkAreaConfigCacheModel>
    {
        public LegworkAreaConfigCache() : base(CacheItemType.LegworkAreaConfig) { }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <returns></returns>
        public LegworkAreaConfigCacheModel Get(int areaID)
        {
            var item = new LegworkAreaConfigCacheModel { AreaID = areaID };

            return Get(item.HashField);
        }

        protected override List<LegworkAreaConfigCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.LegworkAreaConfigService.GetAll();
        }
    }
}
