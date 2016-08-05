namespace Td.Kylin.DataCache.CacheModel
{
    /// <summary>
    /// 商家自定义分类
    /// </summary>
    public class MerchantCustomCategoryCacheModel:BaseCacheModel
    {
        /// <summary>
        /// HashField（CategoryID）
        /// </summary>
        public override string HashField
        {
            get
            {
                return CategoryID.ToString();
            }
        }

        /// <summary>
        /// 自定义分类ID
        /// </summary>
        public long CategoryID { get; set; }

        /// <summary>
        /// 自定义分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商家ID
        /// </summary>
        public long MerchantID { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int OrderNo { get; set; }
    }
}
