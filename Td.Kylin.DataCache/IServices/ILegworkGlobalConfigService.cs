using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.IServices
{
    internal interface ILegworkGlobalConfigService
    {
        /// <summary>
        /// 获取所有跑腿业务全局配置
        /// </summary>
        /// <returns></returns>
        List<LegworkGlobalConfigCacheModel> GetAll();
    }
}
