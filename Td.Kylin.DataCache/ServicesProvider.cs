using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.DataCache.Context;
using Td.Kylin.DataCache.IServices;
using Td.Kylin.DataCache.Services;

namespace Td.Kylin.DataCache
{
    internal sealed class ServicesProvider
    {
        private static ServicesProvider _instance;

        private static readonly object myLock = new object();

        /// <summary>
        /// 数据操作服务集
        /// </summary>
        public static ServicesProvider Items
        {
            get
            {
                if (null == _instance)
                {
                    lock (myLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ServicesProvider();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 实例化
        /// </summary>
        private ServicesProvider()
        {
            switch (CacheStartup.SqlType)
            {
                case SqlProviderType.PostgreSQL:
                    InitServices<PostgreSqlDataContext>();
                    break;
                case SqlProviderType.SqlServer:
                    InitServices<SqlServerDataContext>();
                    break;
            }
        }

        #region 数据操作服务定义

        /// <summary>
        /// 系统区域数据服务
        /// </summary>
        public ISystemAreaService SystemAreaService { get; private set; }

        #endregion

        /// <summary>
        /// 数据服务初始化
        /// </summary>
        /// <typeparam name="T"></typeparam>

        void InitServices<T>() where T : DataContext, new()
        {
            SystemAreaService = new SystemAreaService<T>(); //系统区域
        }
    }
}
