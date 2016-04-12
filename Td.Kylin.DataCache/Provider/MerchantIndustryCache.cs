using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Redis;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 商家行业缓存
    /// </summary>
    internal sealed class MerchantIndustryCache : CacheItem<List<MerchantIndustryCacheModel>>
    {
        public MerchantIndustryCache() : base(CacheItemType.MerchantIndustry) { }

        protected override List<MerchantIndustryCacheModel> GetCache()
        {
            List<MerchantIndustryCacheModel> data = null;

            if (null != RedisDB)
            {
                data = RedisDB.HashGetAll<MerchantIndustryCacheModel>(CacheKey).Select(p => p.Value).ToList();
            }

            return data;
        }

        protected override List<MerchantIndustryCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.MerchantIndustryService.GetEnabledAll();
        }

        protected override void SetCache(List<MerchantIndustryCacheModel> data)
        {
            if (null != RedisDB)
            {
                //清除数据缓存
                RedisDB.KeyDelete(CacheKey);

                if (data == null) data = ReadDataFromDB();

                if (null != data && data.Count > 0)
                {

                    var dic = data.ToDictionary(k => (RedisValue)k.IndustryID, v => v);

                    RedisDB.HashSet(CacheKey, dic);
                }
            }
        }
    }
}
