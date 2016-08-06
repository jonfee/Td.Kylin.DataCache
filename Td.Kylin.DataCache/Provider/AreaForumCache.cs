using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 区域圈子缓存
    /// </summary>
    public sealed class AreaForumCache : CacheItem<AreaForumCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="AreaForumCache"/>实例
        /// </summary>
        public AreaForumCache() : base(CacheItemType.AreaForum) { }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="areaForumID">区域圈子ID</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public AreaForumCacheModel Get(long areaForumID, bool allScope = true)
        {
            var item = new AreaForumCacheModel { AreaForumID = areaForumID };

            return Get(item.HashField, allScope);
        }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<AreaForumCacheModel> ReadDataFromDB()
        {
            return new AreaForumService().GetEnabledAll();
        }
    }
}
