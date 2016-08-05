using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 跑腿业务全局配置缓存
    /// </summary>
    public class LegworkGlobalConfigCache : CacheItem<LegworkGlobalConfigCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="LegworkGlobalConfigCache"/>实例
        /// </summary>
        public LegworkGlobalConfigCache() : base(CacheItemType.LegworkGlobalConfig) { }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="globalConfigID">配置ID</param>
        /// <returns></returns>
        public LegworkGlobalConfigCacheModel Get(long globalConfigID)
        {
            var item = new LegworkGlobalConfigCacheModel { GlobalConfigID = globalConfigID };

            return Get(item.HashField);
        }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<LegworkGlobalConfigCacheModel> ReadDataFromDB()
        {
            return new LegworkGlobalConfigService().GetAll();
        }
    }
}
