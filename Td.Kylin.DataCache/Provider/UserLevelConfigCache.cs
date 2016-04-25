using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 用户等级规则配置缓存
    /// </summary>
    public sealed class UserLevelConfigCache : CacheItem<UserLevelConfigCacheModel>
    {
        public UserLevelConfigCache() : base(CacheItemType.UserLevelConfig) { }
        
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
