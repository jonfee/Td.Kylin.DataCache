using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 全局配置缓存
    /// </summary>
    public sealed class SystemGolbalConfigCache : CacheItem<SystemGolbalConfigCacheModel>
    {
        public SystemGolbalConfigCache() : base(CacheItemType.SystemGolbalConfig) { }
        
        protected override List<SystemGolbalConfigCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.SystemGolbalConfigService.GetAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="resourceType">资源类型</param>
        /// <param name="resourceKey">资源类型子项</param>
        /// <returns></returns>
        public SystemGolbalConfigCacheModel Get(int resourceType,int resourceKey)
        {
            var item = new SystemGolbalConfigCacheModel { ResourceType = resourceType,ResourceKey= resourceKey };

            return Get(item.HashField);
        }
    }
}
