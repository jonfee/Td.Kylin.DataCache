using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 区域针对商家抽成配置缓存
    /// </summary>
    public sealed class AreaForMerchantCommissionCache : CacheItem<AreaForMerchantCommissionCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="AreaForMerchantCommissionCache"/>实例
        /// </summary>
        public AreaForMerchantCommissionCache() : base(CacheItemType.AreaForMerchantCommission) { }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <param name="merchantID">商家ID</param>
        /// <param name="commissionItem">抽成项（枚举：AreaMerchantCommissionOption，如：商家商品订单抽成|商家上门预约订单抽成）</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public AreaForMerchantCommissionCacheModel Get(int areaID, long merchantID, int commissionItem, bool allScope = true)
        {
            var item = new AreaForMerchantCommissionCacheModel { AreaID = areaID, MerchantID = merchantID, CommissionItem = commissionItem };

            return Get(item.HashField, allScope);
        }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<AreaForMerchantCommissionCacheModel> ReadDataFromDB()
        {
            return new AreaForMerchantCommissionService().GetAll();
        }
    }
}
