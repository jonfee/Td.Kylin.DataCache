using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 用户积分规则配置缓存
    /// </summary>
    public sealed class UserPointsConfigCache : CacheItem<UserPointsConfigCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="UserPointsConfigCache"/>实例
        /// </summary>
        public UserPointsConfigCache() : base(CacheItemType.UserPointsConfig) { }

        /// <summary>
        /// 从数据库读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<UserPointsConfigCacheModel> ReadDataFromDB()
        {
            return new UserPointsConfigService().GetAll();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="activityType">用户业务活动类型</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public UserPointsConfigCacheModel Get(int activityType, bool allScope = true)
        {
            var item = new UserPointsConfigCacheModel { ActivityType = activityType };

            return Get(item.HashField, allScope);
        }
    }
}
