using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 平台针对区域抽成缓存
    /// </summary>
    public sealed class PlatformCommissionCache : CacheItem<PlatformCommissionCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="PlatformCommissionCache"/>实例
        /// </summary>
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

        /// <summary>
        /// 从数据库读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<PlatformCommissionCacheModel> ReadDataFromDB()
        {
            return new PlatformCommissionService().GetAll();
        }
    }
}
