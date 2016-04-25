﻿using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 开通区域数据缓存
    /// </summary>
    public sealed class OpenAreaCache : CacheItem<OpenAreaCacheModel>
    {
        public OpenAreaCache() : base(CacheItemType.OpenArea) { }
        
        protected override List<OpenAreaCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.OpenAreaService.GetAll();
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
