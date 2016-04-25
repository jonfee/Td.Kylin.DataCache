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

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <param name="commissionItem">抽成项（如：B2C订单金额抽成|商家订单交易佣金抽成）</param>
        /// <returns></returns>
        public PlatformCommissionCacheModel Get(int areaID, int commissionItem)
        {
            var item = new PlatformCommissionCacheModel { AreaID = areaID, CommissionItem = commissionItem };

            return Get(item.HashField);
        }

        protected override List<PlatformCommissionCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.PlatformCommissionService.GetAll();
        }
    }
}
