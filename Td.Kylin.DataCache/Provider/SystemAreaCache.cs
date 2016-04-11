using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 系统区域缓存
    /// </summary>
    internal sealed class SystemAreaCache : CacheItem<List<SystemAreaCacheModel>>
    {
        public SystemAreaCache() : base(CacheItemType.SystemArea) { }

        protected override List<SystemAreaCacheModel> GetCache()
        {
            List<SystemAreaCacheModel> data = null;

            try
            {
                if (null != RedisDB)
                {
                    data = RedisDB.HashGetAll<SystemAreaCacheModel>(CacheKey).Select(p => p.Value).ToList();
                }
            }
            catch
            {
                data = ReadDataFromDB();
            }

            return data;
        }

        protected override List<SystemAreaCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.SystemAreaService.GetAll();
        }

        protected override void SetCache(List<SystemAreaCacheModel> data)
        {
            if (null != RedisDB)
            {
                //清除数据缓存
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {

                    var dic = data.ToDictionary(k => (RedisValue)k.AreaID, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }
    }
}
