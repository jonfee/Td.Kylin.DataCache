using System.Collections.Generic;
using Td.Kylin.DataCache.CacheModel;

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
        /// <returns></returns>
        public LegworkGoodsCategoryCacheModel Get(long categoryID)
        {
            var item = new LegworkGoodsCategoryCacheModel { CategoryID = categoryID };

            return Get(item.HashField);
        }

        /// <summary>
        /// 从数据库中读取数据
        /// </summary>
        /// <returns></returns>
        protected override List<LegworkGoodsCategoryCacheModel> ReadDataFromDB()
        {
            return ServicesProvider.Items.LegworkGoodsCategoryService.GetAll();
        }
    }
}
