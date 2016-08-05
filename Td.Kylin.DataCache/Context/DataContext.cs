using Microsoft.EntityFrameworkCore;
using Td.Kylin.Entity;

namespace Td.Kylin.DataCache.Context
{
    internal sealed partial class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            switch (Startup.SqlType)
            {
                case EnumLibrary.SqlProviderType.SqlServer: optionBuilder.UseSqlServer(Startup.SqlConnctionString);break;
                case EnumLibrary.SqlProviderType.NpgSQL:break;
            }
            
        }

        #region OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //系统模块接口授权
            modelBuilder.Entity<System_ModuleAuthorize>(entity =>
            {
                entity.HasKey(p => new { p.ServerID, p.ModuleID });
            });

            //系统全局配置
            modelBuilder.Entity<System_GlobalResources>(entity =>
            {
                entity.HasKey(p => new { p.ResourceType, p.ResourceKey });
            });

            //积分配置
            modelBuilder.Entity<System_PointsConfig>(entity =>
            {
                entity.Property(p => p.ActivityType).ValueGeneratedNever();
                entity.HasKey(p => p.ActivityType);
            });

            //系统用户等级配置
            modelBuilder.Entity<System_Level>(entity =>
            {
                entity.Property(p => p.LevelID).ValueGeneratedNever();
                entity.HasKey(p => p.LevelID);
            });

            //经验值配置
            modelBuilder.Entity<System_EmpiricalConfig>(entity =>
            {
                entity.Property(p => p.ActivityType).ValueGeneratedNever();
                entity.HasKey(p => p.ActivityType);
            });

            //系统商家行业
            modelBuilder.Entity<Merchant_Industry>(entity =>
            {
                entity.Property(p => p.IndustryID).ValueGeneratedNever();
                entity.HasKey(p => p.IndustryID);
            });

            //岗位
            modelBuilder.Entity<Job_Category>(entity =>
            {
                entity.Property(p => p.CategoryID).ValueGeneratedNever();
                entity.HasKey(p => p.CategoryID);
            });

            //上门预约业务
            modelBuilder.Entity<KylinService_Business>(entity =>
            {
                entity.Property(p => p.BusinessID).ValueGeneratedNever();
                entity.HasKey(p => p.BusinessID);
            });

            //上门预约业务子项
            modelBuilder.Entity<KylinService_BusinessOptions>(entity =>
            {
                entity.Property(p => p.OptionID).ValueGeneratedNever();
                entity.HasKey(p => p.OptionID);
            });

            //上门预约业务配置
            modelBuilder.Entity<KylinService_BusinessConfig>(entity =>
            {
                entity.HasKey(p => new { p.BusinessID, p.OptionID });
            });

            //系统区域
            modelBuilder.Entity<System_Area>(entity =>
            {
                entity.Property(p => p.AreaID).ValueGeneratedNever();
                entity.HasKey(p => p.AreaID);
            });

            //开通区域
            modelBuilder.Entity<Area_Open>(entity =>
            {
                entity.Property(p => p.AreaID).ValueGeneratedNever();
                entity.HasKey(p => p.AreaID);
            });

            //区域行业推荐
            modelBuilder.Entity<Area_RecommendIndustry>(entity =>
            {
                entity.HasKey(p => new { p.AreaID, p.IndustryID });
            });

            //圈子分类
            modelBuilder.Entity<Circle_Category>(entity =>
            {
                entity.Property(p => p.CategoryID).ValueGeneratedNever();
                entity.HasKey(p => p.CategoryID);
            });

            //系统圈子
            modelBuilder.Entity<Circle_Forum>(entity =>
            {
                entity.Property(p => p.ForumID).ValueGeneratedNever();
                entity.HasKey(p => p.ForumID);
            });

            //区域圈子
            modelBuilder.Entity<Circle_AreaForum>(entity =>
            {
                entity.Property(p => p.AreaForumID).ValueGeneratedNever();
                entity.HasKey(p => p.AreaForumID);
            });

            modelBuilder.Entity<Mall_Category>(entity =>
            {
                entity.Property(p => p.CategoryID).ValueGeneratedNever();
                entity.HasKey(p => p.CategoryID);
            });

            modelBuilder.Entity<Mall_CategoryTag>(entity =>
            {
                entity.Property(p => p.TagID).ValueGeneratedNever();
                entity.HasKey(p => p.TagID);
            });

            modelBuilder.Entity<MerchantGoods_SystemCategory>(entity =>
            {
                entity.Property(p => p.CategoryID).ValueGeneratedNever();
                entity.HasKey(p => p.CategoryID);
            });

            //平台对区域抽成配置
            modelBuilder.Entity<Commission_Platform>(entity =>
            {
                entity.HasKey(p => new { p.AreaID, p.CommissionItem });
            });

            //区域运营商对区域下交易默认抽成配置
            modelBuilder.Entity<Commission_OperatorDefault>(entity =>
            {
                entity.HasKey(p => new { p.AreaID, p.CommissionItem });
            });

            //区域运营商对商家的抽成配置
            modelBuilder.Entity<Commission_OperatorFromMerchant>(entity =>
            {
                entity.HasKey(p => new { p.AreaID, p.MerchantID, p.CommissionItem });
            });

            //区域运营商对个人服务者的抽成配置
            modelBuilder.Entity<Commission_OperatorFromWorker>(entity =>
            {
                entity.HasKey(p => new { p.AreaID, p.UserID, p.CommissionItem });
            });

            //生活服务分类
            modelBuilder.Entity<Service_SystemCategory>(entity =>
            {
                entity.Property(p => p.CategoryID).ValueGeneratedNever();
                entity.HasKey(p => p.CategoryID);
            });

            //商家自定义分类
            modelBuilder.Entity<MerchGoods_Category>(entity =>
            {
                entity.Property(p => p.CategoryID).ValueGeneratedNever();
                entity.HasKey(p => p.CategoryID);
            });

            #region 跑腿业务

            // 全局配置
            modelBuilder.Entity<Legwork_GlobalConfig>(entity =>
               {
                   entity.Property(p => p.GlobalConfigID).ValueGeneratedNever();
                   entity.HasKey(p => p.GlobalConfigID);
               });

            // 区域配置
            modelBuilder.Entity<Legwork_AreaConfig>(entity =>
            {
                entity.Property(p => p.AreaID).ValueGeneratedNever();
                entity.HasKey(p => p.AreaID);
            });

            // 物品分类
            modelBuilder.Entity<Legwork_GoodsCategory>(entity =>
            {
                entity.Property(p => p.CategoryID).ValueGeneratedNever();
                entity.HasKey(p => p.CategoryID);
            });

            #endregion
        }

        #endregion
    }
}
