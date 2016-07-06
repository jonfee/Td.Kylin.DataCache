using System.ComponentModel;
namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 缓存级别
    /// </summary>
    public enum CacheLevel
    {
        /// <summary>
        /// 持久
        /// </summary>
        [Description("持久")]
        Permanent,
        /// <summary>
        /// 高
        /// </summary>
        [Description("高")]
        Hight,
        /// <summary>
        /// 中
        /// </summary>
        [Description("中")]
        Middel,
        /// <summary>
        /// 低
        /// </summary>
        [Description("低")]
        Lower
    }
}
