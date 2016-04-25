namespace Td.Kylin.DataCache.CacheModel
{
    /// <summary>
    /// 区域针对个人工作人员抽成配置缓存模型
    /// </summary>
    public class AreaForPersonalWorkerCommissionCacheModel:BaseCacheModel
    {
        /// <summary>
        /// HashField（AreaID+UserID+CommissionItem）
        /// </summary>
        public override string HashField
        {
            get
            {
                return string.Format("{0}{1}{2}", AreaID, UserID, CommissionItem);
            }
        }

        /// <summary>
        /// 区域ID
        /// </summary>
        public int AreaID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 抽成项（枚举：AreaWorkerCommissionOption，如：上门预约订单抽成）
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
