namespace Td.Kylin.DataCache.CacheModel
{
    /// <summary>
    /// 平台针对区域抽成配置缓存模型
    /// </summary>
    public class PlatformCommissionCacheModel : BaseCacheModel
    {
        /// <summary>
        /// HashField（AreaID+CommissionItem）
        /// </summary>
        public override string HashField
        {
            get
            {
                return string.Format("{0}{1}", AreaID, CommissionItem);
            }
        }

        /// <summary>
        /// 区域ID
        /// </summary>
        public int AreaID { get; set; }

        /// <summary>
        /// 抽成项（如：B2C订单金额抽成|商家订单交易佣金抽成）
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
