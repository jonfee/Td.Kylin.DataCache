namespace Td.Kylin.DataCache.CacheModel
{
    /// <summary>
    /// 缓存模型抽象基类
    /// </summary>
    public abstract class BaseCacheModel
    {
        /// <summary>
        /// HashField
        /// </summary>
        public abstract string HashField { get; }
    }
}
