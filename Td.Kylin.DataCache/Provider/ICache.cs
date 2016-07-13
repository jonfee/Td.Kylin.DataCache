﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Td.Kylin.DataCache.Provider
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 缓存项类型
        /// </summary>
        CacheItemType ItemType { get; }

        /// <summary>
        /// 缓存名称
        /// </summary>
        CacheLevel Level { get; }

        /// <summary>
        /// 缓存的Key
        /// </summary>
        string CacheKey { get; }

        /// <summary>
        /// 更新缓存
        /// </summary>
         Task<bool> Update();

        /// <summary>
        /// 重置缓存级别
        /// </summary>
        /// <param name="level"></param>
        void ResetLevel(CacheLevel level);

        /// <summary>
        /// 获取缓存原数据
        /// </summary>
        List<object> GetCacheData();
    }
}
