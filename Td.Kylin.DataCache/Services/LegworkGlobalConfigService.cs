using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache.CacheModel;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;


namespace Td.Kylin.DataCache.Services
{
    /// <summary>
    /// 跑腿全局配置服务
    /// </summary>
    internal sealed class LegworkGlobalConfigService : ILegworkGlobalConfigService
    {
        public List<LegworkGlobalConfigCacheModel> GetAll()
        {
            using (var db = new DataContext())
            {
                return (from p in db.Legwork_GlobalConfig
                        select new LegworkGlobalConfigCacheModel
                        {
                            AutoConfirmTime = p.AutoConfirmTime,
                            GlobalConfigID = p.GlobalConfigID,
                            OrderTimeout = p.OrderTimeout,
                            PaymentTimeout = p.PaymentTimeout,
                            QuotationWaitingTime = p.QuotationWaitingTime,
                            QuotationWaitingTimeout = p.QuotationWaitingTimeout,
                            QuotationWaitingWorkers = p.QuotationWaitingWorkers
                        }).ToList();
            }
        }
    }
}
