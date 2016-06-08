﻿namespace Td.Kylin.DataCache.CacheModel
{
    /// <summary>
    /// 商家行业缓存模型
    /// </summary>
    public class MerchantIndustryCacheModel:BaseCacheModel
    {
        /// <summary>
        /// HashField（IndustryID）
        /// </summary>
        public override string HashField
        {
            get
            {
                return IndustryID.ToString();
            }
        }

        ///<summary>
        ///行业ID
        ///</summary>
        public long IndustryID { get; set; }

        ///<summary>
        ///行业名称
        ///</summary>
        public string Name { get; set; }

        /// <summary>
        /// 行业父级ID
        /// </summary>
        public long ParentID { get; set; }

        /// <summary>
        /// 行业层级（如：1｜2）
        /// </summary>
        public string Layer { get; set; }

        ///<summary>
        ///排序
        ///</summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
    }
}
