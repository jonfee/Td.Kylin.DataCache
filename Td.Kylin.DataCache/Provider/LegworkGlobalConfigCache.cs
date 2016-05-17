using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 跑腿业务全局配置缓存
    /// </summary>
    public class LegworkGlobalConfigCache : CacheItem<LegworkGlobalConfigCacheModel>
    {
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

        protected override List<LegworkGlobalConfigCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.LegworkGlobalConfigService.GetAll();
        }
    }
}
