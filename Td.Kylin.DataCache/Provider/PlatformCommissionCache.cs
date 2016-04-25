using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 平台针对区域抽成缓存
    /// </summary>
    public sealed class PlatformCommissionCache : CacheItem<PlatformCommissionCacheModel>
    {
        public PlatformCommissionCache() : base(CacheItemType.PlatformCommission) { }

        public PlatformCommissionCacheModel Get(int areaID)
        {
            var item = new PlatformCommissionCacheModel { AreaID = areaID };

            return Get(item.HashField);
        }

        protected override List<PlatformCommissionCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.PlatformCommissionService.GetAll();
        }
    }
}
