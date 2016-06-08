﻿namespace Td.Kylin.DataCache.CacheModel
{
    /// <summary>
    /// 区域行业推荐缓存模型
    /// </summary>
    public class AreaRecommendIndustryCacheModel:BaseCacheModel
    {
        /// <summary>
        /// HashField（由“AreaID_IndustryID”组成）
        /// </summary>
        public override string HashField
        {
            get
            {
                return string.Format("{0}{1}", AreaID, IndustryID);
            }
        }

        /// <summary>
        /// 区域ID
        /// </summary>
        public int AreaID { get; set; }

        /// <summary>
        /// 行业ID
        /// </summary>
        public long IndustryID { get; set; }

        /// <summary>
        /// 行业父级ID
        /// </summary>
        public long ParentID { get; set; }

        ///<summary>
        ///排序
        ///</summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// 推荐状态（枚举：AreaIndustryRecommend，以2的N次方值和存储）
        /// </summary>
        public int RecommendType { get; set; }

        /// <summary>
        /// 行业名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ICON
        /// </summary>
        public string Icon { get; set; }
    }
}
