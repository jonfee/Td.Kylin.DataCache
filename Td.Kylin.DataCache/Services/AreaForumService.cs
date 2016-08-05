using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;

namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 区域圈子数据服务
    /// </summary>
    internal sealed class AreaForumService : IAreaForumService
    {
        public List<AreaForumCacheModel> GetEnabledAll()
        {
            using (var db = new DataContext())
            {
                var quer = from p in db.Circle_AreaForum
                           where p.IsDelete == false && p.Disabled == false
                           orderby p.OrderNo descending
                           select new AreaForumCacheModel
                           {
                               CategoryID = p.CategoryID,
                               AliasName = p.AliasName,
                               AreaForumID = p.AreaForumID,
                               AreaID = p.AreaID,
                               Description = p.Description,
                               ForumID = p.ForumID,
                               Logo = p.Logo,
                               Moderators = p.Moderators,
                               OrderNo = p.OrderNo,
                               PassLevel = p.PassLevel,
                               PostLevel = p.PostLevel,
                               PostType = p.PostType
                           };

                return quer.ToList();
            }
        }
    }
}
