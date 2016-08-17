using Microsoft.Extensions.Options;
using System;

namespace Td.Kylin.DataCache
{
    /// <summary>
    /// 数据缓存服务
    /// </summary>
    public class DataCacheServer : IDataCacheServer
    {
        /// <summary>
        /// 数据缓存服务实例
        /// </summary>
        /// <param name="options"></param>
        public DataCacheServer(IOptions<DataCacheServerOptions> options) : this(options.Value) { }

        /// <summary>
        /// 数据缓存服务实例
        /// </summary>
        /// <param name="options"></param>
        public DataCacheServer(DataCacheServerOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            Options = options ?? new DataCacheServerOptions();
        }

        /// <summary>
        /// 数据缓存服务参数
        /// </summary>
        public DataCacheServerOptions Options { get; }

        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            ValidateOptions();
            
            try
            {
                Startup.Start(Options);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 验证参数
        /// </summary>
        private void ValidateOptions()
        {
            if (Options.RedisOptions == null)
            {
                throw new InvalidOperationException("ConfigurationOptions (Options.RedisOptions) value is null.");
            }

            if (string.IsNullOrWhiteSpace(Options.SqlConnection))
            {
                throw new InvalidOperationException("The cache source database connection string is empty.");
            }
        }
    }
}
