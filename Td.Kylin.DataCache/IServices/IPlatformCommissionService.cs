using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.IServices
{
    /// <summary>
    /// 平台针对区域抽成服务接口
    /// </summary>
    internal interface IPlatformCommissionService
    {
        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        List<PlatformCommissionCacheModel> GetAll();
    }
}
