using Td.ComponentModel;

namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 缓存数据存储类型
    /// </summary>
    public enum RedisSaveType
    {
        /// <summary>
        /// 文本
        /// </summary>
        [Description("文本")]
        Text,
        /// <summary>
        /// HashSet
        /// </summary>
        [Description("HashSet")]
        HashSet
    }
}
