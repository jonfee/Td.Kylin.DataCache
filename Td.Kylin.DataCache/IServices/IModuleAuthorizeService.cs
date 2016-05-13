using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.IServices
{
    internal  interface IModuleAuthorizeService
    {
        /// <summary>
        /// 获取所有模块授权
        /// </summary>
        /// <returns></returns>
        List<ApiModuleAuthorizeCacheModel> GetAll();
    }
}
