using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.IServices
{
    /// <summary>
    /// 区域针对商家抽成配置服务接口
    /// </summary>
    internal interface IAreaForMerchantCommissionService
    {
        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        List<AreaForMerchantCommissionCacheModel> GetAll();
    }
}
