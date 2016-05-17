namespace Td.Kylin.DataCache.CacheModel
{
    /// <summary>
    /// 接口模块授权数据缓存模型
    /// </summary>
    public class ApiModuleAuthorizeCacheModel : BaseCacheModel
    {
        /// <summary>
        /// HastField（ ServerID+ModuleID）
        /// </summary>
        public override string HashField
        {
            get
            {
                return string.Format("{0}{1}", ServerID, ModuleID);
            }
        }

        ///<summary>
        ///服务系统编号（系统硬编码代号）
        ///</summary>
        public string ServerID { get; set; }

        ///<summary>
        ///模块编号(系统硬编码代号)
        ///</summary>
        public string ModuleID { get; set; }

        ///<summary>
        ///密钥
        ///</summary>
        public string AppSecret { get; set; }

        ///<summary>
        ///角色(1管理员，2编辑者，4使用者）
        ///</summary>
        public int Role { get; set; }
    }
}
