namespace Td.Kylin.DataCache.CacheModel
{
    /// <summary>
    /// 用户等级规则数据缓存模型
    /// </summary>
    public class UserLevelConfigCacheModel:BaseCacheModel
    {
        /// <summary>
        /// HashField（LevelID）
        /// </summary>
        public override string HashField
        {
            get
            {
                return LevelID.ToString();
            }
        }

        ///<summary>
        ///数据ID
        ///</summary>
        public long LevelID { get; set; }

        ///<summary>
        ///等级名称
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///最小经验值
        ///</summary>
        public int Min { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
    }
}
