using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 系统区域缓存
    /// </summary>
    public sealed class SystemAreaCache : CacheItem<SystemAreaCacheModel>
    {
        public SystemAreaCache() : base(CacheItemType.SystemArea) { }
        
        protected override List<SystemAreaCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.SystemAreaService.GetAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <returns></returns>
        public SystemAreaCacheModel Get(int areaID)
        {
            var item = new SystemAreaCacheModel { AreaID = areaID };

            return Get(item.HashField);
        }
    }
}
