using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// API模块授权缓存
    /// </summary>
    public sealed class ApiModuleAuthorizeCache : CacheItem<ApiModuleAuthorizeCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="ApiModuleAuthorizeCache"/>实例
        /// </summary>
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

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<ApiModuleAuthorizeCacheModel> ReadDataFromDB()
        {
            return new ModuleAuthorizeService().GetAll();
        }
    }
}
