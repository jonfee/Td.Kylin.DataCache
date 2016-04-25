using System;

namespace Td.Kylin.DataCache.CacheModel
{
    /// <summary>
    /// 区域针对商家抽成配置缓存模型
    /// </summary>
    public class AreaForMerchantCommissionCacheModel : BaseCacheModel
    {
        /// <summary>
        /// HashField（AreaID+MerchantID+CommissionItem）
        /// </summary>
        public override string HashField
        {
            get
            {
                return string.Format("{0}{1}{2}", AreaID, MerchantID, CommissionItem);
            }
        }

        /// <summary>
        /// 区域ID
        /// </summary>
        public int AreaID { get; set; }

        /// <summary>
        /// 商家ID
        /// </summary>
        public long MerchantID { get; set; }

        /// <summary>
        /// 抽成项（枚举：AreaMerchantCommissionOption，如：商家商品订单抽成|商家上门预约订单抽成）
        /// </summary>
        public int CommissionItem { get; set; }

        /// <summary>
        /// 抽成方式（如：按金额百分比|按固定金额）
        /// </summary>
        public int CommissionType { get; set; }

        /// <summary>
        /// 抽成结果
        /// </summary>
        public decimal Value { get; set; }
    }
}
