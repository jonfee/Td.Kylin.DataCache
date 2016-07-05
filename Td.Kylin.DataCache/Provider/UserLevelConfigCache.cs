using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 用户等级规则配置缓存
    /// </summary>
    public sealed class UserLevelConfigCache : CacheItem<UserLevelConfigCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="UserLevelConfigCache"/>实例
        /// </summary>
        public UserLevelConfigCache() : base(CacheItemType.UserLevelConfig) { }

        /// <summary>
        /// 从数据库读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<UserLevelConfigCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.UserLevelConfigService.GetEnabledAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="levelID">等级ID</param>
        /// <returns></returns>
        public UserLevelConfigCacheModel Get(long levelID)
        {
            var item = new UserLevelConfigCacheModel { LevelID = levelID };

            return Get(item.HashField);
        }
    }
}
