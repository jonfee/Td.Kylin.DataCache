using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 用户积分规则配置缓存
    /// </summary>
    internal sealed class UserPointsConfigCache : CacheItem<List<UserPointsConfigCacheModel>>
    {
        public UserPointsConfigCache() : base(CacheItemType.UserPointsConfig) { }

        protected override List<UserPointsConfigCacheModel> GetCache()
        {
            List<UserPointsConfigCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<UserPointsConfigCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<UserPointsConfigCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.UserPointsConfigService.GetAll();
        }

        protected override void SetCache(List<UserPointsConfigCacheModel> data)
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
