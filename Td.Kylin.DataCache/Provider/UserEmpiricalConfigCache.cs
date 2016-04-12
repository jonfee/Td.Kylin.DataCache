using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    internal sealed class UserEmpiricalConfigCache : CacheItem<List<UserEmpiricalConfigCacheModel>>
    {
        public UserEmpiricalConfigCache() : base(CacheItemType.UserEmpiricalConfig) { }

        protected override List<UserEmpiricalConfigCacheModel> GetCache()
        {
            List<UserEmpiricalConfigCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<UserEmpiricalConfigCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<UserEmpiricalConfigCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.UserEmpiricalConfigService.GetAll();
        }

        protected override void SetCache(List<UserEmpiricalConfigCacheModel> data)
        {
            if (null != RedisDB)
            {
                //清除数据缓存
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {

                    var dic = data.ToDictionary(k => (RedisValue)k.ActivityType, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }
    }
}
