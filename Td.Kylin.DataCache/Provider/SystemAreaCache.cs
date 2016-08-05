using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 系统区域缓存
    /// </summary>
    public sealed class SystemAreaCache : CacheItem<SystemAreaCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="SystemAreaCache"/>实例
        /// </summary>
        public SystemAreaCache() : base(CacheItemType.SystemArea) { }

        /// <summary>
        /// 从数据库读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<SystemAreaCacheModel> ReadDataFromDB()
        {
            return new SystemAreaService().GetAll();
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
