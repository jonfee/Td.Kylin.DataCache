using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 跑腿业务物品类型缓存
    /// </summary>
    public class LegworkGoodsCategoryCache : CacheItem<LegworkGoodsCategoryCacheModel>
    {
        /// <summary>
        /// 初始化一个<seealso cref="LegworkGoodsCategoryCache"/>实例
        /// </summary>
        public LegworkGoodsCategoryCache() : base(CacheItemType.LegworkGoodsCategory) { }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="categoryID">分类ID</param>
        /// <param name="allScope">是否查找所有缓存域</param>
        /// <returns></returns>
        public LegworkGoodsCategoryCacheModel Get(long categoryID, bool allScope = true)
        {
            var item = new LegworkGoodsCategoryCacheModel { CategoryID = categoryID };

            return Get(item.HashField,allScope);
        }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<LegworkGoodsCategoryCacheModel> ReadDataFromDB()
        {
            return new LegworkGoodsCategoryService().GetAll();
        }
    }
}
