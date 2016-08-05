using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 开通区域数据缓存
    /// </summary>
    public sealed class OpenAreaCache : CacheItem<OpenAreaCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="OpenAreaCache"/>实例
        /// </summary>
        public OpenAreaCache() : base(CacheItemType.OpenArea) { }
        
        /// <summary>
        /// 从数据库读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<OpenAreaCacheModel> ReadDataFromDB()
        {
            return new OpenAreaService().GetAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <returns></returns>
        public OpenAreaCacheModel Get(int areaID)
        {
            var item = new OpenAreaCacheModel { AreaID = areaID };

            return Get(item.HashField);
        }
    }
}
