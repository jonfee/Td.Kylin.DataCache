using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    internal sealed class UserLevelConfigCache : CacheItem<List<UserLevelConfigCacheModel>>
    {
        public UserLevelConfigCache() : base(CacheItemType.UserLevelConfig) { }

        protected override List<UserLevelConfigCacheModel> GetCache()
        {
            List<UserLevelConfigCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<UserLevelConfigCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<UserLevelConfigCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.UserLevelConfigService.GetEnabledAll();
        }

        protected override void SetCache(List<UserLevelConfigCacheModel> data)
        {
            if (null != RedisDB)
            {
                //清除数据缓存
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {

                    var dic = data.ToDictionary(k => (RedisValue)k.LevelID, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }
    }
}
