namespace Td.Kylin.DataCache.CacheModel
{
    public class LegworkGoodsCategoryCacheModel : BaseCacheModel
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
		/// 分类ID。
		/// </summary>
		public long CategoryID
        {
            get;
            set;
        }

        /// <summary>
        /// 分类名称。
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 排列顺序。
        /// </summary>
        public int SortOrder
        {
            get;
            set;
        }
    }
}
