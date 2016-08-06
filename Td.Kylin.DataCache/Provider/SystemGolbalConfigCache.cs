using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 全局配置缓存
    /// </summary>
    public sealed class SystemGolbalConfigCache : CacheItem<SystemGolbalConfigCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="SystemGolbalConfigCache"/>实例
        /// </summary>
        public SystemGolbalConfigCache() : base(CacheItemType.SystemGolbalConfig) { }

        /// <summary>
        /// 从数据库读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<SystemGolbalConfigCacheModel> ReadDataFromDB()
        {
            return new SystemGolbalConfigService().GetAll();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="resourceType">资源类型</param>
        /// <param name="resourceKey">资源类型子项</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public SystemGolbalConfigCacheModel Get(int resourceType, int resourceKey, bool allScope = true)
        {
            var item = new SystemGolbalConfigCacheModel { ResourceType = resourceType, ResourceKey = resourceKey };

            return Get(item.HashField, allScope);
        }
    }
}
