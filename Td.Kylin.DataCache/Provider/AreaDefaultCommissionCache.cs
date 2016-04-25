using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 区域默认抽成配置缓存
    /// </summary>
    public sealed class AreaDefaultCommissionCache : CacheItem<AreaDefaultCommissionCacheModel>
    {
        public AreaDefaultCommissionCache() : base(CacheItemType.AreaDefaultCommission) { }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <param name="commissionItem">抽成项（枚举：AreaDefaultCommissionOption，如：商家商品订单抽成|商家上门预约订单抽成）</param>
        /// <returns></returns>
        public AreaDefaultCommissionCacheModel Get(int areaID,int commissionItem)
        {
            var item = new AreaDefaultCommissionCacheModel { AreaID = areaID, CommissionItem = commissionItem };

            return Get(item.HashField);
        }

        protected override List<AreaDefaultCommissionCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.AreaDefaultCommissionService.GetAll();
        }
    }
}
