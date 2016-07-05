namespace Td.Kylin.DataCache.CacheModel
{
    /// <summary>
    /// 跑腿全局配置数据缓存模型
    /// </summary>
    public class LegworkGlobalConfigCacheModel : BaseCacheModel
    {
        /// <summary>
        /// HashField（GlobalConfigID）
        /// </summary>
        public override string HashField
        {
            get
            {
                return GlobalConfigID.ToString();
            }
        }

        /// <summary>
        /// 全局配置ID
        /// </summary>
        public long GlobalConfigID { get; set; }

        /// <summary>
        /// 等待报价时间。
        /// </summary>
        public short QuotationWaitingTime
        {
            get;
            set;
        }

        /// <summary>
        /// 等待报价人数。
        /// </summary>
        public short QuotationWaitingWorkers
        {
            get;
            set;
        }

        /// <summary>
        /// 报价超时时间。
        /// </summary>
        public short QuotationWaitingTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// 接单超时时间。
        /// </summary>
        public short OrderTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// 支付超时时间。
        /// </summary>
        public short PaymentTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// 自动确认收货时间。
        /// </summary>
        public short AutoConfirmTime
        {
            get;
            set;
        }
    }
}
