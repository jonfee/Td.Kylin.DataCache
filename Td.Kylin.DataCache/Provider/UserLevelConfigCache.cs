using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

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
            return new UserLevelConfigService().GetEnabledAll();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="levelID">等级ID</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public UserLevelConfigCacheModel Get(long levelID, bool allScope = true)
        {
            var item = new UserLevelConfigCacheModel { LevelID = levelID };

            return Get(item.HashField, allScope);
        }
    }
}
