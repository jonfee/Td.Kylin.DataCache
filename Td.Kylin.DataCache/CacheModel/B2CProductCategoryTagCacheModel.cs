namespace Td.Kylin.DataCache.CacheModel
{
    /// <summary>
    /// 精品汇自定义分类下标签数据缓存模型
    /// </summary>
    public class B2CProductCategoryTagCacheModel:BaseCacheModel
    {
        /// <summary>
        /// HashField（TagID）
        /// </summary>
        public override string HashField
        {
            get
            {
                return TagID.ToString();
            }
        }

        ///<summary>
        ///标签ID
        ///</summary>
        public long TagID { get; set; }

        ///<summary>
        ///商品类目ID
        ///</summary>
        public long CategoryID { get; set; }

        ///<summary>
        ///商品标签名称
        ///</summary>
        public string TagName { get; set; }

        ///<summary>
        ///排序值
        ///</summary>
        public int OrderNo { get; set; }
    }
}
