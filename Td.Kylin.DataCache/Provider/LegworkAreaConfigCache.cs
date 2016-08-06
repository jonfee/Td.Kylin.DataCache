using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 跑腿区域配置缓存
    /// </summary>
    public class LegworkAreaConfigCache : CacheItem<LegworkAreaConfigCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="LegworkAreaConfigCache"/>实例
        /// </summary>
        public LegworkAreaConfigCache() : base(CacheItemType.LegworkAreaConfig) { }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public LegworkAreaConfigCacheModel Get(int areaID, bool allScope = true)
        {
            var item = new LegworkAreaConfigCacheModel { AreaID = areaID };

            return Get(item.HashField,allScope);
        }

        /// <summary>
        /// 从数据库读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<LegworkAreaConfigCacheModel> ReadDataFromDB()
        {
            return new LegworkAreaConfigService().GetAll();
        }
    }
}
