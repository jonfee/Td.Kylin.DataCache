using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 用户积分规则配置缓存
    /// </summary>
    public sealed class UserPointsConfigCache : CacheItem<UserPointsConfigCacheModel>
    {
        public UserPointsConfigCache() : base(CacheItemType.UserPointsConfig) { }
        
        protected override List<UserPointsConfigCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.UserPointsConfigService.GetAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="activityType">用户业务活动类型</param>
        /// <returns></returns>
        public UserPointsConfigCacheModel Get(int activityType)
        {
            var item = new UserPointsConfigCacheModel { ActivityType = activityType };

            return Get(item.HashField);
        }
    }
}
