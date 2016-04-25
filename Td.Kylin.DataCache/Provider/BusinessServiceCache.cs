using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 上门预约业务缓存
    /// </summary>
    public sealed class BusinessServiceCache : CacheItem<BusinessServiceCacheModel>
    {
        public BusinessServiceCache() : base(CacheItemType.BusinessServices) { }
        
        protected override List<BusinessServiceCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.BusinessService.GetEnabledAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="businessID">业务ID</param>
        /// <returns></returns>
        public BusinessServiceCacheModel Get(long businessID)
        {
            var item = new BusinessServiceCacheModel { BusinessID = businessID };

            return Get(item.HashField);
        }
    }
}
