using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 区域针对个人服务人员抽成配置缓存
    /// </summary>
    public sealed class AreaForPersonalWorkerCommissionCache : CacheItem<AreaForPersonalWorkerCommissionCacheModel>
    {
        public AreaForPersonalWorkerCommissionCache() : base(CacheItemType.AreaForPersonalWorkerCommission) { }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="commissionItem">抽成项（枚举：AreaWorkerCommissionOption，如：上门预约订单抽成）</param>
        /// <returns></returns>
        public AreaForPersonalWorkerCommissionCacheModel Get(int areaID, long userID, int commissionItem)
        {
            var item = new AreaForPersonalWorkerCommissionCacheModel { AreaID = areaID, UserID = userID, CommissionItem = commissionItem };

            return Get(item.HashField);
        }

        protected override List<AreaForPersonalWorkerCommissionCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.AreaForPersonalWorkerCommissionService.GetAll();
        }
    }
}
