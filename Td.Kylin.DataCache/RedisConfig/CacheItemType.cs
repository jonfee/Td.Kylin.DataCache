using Td.ComponentModel;

namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 缓存项类型
    /// </summary>
    public enum CacheItemType
    {
        /// <summary>
        /// 模块授权
        /// </summary>
        [Description("模块授权")]
        ApiModuleAuthorize,
        /// <summary>
        /// 系统区域
        /// </summary>
        [Description("系统区域")]
        SystemArea,
        /// <summary>
        /// 开通区域
        /// </summary>
        [Description("开通区域")]
        OpenArea,
        /// <summary>
        /// 商家行业
        /// </summary>
        [Description("商家行业")]
        MerchantIndustry,
        /// <summary>
        /// 区域行业推荐
        /// </summary>
        [Description("区域行业推荐")]
        AreaRecommendIndustry,
        /// <summary>
        /// 上门预约业务
        /// </summary>
        [Description("上门预约业务")]
        BusinessServices,
        /// <summary>
        /// 商家商品系统分类
        /// </summary>
        [Description("商家商品系统分类")]
        MerchantProductSystemCategory,
        /// <summary>
        /// B2C区域商品分类
        /// </summary>
        [Description("B2C区域商品分类")]
        B2CProductCategory,
        /// <summary>
        /// B2C区域商品分类标签
        /// </summary>
        [Description("B2C区域商品分类标签")]
        B2CProductCategoryTags,
        /// <summary>
        /// 系统全局配置
        /// </summary>
        [Description("系统全局配置")]
        SystemGolbalConfig,
        /// <summary>
        /// 用户积分规则配置
        /// </summary>
        [Description("用户积分规则配置")]
        UserPointsConfig,
        /// <summary>
        /// 用户经验值规则配置
        /// </summary>
        [Description("用户经验值规则配置")]
        UserEmpiricalConfig,
        /// <summary>
        /// 用户等级规则配置
        /// </summary>
        [Description("用户等级规则配置")]
        UserLevelConfig,
        /// <summary>
        /// 社区圈子分类
        /// </summary>
        [Description("社区圈子分类")]
        ForumCategory,
        /// <summary>
        /// 圈子
        /// </summary>
        [Description("圈子")]
        ForumCircle,
        /// <summary>
        /// 区域圈子
        /// </summary>
        [Description("区域圈子")]
        AreaForum,
        /// <summary>
        /// 职位类别
        /// </summary>
        [Description("职位类别")]
        JobCategory,
        /// <summary>
        /// 平台针对区域抽成配置
        /// </summary>
        [Description("平台针对区域抽成配置")]
        PlatformCommission,
        /// <summary>
        /// 区域针对商家抽成配置
        /// </summary>
        [Description("区域针对商家抽成配置")]
        AreaForMerchantCommission,
        /// <summary>
        /// 区域针对个人服务人员抽成配置
        /// </summary>
        [Description("区域针对个人服务人员抽成配置")]
        AreaForPersonalWorkerCommission,
        /// <summary>
        /// 区域默认抽成配置
        /// </summary>
        [Description("区域默认抽成配置")]
        AreaDefaultCommission,
        /// <summary>
        /// 跑退业务物品类型
        /// </summary>
        [Description("跑退业务物品类型")]
        LegworkGoodsCategory,
        /// <summary>
        /// 跑退业务全局配置
        /// </summary>
        [Description("跑退业务全局配置")]
        LegworkGlobalConfig,
        /// <summary>
        /// 跑腿业务区域配置
        /// </summary>
        [Description("跑腿业务区域配置")]
        LegworkAreaConfig,
    }
}
