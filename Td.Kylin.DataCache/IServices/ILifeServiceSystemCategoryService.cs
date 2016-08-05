using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.Entity;

namespace Td.Kylin.DataCache.IServices
{
    internal interface ILifeServiceSystemCategoryService
    {
        /// <summary>
        /// 获取所有有效的生活服务分类集合
        /// </summary>
        /// <returns></returns>
        List<LifeServiceSystemCategoryCacheModel> GetEnabledAll();
    }
}
