using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.DataCache.CacheModel;
namespace Td.Kylin.DataCache.Provider
{
    public sealed class ApiModuleAuthorizeCache : CacheItem<ApiModuleAuthorizeCacheModel>
    {
        public ApiModuleAuthorizeCache() : base(CacheItemType.ApiModuleAuthorize) { }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="serverID">接口服务ID</param>
        /// <param name="moduleID">模块ID</param>
        /// <returns></returns>
        public ApiModuleAuthorizeCacheModel Get(string serverID, string moduleID)
        {
            var item = new ApiModuleAuthorizeCacheModel { ServerID = serverID, ModuleID = moduleID };

            return Get(item.HashField);
        }

        protected override List<ApiModuleAuthorizeCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.ModuleAuthorizeService.GetAll();
        }
    }
}
