using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 用户经验值规则配置缓存
    /// </summary>
    public sealed class UserEmpiricalConfigCache : CacheItem<UserEmpiricalConfigCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="UserEmpiricalConfigCache"/>实例
        /// </summary>
        public UserEmpiricalConfigCache() : base(CacheItemType.UserEmpiricalConfig) { }

        /// <summary>
        /// 从数据库读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<UserEmpiricalConfigCacheModel> ReadDataFromDB()
        {
            return new UserEmpiricalConfigService().GetAll();
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
