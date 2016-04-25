using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 系统圈子缓存
    /// </summary>
    public sealed class ForumCircleCache : CacheItem<ForumCircleCacheModel>
    {
        public ForumCircleCache() : base(CacheItemType.ForumCircle) { }
        
        protected override List<ForumCircleCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.ForumCircleService.GetEnabledAll();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="forumID">圈子ID</param>
        /// <returns></returns>
        public ForumCircleCacheModel Get(long forumID)
        {
            var item = new ForumCircleCacheModel { ForumID = forumID };

            return Get(item.HashField);
        }
    }
}
