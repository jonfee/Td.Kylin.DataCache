using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.IServices
{
    internal interface ILegworkAreaConfigService
    {
        /// <summary>
        /// 获取所有跑腿业务区域配置
        /// </summary>
        /// <returns></returns>
        List<LegworkAreaConfigCacheModel> GetAll();
    }
}
