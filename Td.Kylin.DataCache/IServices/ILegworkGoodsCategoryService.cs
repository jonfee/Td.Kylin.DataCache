using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.IServices
{
    internal interface ILegworkGoodsCategoryService
    {
        /// <summary>
        /// 获取所有跑腿业务物品类型
        /// </summary>
        /// <returns></returns>
        List<LegworkGoodsCategoryCacheModel> GetAll();
    }
}
