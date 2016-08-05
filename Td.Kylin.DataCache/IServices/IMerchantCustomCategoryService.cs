using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.IServices
{
    /// <summary>
    /// 商家自定义分类数据服务接口
    /// </summary>
    internal interface IMerchantCustomCategoryService
    {
        /// <summary>
        /// 获取所有有效的分类集合
        /// </summary>
        /// <returns></returns>
        List<MerchantCustomCategoryCacheModel> GetEnabledAll();
    }
}
