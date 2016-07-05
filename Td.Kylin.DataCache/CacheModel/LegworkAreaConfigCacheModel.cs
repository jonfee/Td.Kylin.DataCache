﻿namespace Td.Kylin.DataCache.CacheModel
{
    /// <summary>
    /// 跑腿区域配置数据缓存模型
    /// </summary>
    public class LegworkAreaConfigCacheModel : BaseCacheModel
    {
        /// <summary>
        /// HashField（AreaID）
        /// </summary>
        public override string HashField
        {
            get
            {
                return AreaID.ToString();
            }
        }

        /// <summary>
		/// 区域ID。
		/// </summary>
		public int AreaID
        {
            get;
            set;
        }

        /// <summary>
        /// 业务须知。
        /// </summary>
        public string Instructions
        {
            get;
            set;
        }

        /// <summary>
        /// 开通跑腿业务的行政区域。
        /// </summary>
        public string OpenAreas
        {
            get;
            set;
        }
    }
}
