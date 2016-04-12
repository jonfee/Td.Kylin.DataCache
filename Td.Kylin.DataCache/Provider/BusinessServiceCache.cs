using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    internal sealed class BusinessServiceCache : CacheItem<List<BusinessServiceCacheModel>>
    {
        public BusinessServiceCache() : base(CacheItemType.BusinessServices) { }

        protected override List<BusinessServiceCacheModel> GetCache()
        {
            List<BusinessServiceCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<BusinessServiceCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<BusinessServiceCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.BusinessService.GetEnabledAll();
        }

        protected override void SetCache(List<BusinessServiceCacheModel> data)
        {
            if (null != RedisDB)
            {
                //清除数据缓存
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {

                    var dic = data.ToDictionary(k => (RedisValue)k.BusinessID, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }
    }
}
