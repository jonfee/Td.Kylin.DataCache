using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 用户经验值规则配置缓存
    /// </summary>
    public sealed class UserEmpiricalConfigCache : CacheItem<UserEmpiricalConfigCacheModel>
    {
        public UserEmpiricalConfigCache() : base(CacheItemType.UserEmpiricalConfig) { }
        
        protected override List<UserEmpiricalConfigCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.UserEmpiricalConfigService.GetAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="activityType">用户业务活动类型</param>
        /// <returns></returns>
        public UserEmpiricalConfigCacheModel Get(int activityType)
        {
            var item = new UserEmpiricalConfigCacheModel { ActivityType = activityType };

            return Get(item.HashField);
        }
    }
}
