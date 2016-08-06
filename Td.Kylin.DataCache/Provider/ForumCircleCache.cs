using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 系统圈子缓存
    /// </summary>
    public sealed class ForumCircleCache : CacheItem<ForumCircleCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="ForumCircleCache"/>实例
        /// </summary>
        public ForumCircleCache() : base(CacheItemType.ForumCircle) { }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<ForumCircleCacheModel> ReadDataFromDB()
        {
            return new ForumCircleService().GetEnabledAll();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="forumID">圈子ID</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public ForumCircleCacheModel Get(long forumID, bool allScope = true)
        {
            var item = new ForumCircleCacheModel { ForumID = forumID };

            return Get(item.HashField,allScope);
        }
    }
}
