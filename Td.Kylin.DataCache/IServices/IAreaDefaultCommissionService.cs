using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.IServices
{
    /// <summary>
    /// 区域默认抽成配置
    /// </summary>
    internal interface IAreaDefaultCommissionService
    {
        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        List<AreaDefaultCommissionCacheModel> GetAll();
    }
}
